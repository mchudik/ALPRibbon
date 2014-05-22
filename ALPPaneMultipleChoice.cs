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
            ALPPowerpointUtils.WriteMultiQuestionXMLFile(Globals.RibbonAddIn.Application.ActivePresentation, RibbonAddIn.ALPCurrentSlide, QuestionTextBox, dataGridView1, AddJustificationCheckBox, JustificationTextBox);
            string textXML = ALPPowerpointUtils.WriteMultiQuestionXMLString(Globals.RibbonAddIn.Application.ActivePresentation, RibbonAddIn.ALPCurrentSlide, QuestionTextBox, dataGridView1, AddJustificationCheckBox, JustificationTextBox);
            ALPPowerpointUtils.SetSlideNotesText(Globals.RibbonAddIn.Application.ActivePresentation.Slides[RibbonAddIn.ALPCurrentSlide], textXML);
//
            PowerPoint.Slide oSlide = Globals.RibbonAddIn.Application.ActivePresentation.Slides[RibbonAddIn.ALPCurrentSlide];
            // Remove XML Placeholder shapes
            foreach (PowerPoint.Shape shape in oSlide.Shapes)
            {
                if (shape.AlternativeText.Equals("MultipleChoicePoll"))
                {
//                    MessageBox.Show("Retrieved Text Embedded to the Slice:\n\n" + shape.TextFrame.TextRange.Text);
//                    ALPPowerpointUtils.ReadMultiQuestionXMLString(shape.TextFrame.TextRange.Text, RibbonAddIn.ALPCurrentSlide, QuestionTextBox, dataGridView1, AddJustificationCheckBox, JustificationTextBox);
                    shape.Delete();
                }
            }

            // Add XML Placeholder shapes
            PowerPoint.Shapes oShapes = oSlide.Shapes;
            PowerPoint.Shape oShapeText = oShapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, 100, 100, 500, 500);
            PowerPoint.TextRange oTextRange = oShapeText.TextFrame.TextRange;
            oTextRange.Text = textXML;
            oTextRange.Font.Name = "Tahoma";
            oTextRange.Font.Size = 24;
            oShapeText.Width = oSlide.Master.Width;
            oShapeText.Left = 0;
            oShapeText.Top = 0;
//            oShapeText.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
            oShapeText.AlternativeText = "MultipleChoicePoll";
            //
        }

        private void OnResize(object sender, EventArgs e)
        {
            int PaddedWidth = this.Width - 40;
            QuestionTextBox.Width = PaddedWidth;
            dataGridView1.Width = PaddedWidth;
            JustificationTextBox.Width = PaddedWidth;
            SubmitButton.Width = PaddedWidth;
            AnswerDescTextBox.Width = PaddedWidth;
            JustificationDescTextBox.Width = PaddedWidth;
        }

        private void ResetVariables()
        {
            QuestionTextBox.Text = "";
            JustificationTextBox.Text = "";
            AddJustificationCheckBox.Checked = false;
            JustificationDescTextBox.Text = "";
            while(dataGridView1.Rows.Count > 1)
            {
                dataGridView1.Rows.RemoveAt(0);
            }
        }

        public void OnInitialize()
        {
            ResetVariables();
            PowerPoint.Slide oSlide = Globals.RibbonAddIn.Application.ActivePresentation.Slides[RibbonAddIn.ALPCurrentSlide];
            // Process XML Placeholder shapes
            foreach (PowerPoint.Shape shape in oSlide.Shapes)
            {
                if (shape.AlternativeText.Equals("MultipleChoicePoll"))
                {
                    ALPPowerpointUtils.ReadMultiQuestionXMLString(shape.TextFrame.TextRange.Text, RibbonAddIn.ALPCurrentSlide, QuestionTextBox, dataGridView1, AddJustificationCheckBox, JustificationTextBox);
                }
            }
        }
        public void OnExit()
        {
            if (Globals.RibbonAddIn.Application.Active == Microsoft.Office.Core.MsoTriState.msoTrue)
            {
                if (MessageBox.Show("Remove Poll from current slide?", "Multiple Choice", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    PowerPoint.Slide oSlide = Globals.RibbonAddIn.Application.ActivePresentation.Slides[RibbonAddIn.ALPCurrentSlide];
                    // Process XML Placeholder shapes
                    foreach (PowerPoint.Shape shape in oSlide.Shapes)
                    {
                        if (shape.AlternativeText.Equals("MultipleChoicePoll"))
                        {
                            shape.Delete();
                            ResetVariables();
                        }
                    }
                }
            }
        }
    }
}
