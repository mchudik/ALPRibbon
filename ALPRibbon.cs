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
            ALPAboutBox dlg = new ALPAboutBox();
            dlg.ShowDialog();
        }

        private void SignIn_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.RibbonAddIn.ALPLogInTaskPane.Visible = ((RibbonToggleButton)sender).Checked;
        }

        private void UploadButton_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.RibbonAddIn.ALPUploadTaskPane.Visible = ((RibbonToggleButton)sender).Checked;
        }

        private void PublishButton_Click(object sender, RibbonControlEventArgs e)
        {
            ALPPowerpointUtils.ExportLectureSlides();
            MessageBox.Show(Resources.Slides_Exported, Resources.Publish_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MultipleChoiceButton_Click(object sender, RibbonControlEventArgs e)
        {
            PowerPoint.Application oApp = Globals.RibbonAddIn.Application;
            PowerPoint.Presentation oPres = oApp.ActivePresentation;
            PowerPoint.PpSlideLayout oLayout = PowerPoint.PpSlideLayout.ppLayoutBlank;
            PowerPoint.View oView = oApp.ActiveWindow.View;

            int totalSlideCount = oPres.Slides.Count;
            int currentSlide = RibbonAddIn.ALPCurrentSlide;

            oPres.Slides.Add(currentSlide + 1, oLayout);
            oView.GotoSlide(currentSlide + 1);
        }
    }
}
