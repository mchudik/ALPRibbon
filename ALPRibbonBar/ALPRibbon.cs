using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using ALPRibbon.Properties;
using Microsoft.Office.Tools.Ribbon;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

namespace ALPRibbon
{
    public partial class ALPRibbon
    {
        private void ALPRibbon_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void Help_Click(object sender, RibbonControlEventArgs e)
        {
//            ALPAboutBox dlg = new ALPAboutBox();
//            dlg.ShowDialog();
            Globals.RibbonAddIn.bDebug = ((RibbonToggleButton)sender).Checked;
        }

        private void SignIn_Click(object sender, RibbonControlEventArgs e)
        {
            foreach (ALPPaneLogIn pane in Globals.RibbonAddIn.ALPPaneLogInList) {
                if (pane.DocWindow == Globals.RibbonAddIn.Application.ActiveWindow) {
                    pane.TaskPane.Visible = ((RibbonToggleButton)sender).Checked;
                    return;
                }
            }
            // Create LogIn Custom Pane
            Cursor.Current = Cursors.WaitCursor; 
            ALPPaneLogIn ALPPaneLogInControl = new ALPPaneLogIn("User Sign In", Globals.RibbonAddIn.Application.ActiveWindow);
            ALPPaneLogInControl.ALPPaneConfigure(275, 550, 275);
            ALPPaneLogInControl.TaskPane.Visible = ((RibbonToggleButton)sender).Checked;
            Cursor.Current = Cursors.Default;
        }

        private void UploadButton_Click(object sender, RibbonControlEventArgs e)
        {
            foreach (ALPPaneUpload pane in Globals.RibbonAddIn.ALPPaneUploadList) {
                if (pane.DocWindow == Globals.RibbonAddIn.Application.ActiveWindow) {
                    pane.TaskPane.Visible = ((RibbonToggleButton)sender).Checked;
                    return;
                }
            }
            // Create Upload Custom Pane
            Cursor.Current = Cursors.WaitCursor;
            ALPPaneUpload ALPPaneUploadControl = new ALPPaneUpload("Upload Presentation", Globals.RibbonAddIn.Application.ActiveWindow);
            ALPPaneUploadControl.ALPPaneConfigure(450, 600, 450);
            ALPPaneUploadControl.TaskPane.Visible = ((RibbonToggleButton)sender).Checked;
            Cursor.Current = Cursors.Default;
        }

        private void PublishButton_Click(object sender, RibbonControlEventArgs e)
        {
            ALPPowerpointUtils.ExportLectureSlides();
            MessageBox.Show(Resources.Slides_Exported, Resources.Publish_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MultipleChoiceButton_Click(object sender, RibbonControlEventArgs e)
        {
            foreach (ALPPaneMultipleChoice pane in Globals.RibbonAddIn.ALPPaneMultipleChoiceList) {
                if (pane.DocWindow == Globals.RibbonAddIn.Application.ActiveWindow) {
                    pane.TaskPane.Visible = ((RibbonToggleButton)sender).Checked;
                    return;
                }
            }
            // Create MultipleChoice Custom Pane
            Cursor.Current = Cursors.WaitCursor;
            ALPPaneMultipleChoice ALPPaneMultipleChoiceControl = new ALPPaneMultipleChoice("Multiple Choice", Globals.RibbonAddIn.Application.ActiveWindow);
            ALPPaneMultipleChoiceControl.ALPPaneConfigure(500, 600, 300);
            ALPPaneMultipleChoiceControl.TaskPane.Visible = ((RibbonToggleButton)sender).Checked;
            Cursor.Current = Cursors.Default;
        }

        private void ImageQuizButton_Click(object sender, RibbonControlEventArgs e)
        {
            foreach (ALPPaneImageQuiz pane in Globals.RibbonAddIn.ALPPaneImageQuizList) {
                if (pane.DocWindow == Globals.RibbonAddIn.Application.ActiveWindow) {
                    pane.TaskPane.Visible = ((RibbonToggleButton)sender).Checked;
                    return;
                }
            }
            // Create ImageQuiz Custom Pane
            Cursor.Current = Cursors.WaitCursor;
            ALPPaneImageQuiz ALPPaneImageQuizControl = new ALPPaneImageQuiz("Image Quiz", Globals.RibbonAddIn.Application.ActiveWindow);
            ALPPaneImageQuizControl.ALPPaneConfigure(700, 900, 300);
            ALPPaneImageQuizControl.TaskPane.Visible = ((RibbonToggleButton)sender).Checked;
            Cursor.Current = Cursors.Default;
        }

        private void FreeResponseButton_Click(object sender, RibbonControlEventArgs e)
        {
            foreach (ALPPaneFreeResponse pane in Globals.RibbonAddIn.ALPPaneFreeResponseList) {
                if (pane.DocWindow == Globals.RibbonAddIn.Application.ActiveWindow) {
                    pane.TaskPane.Visible = ((RibbonToggleButton)sender).Checked;
                    return;
                }
            }
            // Create FreeResponse Custom Pane
            Cursor.Current = Cursors.WaitCursor;
            ALPPaneFreeResponse ALPPaneFreeResponseControl = new ALPPaneFreeResponse("Free Response", Globals.RibbonAddIn.Application.ActiveWindow);
            ALPPaneFreeResponseControl.ALPPaneConfigure(500, 600, 300);
            ALPPaneFreeResponseControl.TaskPane.Visible = ((RibbonToggleButton)sender).Checked;
            Cursor.Current = Cursors.Default;
        }
    }
}
