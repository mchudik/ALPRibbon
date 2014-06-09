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
    public partial class ALPPaneFreeResponse : UserControl
    {
        public ALPPaneFreeResponse()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (RibbonAddIn.ALPCurrentSlide <= 0)
                    return;

                PowerPoint.Slide oSlide = ALPPowerpointUtils.GetOrInsertPlaceholderSlide("Free_Response");
                if (oSlide != null)
                {
                    // Add Visible items
                    ALPPowerpointUtils.RemoveShapeFromSlide(oSlide, "FreeResponsePollQuestion");
                    AddVisibleShapes(oSlide);

                    //Process Hidden items
                    ALPPowerpointUtils.RemoveShapeFromSlide(oSlide, "FreeResponsePollXML");
                    AddHiddenShapes(oSlide);

                    //Export Slide as Image
                    ALPPowerpointUtils.RemoveShapeFromSlide(oSlide, "FreeResponsePollSlideImage");
                    ALPPowerpointUtils.AddVisibleImageShape(oSlide, "FreeResponsePollSlideImage");

                    // Remove Visible items
                    ALPPowerpointUtils.RemoveShapeFromSlide(oSlide, "FreeResponsePollQuestion");
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
            SubmitButton.Width = PaddedWidth;

            // Dynamic Height Calculation
            SubmitButton.Top = this.Height - 51;
        }

        private void ResetVariables()
        {
            QuestionTextBox.Text = "";
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
                    if (shape.AlternativeText.Equals("FreeResponsePollXML"))
                    {
                        ALPPowerpointUtils.ReadFreeResponseXMLString(shape.TextFrame.TextRange.Text, RibbonAddIn.ALPCurrentSlide, QuestionTextBox);
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
                oShapeTextQuestion.AlternativeText = "FreeResponsePollQuestion";
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
                string textXML = ALPPowerpointUtils.WriteFreeResponseXMLString(Globals.RibbonAddIn.Application.ActivePresentation, RibbonAddIn.ALPCurrentSlide, QuestionTextBox);
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
                oShapeTextXML.AlternativeText = "FreeResponsePollXML";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Resources.Critical_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
