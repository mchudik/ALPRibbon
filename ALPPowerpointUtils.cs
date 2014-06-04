using System;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using ALPRibbon.Properties;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Imaging = System.Drawing.Imaging;
//using System.Diagnostics;

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

        public static void SetSlideNotesText(PowerPoint.Slide slide, string text)
        {
            PowerPoint.Shape oShape = slide.NotesPage.Shapes.Placeholders._Index(2);
            oShape.TextFrame.TextRange.Delete();
            oShape.TextFrame.TextRange.InsertAfter(text);
        }

        public static string WriteMultiQuestionXMLString(PowerPoint.Presentation oPres, int CurentSlideId, TextBox QuestionTextBox, DataGridView dataGridView, CheckBox AddJustificationCheckBox, TextBox JustificationTextBox)
        {
            using (var ms = new MemoryStream())
            using (XmlTextWriter xmlString = new XmlTextWriter(ms, System.Text.Encoding.UTF8))
            {
                WriteMultiQuestionXML(xmlString, oPres, CurentSlideId, QuestionTextBox, dataGridView, AddJustificationCheckBox, JustificationTextBox);
                return System.Text.Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        public static void WriteMultiQuestionXMLFile(PowerPoint.Presentation oPres, int CurentSlideId, TextBox QuestionTextBox, DataGridView dataGridView, CheckBox AddJustificationCheckBox, TextBox JustificationTextBox)
        {
            using (XmlTextWriter xmlFile = new XmlTextWriter(RibbonAddIn.WORKING_DIR + "\\" + RibbonAddIn.EXPORT_DIR + "\\LecturePolls.xml", System.Text.Encoding.UTF8))
            {
                WriteMultiQuestionXML(xmlFile, oPres, CurentSlideId, QuestionTextBox, dataGridView, AddJustificationCheckBox, JustificationTextBox);
            }
        }

        public static void WriteMultiQuestionXML(XmlTextWriter xmlTextWriter, PowerPoint.Presentation oPres, int CurentSlideId, TextBox QuestionTextBox, DataGridView dataGridView, CheckBox AddJustificationCheckBox, TextBox JustificationTextBox)
        {
            try
            {
                //Write the XML delcaration. 
                xmlTextWriter.WriteStartDocument();

                //Use indentation for readability.
                xmlTextWriter.Formatting = Formatting.Indented;

                //Write an element (this one is the root).
//                xmlTextWriter.WriteStartElement("lecture");

                //Write the title element.
//                xmlTextWriter.WriteStartElement("title");
//                xmlTextWriter.WriteString(oPres.Name);
//                xmlTextWriter.WriteEndElement();  //title

                //Write the poll element.
//                xmlTextWriter.WriteStartElement("polls");

                xmlTextWriter.WriteStartElement("poll");
//                xmlTextWriter.WriteAttributeString("slide_index", "" + CurentSlideId + "");
                xmlTextWriter.WriteAttributeString("type", "multiple_choice");

                xmlTextWriter.WriteStartElement("question");
                xmlTextWriter.WriteAttributeString("text", QuestionTextBox.Text);
                xmlTextWriter.WriteEndElement();  //question


                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.IsNewRow) break;
                    if (row.Cells[1].Value == null) continue;   //No text in answer

                    xmlTextWriter.WriteStartElement("answer");
                    if (row.Cells[0].Value != null)
                        xmlTextWriter.WriteAttributeString("correct", row.Cells[0].Value.ToString());
                    else
                        xmlTextWriter.WriteAttributeString("correct", "False");
                    xmlTextWriter.WriteAttributeString("text", row.Cells[1].Value.ToString());
                    xmlTextWriter.WriteEndElement();  //answer
                }

                xmlTextWriter.WriteStartElement("justification");
                xmlTextWriter.WriteAttributeString("text", JustificationTextBox.Text);
                xmlTextWriter.WriteAttributeString("required", AddJustificationCheckBox.Checked.ToString());
                xmlTextWriter.WriteEndElement();  //justification

                xmlTextWriter.WriteEndElement();  //poll

                // Close elements
//                xmlTextWriter.WriteEndElement();  //polls
//                xmlTextWriter.WriteEndElement();  //lecture

                // Write the XML to file and close the xmlFile.
                xmlTextWriter.Flush();
                xmlTextWriter.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), Resources.Critical_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void ReadMultiQuestionXMLString(string stringXML, int CurentSlideId, TextBox QuestionTextBox, DataGridView dataGridView, CheckBox AddJustificationCheckBox, TextBox JustificationTextBox)
        {
            using (var ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(stringXML)))
            using (XmlTextReader xmlString = new XmlTextReader(ms))
            {
                ReadMultiQuestionXML(xmlString, CurentSlideId, QuestionTextBox, dataGridView, AddJustificationCheckBox, JustificationTextBox);
            }
        }

        public static void ReadMultiQuestionXMLFile(PowerPoint.Presentation oPres, int CurentSlideId, TextBox QuestionTextBox, DataGridView dataGridView, CheckBox AddJustificationCheckBox, TextBox JustificationTextBox)
        {
            using (XmlTextReader xmlFile = new XmlTextReader(RibbonAddIn.WORKING_DIR + "\\" + RibbonAddIn.EXPORT_DIR + "\\LecturePolls.xml"))
            {
                ReadMultiQuestionXML(xmlFile, CurentSlideId, QuestionTextBox, dataGridView, AddJustificationCheckBox, JustificationTextBox);
            }
        }

        public static void ReadMultiQuestionXML(XmlTextReader xmlTextReader, int CurentSlideId, TextBox QuestionTextBox, DataGridView dataGridView, CheckBox AddJustificationCheckBox, TextBox JustificationTextBox)
        {
            try
            {
                int nRows = 0;
                //  Loop over the XML file
                while (xmlTextReader.Read())
                {
                    //  Here we check the type of the node, in this case we are looking for element
                    if (xmlTextReader.NodeType == XmlNodeType.Element)
                    {
                        if (xmlTextReader.Name == "poll")
                        {
                            nRows = 0;
//                            Debug.WriteLine(xmlTextReader.GetAttribute("slide_index"));
//                            Debug.WriteLine(xmlTextReader.GetAttribute("type"));
                        }
                        if (xmlTextReader.Name == "question")
                        {
                            QuestionTextBox.Text = xmlTextReader.GetAttribute("text");
                        }
                        if (xmlTextReader.Name == "answer")
                        {
                            dataGridView.Rows.Add();
                            dataGridView.Rows[nRows].Cells[0].Value = XmlConvert.ToBoolean(xmlTextReader.GetAttribute("correct").ToLower());
                            dataGridView.Rows[nRows].Cells[1].Value = xmlTextReader.GetAttribute("text");
                            nRows++;
                        }
                        if (xmlTextReader.Name == "justification")
                        {
                            JustificationTextBox.Text = xmlTextReader.GetAttribute("text");
                            AddJustificationCheckBox.Checked = XmlConvert.ToBoolean(xmlTextReader.GetAttribute("required").ToLower());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), Resources.Critical_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
