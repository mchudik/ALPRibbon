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
                    break;
                }
            }
        }

        private void UploadButton_Click(object sender, RibbonControlEventArgs e)
        {
            foreach (ALPPaneUpload pane in Globals.RibbonAddIn.ALPPaneUploadList) {
                if (pane.DocWindow == Globals.RibbonAddIn.Application.ActiveWindow) {
                    pane.TaskPane.Visible = ((RibbonToggleButton)sender).Checked;
                    break;
                }
            }
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
                    break;
                }
            }
        }

        private void ImageQuizButton_Click(object sender, RibbonControlEventArgs e)
        {
            foreach (ALPPaneImageQuiz pane in Globals.RibbonAddIn.ALPPaneImageQuizList) {
                if (pane.DocWindow == Globals.RibbonAddIn.Application.ActiveWindow) {
                    pane.TaskPane.Visible = ((RibbonToggleButton)sender).Checked;
                    break;
                }
            }
        }

        private void FreeResponseButton_Click(object sender, RibbonControlEventArgs e)
        {
            foreach (ALPPaneFreeResponse pane in Globals.RibbonAddIn.ALPPaneFreeResponseList) {
                if (pane.DocWindow == Globals.RibbonAddIn.Application.ActiveWindow) {
                    pane.TaskPane.Visible = ((RibbonToggleButton)sender).Checked;
                    break;
                }
            }
        }
    }
}
