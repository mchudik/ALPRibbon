using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Office = Microsoft.Office.Core;

namespace ALPRibbon
{
    public partial class RibbonAddIn
    {
        // Properties
        public Microsoft.Office.Tools.CustomTaskPane ALPLogInTaskPane
        {
            get
            {
                return ALPPaneLogInTaskPane;
            }
        }

        // Methods
        private ALPPaneLogIn ALPPaneLogInControl;
        private Microsoft.Office.Tools.CustomTaskPane ALPPaneLogInTaskPane;

        // Event Handlers
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            ALPPaneLogInControl = new ALPPaneLogIn();
            ALPPaneLogInTaskPane = this.CustomTaskPanes.Add(ALPPaneLogInControl, "User Sign In");
            ALPPaneLogInTaskPane.VisibleChanged += new EventHandler(ALPPaneLogInTaskPane_VisibleChanged);
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        private void ALPPaneLogInTaskPane_VisibleChanged(object sender, System.EventArgs e)
        {
            Globals.Ribbons.ALPRibbon.SignIn.Checked = ALPPaneLogInTaskPane.Visible;
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
