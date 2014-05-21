using System;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using ALPRibbon.Properties;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Imaging = System.Drawing.Imaging;

namespace ALPRibbon
{
    class ALPPowerpointUtils
    {
        const Microsoft.Office.Core.MsoTriState TRUE =
            Microsoft.Office.Core.MsoTriState.msoTrue;

        // export all lecture slides and text without a dialog
        public static void ExportLectureSlides()
        {
            // clean  the export directory
            ALPGeneralUtils.ClearDirectory(RibbonAddIn.EXPORT_DIR);

            // create the zip file name
            DateTime currentTime = DateTime.Now;
            String zipName = "slides_ " + currentTime.ToString("MM_dd_yy_HH_mm_ss") + ".zip";
            
            try
            {
                // get current app
                PowerPoint.Application oApp = Globals.RibbonAddIn.Application;

                // get active presentation
                PowerPoint.Presentation oPres = oApp.ActivePresentation;

                // dump lecture text xml
                CreateLectureIndex(oPres);

                // zip up the files
                ALPGeneralUtils.CreateZipFile(Path.Combine(RibbonAddIn.WORKING_DIR, RibbonAddIn.EXPORT_DIR), RibbonAddIn.WORKING_DIR, zipName);

                // cleanup temp files
                ALPGeneralUtils.ClearDirectory(RibbonAddIn.EXPORT_DIR);

                // move to desktop
                File.Move(Path.Combine(RibbonAddIn.WORKING_DIR, zipName), Path.Combine(RibbonAddIn.DESKTOP_DIR, zipName));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), Resources.Critical_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }

        // export all lecture slides and text with a progress dialog
        public static void ExportLectureSlides(Form myOwner, ProgressBar myProgress)
        {
            // clean  the export directory
            ALPGeneralUtils.ClearDirectory(RibbonAddIn.EXPORT_DIR);

            // create the zip file name
            DateTime currentTime = DateTime.Now;
            String zipName = "slides_ " + currentTime.ToString("MM_dd_yy_HH_mm_ss") + ".zip";

            try
            {
                // get current app
                PowerPoint.Application oApp = Globals.RibbonAddIn.Application;

                // get active presentation
                PowerPoint.Presentation oPres = oApp.ActivePresentation;

                // dump lecture text xml
                CreateLectureIndex(oPres, myProgress);

                // zip up the files
                ALPGeneralUtils.CreateZipFile(Path.Combine(RibbonAddIn.WORKING_DIR, RibbonAddIn.EXPORT_DIR), RibbonAddIn.WORKING_DIR, zipName);

                // cleanup temp files
                ALPGeneralUtils.ClearDirectory(RibbonAddIn.EXPORT_DIR);

                // copy to desktop
                File.Copy(Path.Combine(RibbonAddIn.WORKING_DIR, zipName), Path.Combine(RibbonAddIn.DESKTOP_DIR, zipName), true);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), Resources.Critical_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            myOwner.Close();
        }

        public static void CreateLectureIndex(PowerPoint.Presentation oPres, ProgressBar myProgress = null)
        {
            using (XmlTextWriter xmlFile = new XmlTextWriter(RibbonAddIn.WORKING_DIR + "\\" + RibbonAddIn.EXPORT_DIR + "\\lecture.xml", System.Text.Encoding.UTF8))
            {
                //Write the XML delcaration. 
                xmlFile.WriteStartDocument();

                //Use indentation for readability.
                xmlFile.Formatting = Formatting.Indented;

                //Write an element (this one is the root).
                xmlFile.WriteStartElement("lecture");

                //Write the title element.
                xmlFile.WriteStartElement("title");
                xmlFile.WriteString(oPres.Name);
                xmlFile.WriteEndElement();  //title

                //Write the slides element.
                xmlFile.WriteStartElement("slides");

                using (StreamWriter jsonFile = new StreamWriter(RibbonAddIn.WORKING_DIR + "\\" + RibbonAddIn.EXPORT_DIR + "\\lecture.js", true))
                {
                    jsonFile.Write("var presentationData = '{\"lecture\": { \"title\": \"" + oPres.Name + "\", \"slides\": { \"slide\": [");

                    if (myProgress != null)
                    {
                        myProgress.Maximum = oPres.Slides.Count;
                    }

                    for (int i = 1; i < oPres.Slides.Count + 1; i++)
                    {
                        if (myProgress != null)
                        {
                            myProgress.Value = i;
                        }

                        PowerPoint.Slide currentSlide = oPres.Slides[i];

                        xmlFile.WriteStartElement("slide");
                        xmlFile.WriteAttributeString("id", "" + i + "");

                        if (i == 1)
                        {
                            jsonFile.Write("{ \"-id\": \"" + i + "\", \"content\": [");
                        }
                        else
                        {
                            jsonFile.Write(",{ \"-id\": \"" + i + "\", \"content\": [");
                        }

                        currentSlide.Export(RibbonAddIn.WORKING_DIR + "\\" + RibbonAddIn.EXPORT_DIR + "\\slide_" + i + ".png", "PNG", 800, 600);
                        currentSlide.Export(RibbonAddIn.WORKING_DIR + "\\" + RibbonAddIn.EXPORT_DIR + "\\thumb_slide_" + i + ".png", "PNG", 240, 180);

                        Boolean resetShapes = true;
                        foreach (PowerPoint.Shape shape in currentSlide.Shapes)
                        {
                            if (shape.HasTextFrame == TRUE)
                            {
                                var textFrame = shape.TextFrame;
                                var textRange = textFrame.TextRange;
                                var paragraphs = textRange.Paragraphs(-1, -1);

                                foreach (PowerPoint.TextRange paragraph in paragraphs)
                                {
                                    xmlFile.WriteStartElement("content");
                                    xmlFile.WriteAttributeString("indent_level", "" + paragraph.IndentLevel + "");
                                    xmlFile.WriteAttributeString("bullet", "" + paragraph.ParagraphFormat.Bullet.Type + "");
                                    xmlFile.WriteAttributeString("text", "" + paragraph.Text.Trim() + "");
                                    xmlFile.WriteEndElement();  //content

                                    if (resetShapes)
                                    {
                                        jsonFile.Write("{ \"-indent_level\": \"" + paragraph.IndentLevel + "\", \"-bullet\": \"" + paragraph.ParagraphFormat.Bullet.Type + "\", \"#text\": \"" + paragraph.Text.Trim() + "\" }");
                                        resetShapes = false;
                                    }
                                    else
                                    {
                                        jsonFile.Write(",{ \"-indent_level\": \"" + paragraph.IndentLevel + "\", \"-bullet\": \"" + paragraph.ParagraphFormat.Bullet.Type + "\", \"#text\": \"" + paragraph.Text.Trim() + "\" }");
                                    }
                                }
                            }
                        }

                        if (currentSlide.Hyperlinks.Count > 0)
                        {
                            xmlFile.WriteStartElement("hyperlinks");
                            foreach (PowerPoint.Hyperlink link in currentSlide.Hyperlinks)
                            {
                                xmlFile.WriteStartElement("hyperlink");
                                xmlFile.WriteAttributeString("display-text", "" + link.TextToDisplay + "");
                                xmlFile.WriteAttributeString("sub-address", "" + link.SubAddress + "");
                                xmlFile.WriteAttributeString("email-subject", "" + link.EmailSubject + "");
                                xmlFile.WriteAttributeString("url", "" + link.Address + "");
                                xmlFile.WriteEndElement();  //hyperlink
                            }
                            xmlFile.WriteEndElement();  //hyperlinks
                        }

                        xmlFile.WriteEndElement();  //slide
                        jsonFile.Write("] }");
                    }
                    // Close elements
                    xmlFile.WriteEndElement();  //slides
                    xmlFile.WriteEndElement();  //lecture

                    // Write the XML to file and close the xmlFile.
                    xmlFile.Flush();
                    xmlFile.Close();

                    jsonFile.Write("] } } }';");
                    jsonFile.Close();
                }
            }
        }

        public static string GetSlideNotesText(PowerPoint.Slide slide)
        {
            if (slide.HasNotesPage == Microsoft.Office.Core.MsoTriState.msoTrue)
            {
                PowerPoint.Shape oShape = slide.NotesPage.Shapes.Placeholders._Index(2);
                if (oShape.HasTextFrame == Microsoft.Office.Core.MsoTriState.msoTrue)
                {
                    if (oShape.TextFrame.HasText == Microsoft.Office.Core.MsoTriState.msoTrue)
                    {
                        return oShape.TextFrame.TextRange.Text;
                    }
                }
            }
            return "";
        }
    }
}
