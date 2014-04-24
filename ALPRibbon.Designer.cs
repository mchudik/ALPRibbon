namespace ALPRibbon
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
            this.SignIn = this.Factory.CreateRibbonButton();
            this.Content = this.Factory.CreateRibbonGroup();
            this.Upload = this.Factory.CreateRibbonButton();
            this.Publish = this.Factory.CreateRibbonButton();
            this.Update = this.Factory.CreateRibbonButton();
            this.AddInteractivity = this.Factory.CreateRibbonGroup();
            this.MultipleChoice = this.Factory.CreateRibbonButton();
            this.ImageQuiz = this.Factory.CreateRibbonButton();
            this.FreeResponse = this.Factory.CreateRibbonButton();
            this.Sources = this.Factory.CreateRibbonGroup();
            this.Analytics = this.Factory.CreateRibbonButton();
            this.Help = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.Account.SuspendLayout();
            this.Content.SuspendLayout();
            this.AddInteractivity.SuspendLayout();
            this.Sources.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.Groups.Add(this.Account);
            this.tab1.Groups.Add(this.Content);
            this.tab1.Groups.Add(this.AddInteractivity);
            this.tab1.Groups.Add(this.Sources);
            this.tab1.Label = "LectureTools ALP";
            this.tab1.Name = "tab1";
            // 
            // Account
            // 
            this.Account.Items.Add(this.SignIn);
            this.Account.Label = "Account";
            this.Account.Name = "Account";
            // 
            // SignIn
            // 
            this.SignIn.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.SignIn.Image = global::ALPRibbon.Properties.Resources.PlaceHolder;
            this.SignIn.Label = "Sign In";
            this.SignIn.Name = "SignIn";
            this.SignIn.ShowImage = true;
            // 
            // Content
            // 
            this.Content.Items.Add(this.Upload);
            this.Content.Items.Add(this.Publish);
            this.Content.Items.Add(this.Update);
            this.Content.Label = "Content";
            this.Content.Name = "Content";
            // 
            // Upload
            // 
            this.Upload.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.Upload.Image = global::ALPRibbon.Properties.Resources.PlaceHolder;
            this.Upload.Label = "Upload";
            this.Upload.Name = "Upload";
            this.Upload.ShowImage = true;
            // 
            // Publish
            // 
            this.Publish.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.Publish.Image = global::ALPRibbon.Properties.Resources.PlaceHolder;
            this.Publish.Label = "Publish";
            this.Publish.Name = "Publish";
            this.Publish.ShowImage = true;
            // 
            // Update
            // 
            this.Update.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.Update.Image = global::ALPRibbon.Properties.Resources.PlaceHolder;
            this.Update.Label = "Update";
            this.Update.Name = "Update";
            this.Update.ShowImage = true;
            // 
            // AddInteractivity
            // 
            this.AddInteractivity.Items.Add(this.MultipleChoice);
            this.AddInteractivity.Items.Add(this.ImageQuiz);
            this.AddInteractivity.Items.Add(this.FreeResponse);
            this.AddInteractivity.Label = "Add Interactivity";
            this.AddInteractivity.Name = "AddInteractivity";
            // 
            // MultipleChoice
            // 
            this.MultipleChoice.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.MultipleChoice.Image = global::ALPRibbon.Properties.Resources.PlaceHolder;
            this.MultipleChoice.Label = "Multiple Choice";
            this.MultipleChoice.Name = "MultipleChoice";
            this.MultipleChoice.ShowImage = true;
            // 
            // ImageQuiz
            // 
            this.ImageQuiz.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.ImageQuiz.Image = global::ALPRibbon.Properties.Resources.PlaceHolder;
            this.ImageQuiz.Label = "Image Quiz";
            this.ImageQuiz.Name = "ImageQuiz";
            this.ImageQuiz.ShowImage = true;
            // 
            // FreeResponse
            // 
            this.FreeResponse.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.FreeResponse.Image = global::ALPRibbon.Properties.Resources.PlaceHolder;
            this.FreeResponse.Label = "Free Response";
            this.FreeResponse.Name = "FreeResponse";
            this.FreeResponse.ShowImage = true;
            // 
            // Sources
            // 
            this.Sources.Items.Add(this.Analytics);
            this.Sources.Items.Add(this.Help);
            this.Sources.Label = "Sources";
            this.Sources.Name = "Sources";
            // 
            // Analytics
            // 
            this.Analytics.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.Analytics.Image = global::ALPRibbon.Properties.Resources.PlaceHolder;
            this.Analytics.Label = "Analytics";
            this.Analytics.Name = "Analytics";
            this.Analytics.ShowImage = true;
            // 
            // Help
            // 
            this.Help.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.Help.Image = global::ALPRibbon.Properties.Resources.PlaceHolder;
            this.Help.Label = "Help";
            this.Help.Name = "Help";
            this.Help.ShowImage = true;
            // 
            // ALPRibbon
            // 
            this.Name = "ALPRibbon";
            this.RibbonType = "Microsoft.PowerPoint.Presentation";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.ALPRibbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.Account.ResumeLayout(false);
            this.Account.PerformLayout();
            this.Content.ResumeLayout(false);
            this.Content.PerformLayout();
            this.AddInteractivity.ResumeLayout(false);
            this.AddInteractivity.PerformLayout();
            this.Sources.ResumeLayout(false);
            this.Sources.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup Account;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton SignIn;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup Content;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup AddInteractivity;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup Sources;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton Upload;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton Publish;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton Update;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton MultipleChoice;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton ImageQuiz;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton FreeResponse;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton Analytics;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton Help;
    }

    partial class ThisRibbonCollection
    {
        internal ALPRibbon ALPRibbon
        {
            get { return this.GetRibbon<ALPRibbon>(); }
        }
    }
}
