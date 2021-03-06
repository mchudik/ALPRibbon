﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Tools = Microsoft.Office.Tools;
using ALPRibbon.Properties;

namespace ALPRibbon
{
    public partial class ALPPaneLogIn : UserControl
    {
        public Tools.CustomTaskPane TaskPane;
        public PowerPoint.DocumentWindow DocWindow;

        public ALPPaneLogIn()
        {
            InitializeComponent();
        }

        public ALPPaneLogIn(string strName, PowerPoint.DocumentWindow docWindow)
        {
            InitializeComponent();
            DocWindow = docWindow;
            TaskPane = Globals.RibbonAddIn.CustomTaskPanes.Add(this, strName, DocWindow);
            TaskPane.VisibleChanged += new EventHandler(ALPPane_VisibleChanged);
            Globals.RibbonAddIn.ALPPaneLogInList.Add(this);
            Globals.Ribbons.ALPRibbon.SignInButton.Checked = true;
        }

        public void ALPPane_VisibleChanged(object sender, System.EventArgs e)
        {
            if (DocWindow == Globals.RibbonAddIn.Application.ActiveWindow) 
            {
                Globals.Ribbons.ALPRibbon.SignInButton.Checked = TaskPane.Visible;
                if (TaskPane.Visible)
                    InitVariables();
                else
                    ResetVariables();
            }
        }

        public void ALPPaneConfigure(int floatingWidth, int floatingHeight, int dockedWidth)
        {
            // Set default for floating view    
            TaskPane.DockPosition = Microsoft.Office.Core.MsoCTPDockPosition.msoCTPDockPositionFloating;
            TaskPane.Width = floatingWidth;
            TaskPane.Height = floatingHeight;
            // Set default for docked view    
            TaskPane.DockPosition = Microsoft.Office.Core.MsoCTPDockPosition.msoCTPDockPositionRight;
            TaskPane.Width = dockedWidth;
            // Set docking restrictions
            TaskPane.DockPositionRestrict = Microsoft.Office.Core.MsoCTPDockPositionRestrict.msoCTPDockPositionRestrictNoHorizontal;
        }

        public void ALPPaneDelete()
        {
            Globals.RibbonAddIn.CustomTaskPanes.Remove(TaskPane);
            TaskPane.Dispose();
            Globals.RibbonAddIn.ALPPaneLogInList.Remove(this);
            this.Dispose();
        }

        private void ResetVariables()
        {
        }

        public void InitVariables()
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Resources.Critical_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
