using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using ALPRibbon.Properties;
using System.IO;
using Tools = Microsoft.Office.Tools;

namespace ALPRibbon
{
    public partial class ALPPaneImageQuiz : UserControl
    {
        public Tools.CustomTaskPane TaskPane;
        public PowerPoint.DocumentWindow DocWindow;

        public ALPPaneImageQuiz()
        {
            InitializeComponent();
        }

        public ALPPaneImageQuiz(string strName, PowerPoint.DocumentWindow docWindow)
        {
            InitializeComponent();
            DocWindow = docWindow;
            TaskPane = Globals.RibbonAddIn.CustomTaskPanes.Add(this, strName, DocWindow);
            TaskPane.VisibleChanged += new EventHandler(ALPPane_VisibleChanged);
            Globals.RibbonAddIn.ALPPaneImageQuizList.Add(this);
            Globals.Ribbons.ALPRibbon.ImageQuizButton.Checked = true;
        }

        public void ALPPane_VisibleChanged(object sender, System.EventArgs e)
        {
            if (DocWindow == Globals.RibbonAddIn.Application.ActiveWindow) 
            {
                Globals.Ribbons.ALPRibbon.ImageQuizButton.Checked = TaskPane.Visible;
                if (TaskPane.Visible)
                    InitVariables();
                else
                    ResetVariables();
            }
        }

        public void ALPPaneConfigure(int floatingWidth, int floatingHeight, int dockedWidth)
        {
            // Set default for floating view    
            TaskPane.DockPosition = Microsoft.Office.Core.MsoCTPDockPosition.msoCTPDockPositionFloating;
            TaskPane.Width = floatingWidth;
            TaskPane.Height = floatingHeight;
            // Set default for docked view    
            TaskPane.DockPosition = Microsoft.Office.Core.MsoCTPDockPosition.msoCTPDockPositionRight;
            TaskPane.Width = dockedWidth;
            // Set docking restrictions
            TaskPane.DockPositionRestrict = Microsoft.Office.Core.MsoCTPDockPositionRestrict.msoCTPDockPositionRestrictNoHorizontal;
        }

        public void ALPPaneDelete()
        {
            Globals.RibbonAddIn.CustomTaskPanes.Remove(TaskPane);
            TaskPane.Dispose();
            Globals.RibbonAddIn.ALPPaneImageQuizList.Remove(this);
            this.Dispose();
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (RibbonAddIn.ALPCurrentSlide <= 0)
                    return;

                // Mark the solution if not saved yet
                bMarked = true;
                using (Pen p = new Pen(Color.Green, 2.0F))
                {
                    if (ImagePictureBox.Image != null)
                    {
                        // Draw the rectangle
                        using (Graphics g = Graphics.FromImage(ImagePictureBox.Image))
                        {
                            g.DrawRectangle(p, SolutionRect);
                        }
                    }
                }
                ImagePictureBox.Invalidate(true);

                PowerPoint.Slide oSlide = ALPPowerpointUtils.GetOrInsertPlaceholderSlide("Image_Quiz");
                if (oSlide != null)
                {
                    // Add Visible items
                    ALPPowerpointUtils.RemoveShapeFromSlide(oSlide, "ImageQuizPollQuestion");
                    ALPPowerpointUtils.RemoveShapeFromSlide(oSlide, "ImageQuizPollImage");
                    ALPPowerpointUtils.RemoveShapeFromSlide(oSlide, "ImageQuizPollJustification");
                    AddVisibleShapes(oSlide);

                    //Process Hidden items
                    ALPPowerpointUtils.RemoveShapeFromSlide(oSlide, "ImageQuizPollXML");
                    ALPPowerpointUtils.RemoveShapeFromSlide(oSlide, "ImageQuizPollImageMTD");
                    AddHiddenShapes(oSlide);

                    //Export Slide as Image
                    ALPPowerpointUtils.RemoveShapeFromSlide(oSlide, "ImageQuizPollSlideImage");
                    ALPPowerpointUtils.AddVisibleImageShape(oSlide, "ImageQuizPollSlideImage");

                    // Remove Visible items
                    ALPPowerpointUtils.RemoveShapeFromSlide(oSlide, "ImageQuizPollQuestion");
                    ALPPowerpointUtils.RemoveShapeFromSlide(oSlide, "ImageQuizPollImage");
                    ALPPowerpointUtils.RemoveShapeFromSlide(oSlide, "ImageQuizPollJustification");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Resources.Critical_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnResize(object sender, EventArgs e)
        {
            // Dynamic Width Calculation
            int PaddedWidth = this.Width - 40;
            QuestionTextBox.Width = PaddedWidth;
            ImagePictureBox.Width = PaddedWidth;
            JustificationTextBox.Width = PaddedWidth;
            MarkSolutionButton.Width = PaddedWidth;
            SubmitButton.Width = PaddedWidth;

            // Dynamic Height Calculation
            ImagePictureBox.Height = this.Height - ImagePictureBox.Top - 214;
            if (ImagePictureBox.Height < 50) ImagePictureBox.Height = 50;
            int PaddedHeight = ImagePictureBox.Top + ImagePictureBox.Height;
            MarkSolutionButton.Top = PaddedHeight + 10;
            AddJustificationCheckBox.Top = PaddedHeight + 55;
            JustificationDescTextBox.Top = PaddedHeight + 78;
            JustificationTextBox.Top = PaddedHeight + 107;
            SubmitButton.Top = PaddedHeight + 163;
        }

        private void ResetVariables()
        {
            QuestionTextBox.Text = "";
            JustificationTextBox.Text = "";
            AddJustificationCheckBox.Checked = false;
            ImagePictureBox.Image = null;
            ImageNameLabel.Text = "Click To Select";
        }

        public void InitVariables()
        {
            try
            {
                // Clear all UI variables
                ResetVariables();

                if (RibbonAddIn.ALPCurrentSlide <= 0)
                    return;

                PowerPoint.Slide oSlide = Globals.RibbonAddIn.Application.ActivePresentation.Slides[RibbonAddIn.ALPCurrentSlide];
                // Read XML Placeholder shape for this poll
                foreach (PowerPoint.Shape shape in oSlide.Shapes)
                {
                    if (shape.AlternativeText.Equals("ImageQuizPollXML"))
                    {
                        SolutionRect = Rectangle.FromLTRB(0, 0, 0, 0);
                        ALPPowerpointUtils.ReadImageQuizXMLString(shape.TextFrame.TextRange.Text, RibbonAddIn.ALPCurrentSlide, QuestionTextBox, ref SolutionRect, AddJustificationCheckBox, JustificationTextBox);
                    }
                    if (shape.AlternativeText.Equals("ImageQuizPollImageMTD"))
                    {
                        ImagePictureBox.Load(shape.LinkFormat.SourceFullName);
                        ImageNameLabel.Text = Path.GetFileName(shape.LinkFormat.SourceFullName);
                    }
                }
                bMarked = true;
                ImagePictureBox.Invalidate(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Resources.Critical_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddVisibleShapes(PowerPoint.Slide oSlide)
        {
            try
            {
                PowerPoint.PageSetup oPageSetup = Globals.RibbonAddIn.Application.ActivePresentation.PageSetup;
                float nSlideWidth = oPageSetup.SlideWidth;
                float nSlideHeight = oPageSetup.SlideHeight;
                PowerPoint.Shapes oShapes = oSlide.Shapes;

                // Add Question Title
                PowerPoint.Shape oShapeTextQuestion = oShapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, 100, 100, nSlideWidth, nSlideHeight);
                PowerPoint.TextRange oTextRangeQuestion = oShapeTextQuestion.TextFrame.TextRange;
                oTextRangeQuestion.ParagraphFormat.Alignment = PowerPoint.PpParagraphAlignment.ppAlignCenter;
                oTextRangeQuestion.Text = QuestionTextBox.Text;
                oTextRangeQuestion.Font.Name = "Tahoma";
                oTextRangeQuestion.Font.Size = 36;
                oTextRangeQuestion.Font.Bold = Microsoft.Office.Core.MsoTriState.msoTrue;
                oShapeTextQuestion.Left = nSlideWidth / 10;
                oShapeTextQuestion.Top = 10;
                oShapeTextQuestion.Width = 8 * (nSlideWidth / 10);
                oShapeTextQuestion.Height = oShapeTextQuestion.TextFrame.TextRange.BoundHeight;
                oShapeTextQuestion.AlternativeText = "ImageQuizPollQuestion";

                // Add Visible Image
                if (ImagePictureBox.ImageLocation != null)
                {
                    // Export the image with ROI to a bitmap
                    string strFileName = ImagePictureBox.ImageLocation;
                    if (ImagePictureBox.Image != null)
                    {
                        strFileName = RibbonAddIn.WORKING_DIR + "\\" + RibbonAddIn.EXPORT_DIR + "\\" + ImageNameLabel.Text;
                        ImagePictureBox.Image.Save(strFileName);
                    }
                    PowerPoint.Shape oShapePicture = oShapes.AddPicture(strFileName, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, 0, 0);
                    oShapePicture.Width = 8 * (nSlideWidth / 10);
                    if (AddJustificationCheckBox.Checked)
                        oShapePicture.Height = 6 * (nSlideHeight / 10);
                    else
                        oShapePicture.Height = 8 * (nSlideHeight / 10);
                    if (oShapePicture.Width > nSlideWidth)
                        oShapePicture.Width = nSlideWidth;
                    oShapePicture.Left = (nSlideWidth / 2) - (oShapePicture.Width / 2);
                    oShapePicture.Top = 2 * (nSlideHeight / 10); ;
                    oShapePicture.AlternativeText = "ImageQuizPollImage";
                }

                // Add Justification
                if (AddJustificationCheckBox.Checked)
                {
                    PowerPoint.Shape oShapeTextJust = oShapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, 100, 100, nSlideWidth, nSlideHeight);
                    PowerPoint.TextRange oTextRangeJust = oShapeTextJust.TextFrame.TextRange;
                    oTextRangeJust.Text = "\nAdd Justification\t";
                    oTextRangeJust.Text += "\n";
                    oTextRangeJust.Text += JustificationTextBox.Text;
                    oTextRangeJust.Font.Name = "Tahoma";
                    oTextRangeJust.Font.Size = 24;
                    oShapeTextJust.Left = nSlideWidth / 10;
                    oShapeTextJust.Top = 8 * (nSlideHeight / 10);
                    oShapeTextJust.AlternativeText = "ImageQuizPollJustification";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Resources.Critical_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddHiddenShapes(PowerPoint.Slide oSlide)
        {
            try
            {
                // Add XML Placeholder shape for this poll
                string textXML = ALPPowerpointUtils.WriteImageQuizXMLString(Globals.RibbonAddIn.Application.ActivePresentation, RibbonAddIn.ALPCurrentSlide, QuestionTextBox, SolutionRect, AddJustificationCheckBox, JustificationTextBox);
                PowerPoint.Shapes oShapes = oSlide.Shapes;
                PowerPoint.Shape oShapeTextXML = oShapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, 100, 100, 500, 500);
                PowerPoint.TextRange oTextRangeXML = oShapeTextXML.TextFrame.TextRange;
                oTextRangeXML.Text = textXML;
                oTextRangeXML.Font.Name = "Tahoma";
                oTextRangeXML.Font.Size = 20;
                oShapeTextXML.Width = oSlide.Master.Width;
                oShapeTextXML.Left = 0;
                oShapeTextXML.Top = 0;
                if (Globals.RibbonAddIn.bDebug == false)
                    oShapeTextXML.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                oShapeTextXML.AlternativeText = "ImageQuizPollXML";

                // Add MetaData shape for image of this poll
                if (ImagePictureBox.ImageLocation != null)
                {
                    PowerPoint.Shape oShapePicture = oShapes.AddPicture(ImagePictureBox.ImageLocation, Microsoft.Office.Core.MsoTriState.msoTrue, Microsoft.Office.Core.MsoTriState.msoFalse, 0, 0);
                    oShapePicture.Left = 0;
                    oShapePicture.Top = 0;
                    if (Globals.RibbonAddIn.bDebug == false)
                        oShapePicture.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                    oShapePicture.AlternativeText = "ImageQuizPollImageMTD";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Resources.Critical_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImagePictureBox_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.Filter = "Images (*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG|All files (*.*)|*.*";
            if (openFileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ImagePictureBox.Load(openFileDlg.FileName);
                ImageNameLabel.Text = Path.GetFileName(openFileDlg.FileName);
            }
        }

        // Image Drawing section
        private Point initialMousePos;
        private Point initialImagePos;
        private Point currentMousePos;
        private bool bDrawing = false;
        private bool bDrawn = false;
        private bool bMarked = false;
        private Rectangle SolutionRect;

        private Point ImageMousePos(Point p)
        {
            Point unscaled_p = new Point();
            try
            {
                if (ImagePictureBox.Image == null) return unscaled_p;

                // image and container dimensions
                float w_i = ImagePictureBox.Image.Width;
                float h_i = ImagePictureBox.Image.Height;
                float w_c = ImagePictureBox.ClientSize.Width;
                float h_c = ImagePictureBox.ClientSize.Height;

                float imageRatio = w_i / h_i; // image W:H ratio
                float containerRatio = w_c / h_c; // container W:H ratio

                if (imageRatio >= containerRatio)
                {
                    // horizontal image
                    float scaleFactor = w_c / w_i;
                    float scaledHeight = h_i * scaleFactor;
                    // calculate gap between top of container and top of image
                    float filler = Math.Abs(h_c - scaledHeight) / 2;
                    unscaled_p.X = (int)((float)p.X / scaleFactor);
                    unscaled_p.Y = (int)(((float)p.Y - filler) / scaleFactor);
                }
                else
                {
                    // vertical image
                    float scaleFactor = h_c / h_i;
                    float scaledWidth = w_i * scaleFactor;
                    float filler = Math.Abs(w_c - scaledWidth) / 2;
                    unscaled_p.X = (int)(((float)p.X - filler) / scaleFactor);
                    unscaled_p.Y = (int)((float)p.Y / scaleFactor);
                }

                return unscaled_p;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Resources.Critical_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return unscaled_p;
            }
        }

        private void ImagePictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                bDrawing = true;
                bMarked = false;
                this.initialMousePos = e.Location;
                this.initialImagePos = ImageMousePos(e.Location);
                if (ImagePictureBox.ImageLocation != null)
                    ImagePictureBox.Load(ImagePictureBox.ImageLocation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Resources.Critical_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImagePictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (!bDrawing)
                    return;
                
                if (ImagePictureBox.Image == null)
                    return;

                // Save the final position of the mouse
                Point finalImagePos = ImageMousePos(e.Location);

                // Adjust the final rectangle to fit the image
                if (this.initialImagePos.X < 0) this.initialImagePos.X = 0;
                if (this.initialImagePos.Y < 0) this.initialImagePos.Y = 0;
                if (finalImagePos.X >= ImagePictureBox.Image.Width) finalImagePos.X = ImagePictureBox.Image.Width;
                if (finalImagePos.Y >= ImagePictureBox.Image.Height) finalImagePos.Y = ImagePictureBox.Image.Height;

                // Create the rectangle from the two points
                SolutionRect = Rectangle.FromLTRB(
                                                    this.initialImagePos.X,
                                                    this.initialImagePos.Y,
                                                    finalImagePos.X,
                                                    finalImagePos.Y);

                // Do whatever you want with the rectangle here
                // ...
                bDrawing = false;
                // Automatinc Image snapping and final Marking
                bDrawn = true;
                ImagePictureBox.Invalidate(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Resources.Critical_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImagePictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (!bDrawing)
                    return;

                // Save the current position of the mouse
                currentMousePos = e.Location;

                // Force the picture box to be repainted
                ImagePictureBox.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Resources.Critical_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImagePictureBox_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (bDrawing)
                {
                    // Create a pen object that we'll use to draw
                    // (change these parameters to make it any color and size you want)
                    using (Pen p = new Pen(Color.Red, 2.0F))
                    {
                        // Create a rectangle with the initial cursor location as the upper-left
                        // point, and the current cursor location as the bottom-right point
                        Rectangle currentRect = Rectangle.FromLTRB(
                                                                   this.initialMousePos.X,
                                                                   this.initialMousePos.Y,
                                                                   currentMousePos.X,
                                                                   currentMousePos.Y);

                        // Draw the rectangle
                        e.Graphics.DrawRectangle(p, currentRect);
                    }
                }
                else
                {
                    if (bDrawn == true)
                    {
                        using (Pen p = new Pen(Color.Red, 2.0F))
                        {
                            if (ImagePictureBox.Image != null)
                            {
                                // Draw the rectangle
                                using (Graphics g = Graphics.FromImage(ImagePictureBox.Image))
                                {
                                    g.DrawRectangle(p, SolutionRect);
                                }
                            }
                        }
                        bDrawn = false;
                        ImagePictureBox.Invalidate(true);
                    }
                    if (bMarked == true)
                    {
                        using (Pen p = new Pen(Color.Green, 2.0F))
                        {
                            if (ImagePictureBox.Image != null)
                            {
                                // Draw the rectangle
                                using (Graphics g = Graphics.FromImage(ImagePictureBox.Image))
                                {
                                    g.DrawRectangle(p, SolutionRect);
                                }
                            }
                        }
                        bMarked = false;
                        ImagePictureBox.Invalidate(true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Resources.Critical_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImageNameLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDlg = new OpenFileDialog();
                openFileDlg.Filter = "Images (*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG|All files (*.*)|*.*";
                if (openFileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ImagePictureBox.Load(openFileDlg.FileName);
                    ImageNameLabel.Text = Path.GetFileName(openFileDlg.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Resources.Critical_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MarkSolutionButton_Click(object sender, EventArgs e)
        {
            try
            {
                bMarked = true;
                ImagePictureBox.Invalidate(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Resources.Critical_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
