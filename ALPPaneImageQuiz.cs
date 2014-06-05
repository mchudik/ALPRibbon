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

                PowerPoint.Slide oSlide = Globals.RibbonAddIn.Application.ActivePresentation.Slides[RibbonAddIn.ALPCurrentSlide];
                // Remove XML Placeholder shapes for this poll
                foreach (PowerPoint.Shape shape in oSlide.Shapes)
                {
                    if (shape.AlternativeText.Equals("ImageQuizPollXML"))
                    {
                        shape.Delete();
                    }
                }
                // Remove Image Placeholder shapes for this poll
                foreach (PowerPoint.Shape shape in oSlide.Shapes)
                {
                    if (shape.AlternativeText.Equals("ImageQuizPollImage"))
                    {
                        shape.Delete();
                    }
                }

                // Add XML Placeholder shape for this poll
                string textXML = ALPPowerpointUtils.WriteImageQuizXMLString(Globals.RibbonAddIn.Application.ActivePresentation, RibbonAddIn.ALPCurrentSlide, QuestionTextBox, DescriptionTextBox, AddJustificationCheckBox, JustificationTextBox);
                PowerPoint.Shapes oShapes = oSlide.Shapes;
                PowerPoint.Shape oShapeText = oShapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, 100, 100, 500, 500);
                PowerPoint.TextRange oTextRange = oShapeText.TextFrame.TextRange;
                oTextRange.Text = textXML;
                oTextRange.Font.Name = "Tahoma";
                oTextRange.Font.Size = 20;
                oShapeText.Width = oSlide.Master.Width;
                oShapeText.Left = 0;
                oShapeText.Top = 0;
                if (Globals.RibbonAddIn.bDebug == false)
                    oShapeText.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                oShapeText.AlternativeText = "ImageQuizPollXML";

                // Add Placeholder shape for image of this poll
                PowerPoint.Shape oShapePicture = oShapes.AddPicture(ImagePictureBox.ImageLocation, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, 0, 0);
                oShapePicture.Left = 0;
                oShapePicture.Top = 0;
                if (Globals.RibbonAddIn.bDebug == false)
                    oShapePicture.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                oShapePicture.AlternativeText = "ImageQuizPollImage";
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
            DescriptionTextBox.Width = PaddedWidth;

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
            DescriptionTextBox.Text = "";
//            while (dataGridView1.Rows.Count > 1)
            {
//                dataGridView1.Rows.RemoveAt(0);
            }
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
                        ALPPowerpointUtils.ReadImageQuizXMLString(shape.TextFrame.TextRange.Text, RibbonAddIn.ALPCurrentSlide, QuestionTextBox, DescriptionTextBox, AddJustificationCheckBox, JustificationTextBox);
                    }
                    if (shape.AlternativeText.Equals("ImageQuizPollImage"))
                    {
                        //                        ALPPowerpointUtils.ReadMultiQuestionXMLString(shape.TextFrame.TextRange.Text, RibbonAddIn.ALPCurrentSlide, QuestionTextBox, dataGridView1, AddJustificationCheckBox, JustificationTextBox);
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
                    PowerPoint.Slide oSlide = Globals.RibbonAddIn.Application.ActivePresentation.Slides[RibbonAddIn.ALPCurrentSlide];
                    
                    bool bRemovePoll = false;
                    // Check if remove Placeholder shapes for this poll
                    foreach (PowerPoint.Shape shape in oSlide.Shapes)
                    {
                        if (shape.AlternativeText.Equals("ImageQuizPollXML") || shape.AlternativeText.Equals("ImageQuizPollImage"))
                        {
                            if (MessageBox.Show("Remove Poll from current slide?", "Multiple Choice", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                bRemovePoll = true;
                                break;
                            }
                        }
                    }
                    if(bRemovePoll == true) {
                        // Remove XML Placeholder shape for this poll
                        foreach (PowerPoint.Shape shape in oSlide.Shapes)
                        {
                            if (shape.AlternativeText.Equals("ImageQuizPollXML"))
                            {
                                shape.Delete();
                            }
                        }
                        // Remove Image Placeholder shape for this poll
                        foreach (PowerPoint.Shape shape in oSlide.Shapes)
                        {
                            if (shape.AlternativeText.Equals("ImageQuizPollImage"))
                            {
                                shape.Delete();
                            }
                        }
                    }
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
                ImagePictureBox.Load(openFileDlg.FileName);
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
