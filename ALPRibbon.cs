using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
