﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ALPRibbon.Properties;
using Microsoft.Office.Tools.Ribbon;

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
    }
}
