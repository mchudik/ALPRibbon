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
                    if (shape.AlternativeText.Equals("FreeResponsePoll"))
                    {
                        shape.Delete();
                    }
                }

                // Add XML Placeholder shape for this poll
                string textXML = "";// ALPPowerpointUtils.WriteMultiQuestionXMLString(Globals.RibbonAddIn.Application.ActivePresentation, RibbonAddIn.ALPCurrentSlide, QuestionTextBox, dataGridView1, AddJustificationCheckBox, JustificationTextBox);
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
                oShapeText.AlternativeText = "FreeResponsePoll";
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
            ResponseTextBox.Width = PaddedWidth;

            // Dynamic Height Calculation
            ResponseTextBox.Height = this.Height - ResponseTextBox.Top - 67;
            if (ResponseTextBox.Height < 40) ResponseTextBox.Height = 40;
            int PaddedHeight = ResponseTextBox.Top + ResponseTextBox.Height;
            SubmitButton.Top = PaddedHeight + 16;
        }

        private void ResetVariables()
        {
            QuestionTextBox.Text = "";
            ResponseTextBox.Text = "";
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
                    if (shape.AlternativeText.Equals("FreeResponsePoll"))
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
                    // Remove XML Placeholder shape for this poll
                    foreach (PowerPoint.Shape shape in oSlide.Shapes)
                    {
                        if (shape.AlternativeText.Equals("FreeResponsePoll"))
                        {
                            if (MessageBox.Show("Remove Poll from current slide?", "Multiple Choice", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
