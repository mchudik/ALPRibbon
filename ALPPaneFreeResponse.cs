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

                PowerPoint.Slide oSlide = Globals.RibbonAddIn.Application.ActivePresentation.Slides[RibbonAddIn.ALPCurrentSlide];
                // Remove XML Placeholder shapes for this poll
                foreach (PowerPoint.Shape shape in oSlide.Shapes)
                {
                    if (shape.AlternativeText.Equals("FreeResponsePollXML"))
                    {
                        shape.Delete();
                    }
                }

                // Add XML Placeholder shape for this poll
                string textXML = ALPPowerpointUtils.WriteFreeResponseXMLString(Globals.RibbonAddIn.Application.ActivePresentation, RibbonAddIn.ALPCurrentSlide, QuestionTextBox);
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
                oShapeText.AlternativeText = "FreeResponsePollXML";
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
                    PowerPoint.Slide oSlide = Globals.RibbonAddIn.Application.ActivePresentation.Slides[RibbonAddIn.ALPCurrentSlide];
                    // Remove XML Placeholder shape for this poll
                    foreach (PowerPoint.Shape shape in oSlide.Shapes)
                    {
                        if (shape.AlternativeText.Equals("FreeResponsePollXML"))
                        {
                            if (MessageBox.Show("Remove Poll from current slide?", "Free Response", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
    }
}
