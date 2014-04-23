﻿namespace ALPRibbon
{
    partial class ALPRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public ALPRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tab1 = this.Factory.CreateRibbonTab();
            this.Account = this.Factory.CreateRibbonGroup();
            this.tab1.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.Groups.Add(this.Account);
            this.tab1.Label = "LectureTools ALP";
            this.tab1.Name = "tab1";
            // 
            // Account
            // 
            this.Account.Label = "Account";
            this.Account.Name = "Account";
            // 
            // ALPRibbon
            // 
            this.Name = "ALPRibbon";
            this.RibbonType = "Microsoft.PowerPoint.Presentation";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.ALPRibbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup Account;
    }

    partial class ThisRibbonCollection
    {
        internal ALPRibbon ALPRibbon
        {
            get { return this.GetRibbon<ALPRibbon>(); }
        }
    }
}
