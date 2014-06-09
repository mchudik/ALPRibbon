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

namespace ALPRibbon
{
    public partial class ALPPaneImageQuiz : UserControl
    {
        public ALPPaneImageQuiz()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (RibbonAddIn.ALPCurrentSlide <= 0)
                    return;

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

        public void OnInitialize()
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
                        ALPPowerpointUtils.ReadImageQuizXMLString(shape.TextFrame.TextRange.Text, RibbonAddIn.ALPCurrentSlide, QuestionTextBox, AddJustificationCheckBox, JustificationTextBox);
                    }
                    if (shape.AlternativeText.Equals("ImageQuizPollImageMTD"))
                    {
                        ImagePictureBox.Load(shape.LinkFormat.SourceFullName);
                        ImageNameLabel.Text = Path.GetFileName(shape.LinkFormat.SourceFullName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Resources.Critical_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void OnExit()
        {
            try
            {
                if (RibbonAddIn.ALPCurrentSlide <= 0)
                    return;

                if (Globals.RibbonAddIn.Application.Active == Microsoft.Office.Core.MsoTriState.msoTrue)
                {
                    // Clear all UI variables
                    ResetVariables();
                }
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
                    PowerPoint.Shape oShapePicture = oShapes.AddPicture(ImagePictureBox.ImageLocation, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, 0, 0);
                    oShapePicture.Width = 8 * (nSlideWidth / 10);
                    if (AddJustificationCheckBox.Checked)
                        oShapePicture.Height = 6 * (nSlideHeight / 10);
                    else
                        oShapePicture.Height = 8 * (nSlideHeight / 10);
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
                string textXML = ALPPowerpointUtils.WriteImageQuizXMLString(Globals.RibbonAddIn.Application.ActivePresentation, RibbonAddIn.ALPCurrentSlide, QuestionTextBox, AddJustificationCheckBox, JustificationTextBox);
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

        private Point initialMousePos;
        private Point currentMousePos;
        private bool bDrawing = false;
        private bool bMarked = false;
        private Rectangle solutionRect;

        private void ImagePictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            bDrawing = true;
            bMarked = false;
            this.initialMousePos = e.Location;
        }

        private void ImagePictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (!bDrawing)
                return;

            // Save the final position of the mouse
            Point finalMousePos = e.Location;

            // Create the rectangle from the two points
            solutionRect = Rectangle.FromLTRB(
                                                this.initialMousePos.X,
                                                this.initialMousePos.Y,
                                                finalMousePos.X,
                                                finalMousePos.Y);

            // Do whatever you want with the rectangle here
            // ...
            bDrawing = false;
        }

        private void ImagePictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (!bDrawing)
                return;

            // Save the current position of the mouse
            currentMousePos = e.Location;

            // Force the picture box to be repainted
            ImagePictureBox.Invalidate();
        }

        private void ImagePictureBox_Paint(object sender, PaintEventArgs e)
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
                if (bMarked == true)
                {
                    using (Pen p = new Pen(Color.Green, 2.0F))
                    {
                        // Draw the rectangle
                        e.Graphics.DrawRectangle(p, solutionRect);
                    }
                }
            }
        }

        private void ImageNameLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.Filter = "Images (*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG|All files (*.*)|*.*";
            if (openFileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ImagePictureBox.Load(openFileDlg.FileName);
                ImageNameLabel.Text = Path.GetFileName(openFileDlg.FileName);
            }
        }

        private void MarkSolutionButton_Click(object sender, EventArgs e)
        {
            bMarked = true;
            ImagePictureBox.Invalidate();
        }
    }
}
