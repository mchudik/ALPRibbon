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

namespace ALPRibbon
{
    public partial class ALPPaneMultipleChoice : UserControl
    {
        public ALPPaneMultipleChoice()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (RibbonAddIn.ALPCurrentSlide <= 0)
                    return;

                PowerPoint.Slide oSlide = ALPPowerpointUtils.GetOrInsertPlaceholderSlide("Multiple_Choice");
                if (oSlide != null)
                {
                    // Add Visible items
                    ALPPowerpointUtils.RemoveShapeFromSlide(oSlide, "MultipleChoicePollQuestion");
                    ALPPowerpointUtils.RemoveShapeFromSlide(oSlide, "MultipleChoicePollAnswers");
                    ALPPowerpointUtils.RemoveShapeFromSlide(oSlide, "MultipleChoicePollJustification");
                    AddVisibleShapes(oSlide);

                    //Process Hidden items
                    ALPPowerpointUtils.RemoveShapeFromSlide(oSlide, "MultipleChoicePollXML");
                    AddHiddenShapes(oSlide);

                    //Export Slide as Image
                    ALPPowerpointUtils.RemoveShapeFromSlide(oSlide, "MultipleChoicePollImage");
                    AddVisibleImageShape(oSlide);

                    // Remove Visible items
                    ALPPowerpointUtils.RemoveShapeFromSlide(oSlide, "MultipleChoicePollQuestion");
                    ALPPowerpointUtils.RemoveShapeFromSlide(oSlide, "MultipleChoicePollAnswers");
                    ALPPowerpointUtils.RemoveShapeFromSlide(oSlide, "MultipleChoicePollJustification");
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
            dataGridView.Width = PaddedWidth;
            JustificationTextBox.Width = PaddedWidth;
            SubmitButton.Width = PaddedWidth;
            AnswerDescTextBox.Width = PaddedWidth;
            JustificationDescTextBox.Width = PaddedWidth;

            // Dynamic Height Calculation
            dataGridView.Height = this.Height - dataGridView.Top - 190;
            if (dataGridView.Height < 50) dataGridView.Height = 50;
            int PaddedHeight = dataGridView.Top + dataGridView.Height;
            JustificationLabel.Top = PaddedHeight + 10;
            AddJustificationCheckBox.Top = PaddedHeight + 31;
            JustificationDescTextBox.Top = PaddedHeight + 54;
            JustificationTextBox.Top = PaddedHeight + 83;
            SubmitButton.Top = PaddedHeight + 139;
        }

        private void ResetVariables()
        {
            QuestionTextBox.Text = "";
            JustificationTextBox.Text = "";
            AddJustificationCheckBox.Checked = false;
            while(dataGridView.Rows.Count > 1)
            {
                dataGridView.Rows.RemoveAt(0);
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
                // Read XML hidden shape for this poll
                foreach (PowerPoint.Shape shape in oSlide.Shapes)
                {
                    if (shape.AlternativeText.Equals("MultipleChoicePollXML"))
                    {
                        ALPPowerpointUtils.ReadMultiQuestionXMLString(shape.TextFrame.TextRange.Text, RibbonAddIn.ALPCurrentSlide, QuestionTextBox, dataGridView, AddJustificationCheckBox, JustificationTextBox);
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
                oShapeTextQuestion.Top = (nSlideHeight - oShapeTextQuestion.Height) / 7;
                oShapeTextQuestion.Width = 8 * (nSlideWidth / 10);
                oShapeTextQuestion.Height = oShapeTextQuestion.TextFrame.TextRange.BoundHeight;
                oShapeTextQuestion.AlternativeText = "MultipleChoicePollQuestion";

                // Add bulleted answer list
                PowerPoint.Shape oShapeText = oShapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, 100, 100, nSlideWidth, nSlideHeight);
                PowerPoint.TextRange oTextRange = oShapeText.TextFrame.TextRange;
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.IsNewRow) break;
                    if (oTextRange.Text.Length > 0)
                        oTextRange.Text += "\n";
                    if (row.Cells[0].Value != null)
                        oTextRange.Text += row.Cells[0].Value.ToString();
                    else
                        oTextRange.Text += "False";
                    oTextRange.Text += "\t";
                    oTextRange.Text += row.Cells[1].Value.ToString();
                }
                oTextRange.ParagraphFormat.Alignment = PowerPoint.PpParagraphAlignment.ppAlignLeft;
                PowerPoint.ParagraphFormat oParagraphFormat = oTextRange.ParagraphFormat;
                oParagraphFormat.SpaceWithin = (float)1.5;
                oParagraphFormat.Bullet.Type = PowerPoint.PpBulletType.ppBulletNumbered;
                oTextRange.Font.Name = "Tahoma";
                oTextRange.Font.Size = 24;
                oShapeText.Width = 8*(nSlideWidth / 10);
                oShapeText.Height = oShapeText.TextFrame.TextRange.BoundHeight;
                oShapeText.Left = nSlideWidth / 10;
                oShapeText.Top = oShapeTextQuestion.Top + oShapeTextQuestion.Height + 40;
                oShapeText.AlternativeText = "MultipleChoicePollAnswers";

                // Add Justification
                PowerPoint.Shape oShapeTextJust = oShapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, 100, 100, nSlideWidth, nSlideHeight);
                PowerPoint.TextRange oTextRangeJust = oShapeTextJust.TextFrame.TextRange;
                oTextRangeJust.Text = "\nAdd Justification:\t" + AddJustificationCheckBox.Checked.ToString();
                oTextRangeJust.Text += "\n";
                oTextRangeJust.Text += JustificationTextBox.Text;
                oTextRangeJust.Font.Name = "Tahoma";
                oTextRangeJust.Font.Size = 24;
                oShapeTextJust.Left = nSlideWidth / 10;
                oShapeTextJust.Top = oShapeText.Top + oShapeText.Height;
                oShapeTextJust.AlternativeText = "MultipleChoicePollJustification";
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
                string textXML = ALPPowerpointUtils.WriteMultiQuestionXMLString(Globals.RibbonAddIn.Application.ActivePresentation, RibbonAddIn.ALPCurrentSlide, QuestionTextBox, dataGridView, AddJustificationCheckBox, JustificationTextBox);
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
                oShapeTextXML.AlternativeText = "MultipleChoicePollXML";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Resources.Critical_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AddVisibleImageShape(PowerPoint.Slide oSlide)
        {
            try
            {
                // Export the slide to a bitmap
                string strFileName = RibbonAddIn.WORKING_DIR + "\\" + RibbonAddIn.EXPORT_DIR + "\\" + oSlide.Name + ".png";
                oSlide.Export(strFileName, "PNG");

                // Add Placeholder shape for image of this poll
                PowerPoint.Shape oShapePicture = oSlide.Shapes.AddPicture(strFileName, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, 0, 0);
                oShapePicture.Left = 0;
                oShapePicture.Top = 0;
                oShapePicture.AlternativeText = "MultipleChoicePollImage";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Resources.Critical_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
