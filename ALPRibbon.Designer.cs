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
            this.SignIn.ScreenTip = "Sign In User";
            this.SignIn.ShowImage = true;
            this.SignIn.SuperTip = "User needs to sign in first to use the rest of the tools in the ribbon, will brin" +
    "g up window in PowerPoint.";
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
            this.Upload.ScreenTip = "Upload to Server";
            this.Upload.ShowImage = true;
            this.Upload.SuperTip = "Allows user to upload presentation to LT, can select course, class, and can overw" +
    "rite classes that already have a presentation.";
            // 
            // Publish
            // 
            this.Publish.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.Publish.Image = global::ALPRibbon.Properties.Resources.PlaceHolder;
            this.Publish.Label = "Publish";
            this.Publish.Name = "Publish";
            this.Publish.ScreenTip = "Publish to Server";
            this.Publish.ShowImage = true;
            this.Publish.SuperTip = "Once the instructor is satisfied with their presentation, they can publish it to " +
    "the web so their students can view it.";
            // 
            // Update
            // 
            this.Update.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.Update.Image = global::ALPRibbon.Properties.Resources.PlaceHolder;
            this.Update.Label = "Update";
            this.Update.Name = "Update";
            this.Update.ScreenTip = "Update Server";
            this.Update.ShowImage = true;
            this.Update.SuperTip = "After instructor uploads or publishes their presentation to the web, they can cli" +
    "ck on “update” after they add slides, make changes, etc.";
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
            this.MultipleChoice.ScreenTip = "Multiple Choice Question";
            this.MultipleChoice.ShowImage = true;
            this.MultipleChoice.SuperTip = "When an instructor decides to create add a mutliple choice question, a new slide " +
    "is created that allows them to insert a question and the corresponding answers.";
            // 
            // ImageQuiz
            // 
            this.ImageQuiz.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.ImageQuiz.Image = global::ALPRibbon.Properties.Resources.PlaceHolder;
            this.ImageQuiz.Label = "Image Quiz";
            this.ImageQuiz.Name = "ImageQuiz";
            this.ImageQuiz.ScreenTip = "Image Quiz Question";
            this.ImageQuiz.ShowImage = true;
            this.ImageQuiz.SuperTip = "When an instructor decides to create an image quiz, a new slide is created that a" +
    "llows them to upload their image.";
            // 
            // FreeResponse
            // 
            this.FreeResponse.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.FreeResponse.Image = global::ALPRibbon.Properties.Resources.PlaceHolder;
            this.FreeResponse.Label = "Free Response";
            this.FreeResponse.Name = "FreeResponse";
            this.FreeResponse.ScreenTip = "Free Response Question";
            this.FreeResponse.ShowImage = true;
            this.FreeResponse.SuperTip = "When an instructor decides to create a free response question, a new slide is cre" +
    "ated that allows them to ask a question that requires their students to respond " +
    "freely.";
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
            this.Help.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.Help_Click);
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
