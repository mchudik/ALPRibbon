using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Office = Microsoft.Office.Core;
using System.Runtime.InteropServices;

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
        public Microsoft.Office.Tools.CustomTaskPane ALPUploadTaskPane
        {
            get
            {
                return ALPPaneUploadTaskPane;
            }
        }

        // Custom Pane Controls
        private ALPPaneLogIn ALPPaneLogInControl;
        private Microsoft.Office.Tools.CustomTaskPane ALPPaneLogInTaskPane;
        private ALPPaneUpload ALPPaneUploadControl;
        private Microsoft.Office.Tools.CustomTaskPane ALPPaneUploadTaskPane;

        // Event Handlers
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            ALPPaneLogInControl = new ALPPaneLogIn();
            ALPPaneLogInTaskPane = this.CustomTaskPanes.Add(ALPPaneLogInControl, "User Sign In");
            ALPPaneLogInTaskPane.VisibleChanged += new EventHandler(ALPPaneLogInTaskPane_VisibleChanged);
            ALPPaneLogInTaskPane.DockPosition = Microsoft.Office.Core.MsoCTPDockPosition.msoCTPDockPositionFloating;
            ALPPaneLogInTaskPane.Width = 275;
            ALPPaneLogInTaskPane.Height = 550;
            ALPPaneLogInTaskPane.DockPosition = Microsoft.Office.Core.MsoCTPDockPosition.msoCTPDockPositionRight;
            ALPPaneLogInTaskPane.DockPositionRestrict = Microsoft.Office.Core.MsoCTPDockPositionRestrict.msoCTPDockPositionRestrictNoHorizontal;

            ALPPaneUploadControl = new ALPPaneUpload();
            ALPPaneUploadTaskPane = this.CustomTaskPanes.Add(ALPPaneUploadControl, "Upload Presentation");
            ALPPaneUploadTaskPane.VisibleChanged += new EventHandler(ALPPaneUploadTaskPane_VisibleChanged);
            ALPPaneUploadTaskPane.DockPosition = Office.MsoCTPDockPosition.msoCTPDockPositionRight;
            ALPPaneUploadTaskPane.Width = 450;
            ALPPaneUploadTaskPane.DockPosition = Office.MsoCTPDockPosition.msoCTPDockPositionFloating;
            ALPPaneUploadTaskPane.Width = 450;
            ALPPaneUploadTaskPane.Height = 600;
            ALPPaneUploadTaskPane.DockPositionRestrict = Microsoft.Office.Core.MsoCTPDockPositionRestrict.msoCTPDockPositionRestrictNoHorizontal;

        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        private void ALPPaneLogInTaskPane_VisibleChanged(object sender, System.EventArgs e)
        {
            Globals.Ribbons.ALPRibbon.SignInButton.Checked = ALPPaneLogInTaskPane.Visible;
        }

        private void ALPPaneUploadTaskPane_VisibleChanged(object sender, System.EventArgs e)
        {
            Globals.Ribbons.ALPRibbon.UploadButton.Checked = ALPPaneUploadTaskPane.Visible;
            if (ALPPaneUploadTaskPane.Visible == true)
            {
                var window = FindWindowW("MsoCommandBar", ALPPaneUploadTaskPane.Title); //MLHIDE
                if (window == null) return;
                MoveWindow(window, 600, 200, ALPPaneUploadTaskPane.Width, ALPPaneUploadTaskPane.Height, true);
            }
        }

        [DllImport("user32.dll", EntryPoint = "FindWindowW")]
        public static extern System.IntPtr FindWindowW([System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPWStr)] string lpClassName, [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPWStr)] string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "MoveWindow")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public static extern bool MoveWindow([System.Runtime.InteropServices.InAttribute()] System.IntPtr hWnd, int X, int Y, int nWidth, int nHeight, [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)] bool bRepaint);

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
