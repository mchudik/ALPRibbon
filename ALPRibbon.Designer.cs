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
            this.ALPRibbonTab = this.Factory.CreateRibbonTab();
            this.Account = this.Factory.CreateRibbonGroup();
            this.Content = this.Factory.CreateRibbonGroup();
            this.AddInteractivity = this.Factory.CreateRibbonGroup();
            this.Sources = this.Factory.CreateRibbonGroup();
            this.SignInButton = this.Factory.CreateRibbonToggleButton();
            this.UploadButton = this.Factory.CreateRibbonButton();
            this.PublishButton = this.Factory.CreateRibbonButton();
            this.UpdateButton = this.Factory.CreateRibbonButton();
            this.MultipleChoiceButton = this.Factory.CreateRibbonButton();
            this.ImageQuizButton = this.Factory.CreateRibbonButton();
            this.FreeResponseButton = this.Factory.CreateRibbonButton();
            this.AnalyticsButton = this.Factory.CreateRibbonButton();
            this.HelpButton = this.Factory.CreateRibbonButton();
            this.ALPRibbonTab.SuspendLayout();
            this.Account.SuspendLayout();
            this.Content.SuspendLayout();
            this.AddInteractivity.SuspendLayout();
            this.Sources.SuspendLayout();
            // 
            // ALPRibbonTab
            // 
            this.ALPRibbonTab.Groups.Add(this.Account);
            this.ALPRibbonTab.Groups.Add(this.Content);
            this.ALPRibbonTab.Groups.Add(this.AddInteractivity);
            this.ALPRibbonTab.Groups.Add(this.Sources);
            this.ALPRibbonTab.Label = "LectureTools ALP";
            this.ALPRibbonTab.Name = "ALPRibbonTab";
            // 
            // Account
            // 
            this.Account.Items.Add(this.SignInButton);
            this.Account.Label = "Account";
            this.Account.Name = "Account";
            // 
            // Content
            // 
            this.Content.Items.Add(this.UploadButton);
            this.Content.Items.Add(this.PublishButton);
            this.Content.Items.Add(this.UpdateButton);
            this.Content.Label = "Content";
            this.Content.Name = "Content";
            // 
            // AddInteractivity
            // 
            this.AddInteractivity.Items.Add(this.MultipleChoiceButton);
            this.AddInteractivity.Items.Add(this.ImageQuizButton);
            this.AddInteractivity.Items.Add(this.FreeResponseButton);
            this.AddInteractivity.Label = "Add Interactivity";
            this.AddInteractivity.Name = "AddInteractivity";
            // 
            // Sources
            // 
            this.Sources.Items.Add(this.AnalyticsButton);
            this.Sources.Items.Add(this.HelpButton);
            this.Sources.Label = "Sources";
            this.Sources.Name = "Sources";
            // 
            // SignInButton
            // 
            this.SignInButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.SignInButton.Image = global::ALPRibbon.Properties.Resources.PlaceHolder;
            this.SignInButton.Label = "Sign In";
            this.SignInButton.Name = "SignInButton";
            this.SignInButton.ScreenTip = "User Sign In";
            this.SignInButton.ShowImage = true;
            this.SignInButton.SuperTip = "User needs to sign in first to use the rest of the tools in the ribbon, will brin" +
    "g up window in PowerPoint.";
            this.SignInButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.SignIn_Click);
            // 
            // UploadButton
            // 
            this.UploadButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.UploadButton.Image = global::ALPRibbon.Properties.Resources.PlaceHolder;
            this.UploadButton.Label = "Upload";
            this.UploadButton.Name = "UploadButton";
            this.UploadButton.ScreenTip = "Upload to Server";
            this.UploadButton.ShowImage = true;
            this.UploadButton.SuperTip = "Allows user to upload presentation to LT, can select course, class, and can overw" +
    "rite classes that already have a presentation.";
            // 
            // PublishButton
            // 
            this.PublishButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.PublishButton.Image = global::ALPRibbon.Properties.Resources.PlaceHolder;
            this.PublishButton.Label = "Publish";
            this.PublishButton.Name = "PublishButton";
            this.PublishButton.ScreenTip = "Publish to Server";
            this.PublishButton.ShowImage = true;
            this.PublishButton.SuperTip = "Once the instructor is satisfied with their presentation, they can publish it to " +
    "the web so their students can view it.";
            // 
            // UpdateButton
            // 
            this.UpdateButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.UpdateButton.Image = global::ALPRibbon.Properties.Resources.PlaceHolder;
            this.UpdateButton.Label = "Update";
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.ScreenTip = "Update Server";
            this.UpdateButton.ShowImage = true;
            this.UpdateButton.SuperTip = "After instructor uploads or publishes their presentation to the web, they can cli" +
    "ck on “update” after they add slides, make changes, etc.";
            // 
            // MultipleChoiceButton
            // 
            this.MultipleChoiceButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.MultipleChoiceButton.Image = global::ALPRibbon.Properties.Resources.PlaceHolder;
            this.MultipleChoiceButton.Label = "Multiple Choice";
            this.MultipleChoiceButton.Name = "MultipleChoiceButton";
            this.MultipleChoiceButton.ScreenTip = "Multiple Choice Question";
            this.MultipleChoiceButton.ShowImage = true;
            this.MultipleChoiceButton.SuperTip = "When an instructor decides to create add a mutliple choice question, a new slide " +
    "is created that allows them to insert a question and the corresponding answers.";
            // 
            // ImageQuizButton
            // 
            this.ImageQuizButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.ImageQuizButton.Image = global::ALPRibbon.Properties.Resources.PlaceHolder;
            this.ImageQuizButton.Label = "Image Quiz";
            this.ImageQuizButton.Name = "ImageQuizButton";
            this.ImageQuizButton.ScreenTip = "Image Quiz Question";
            this.ImageQuizButton.ShowImage = true;
            this.ImageQuizButton.SuperTip = "When an instructor decides to create an image quiz, a new slide is created that a" +
    "llows them to upload their image.";
            // 
            // FreeResponseButton
            // 
            this.FreeResponseButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.FreeResponseButton.Image = global::ALPRibbon.Properties.Resources.PlaceHolder;
            this.FreeResponseButton.Label = "Free Response";
            this.FreeResponseButton.Name = "FreeResponseButton";
            this.FreeResponseButton.ScreenTip = "Free Response Question";
            this.FreeResponseButton.ShowImage = true;
            this.FreeResponseButton.SuperTip = "When an instructor decides to create a free response question, a new slide is cre" +
    "ated that allows them to ask a question that requires their students to respond " +
    "freely.";
            // 
            // AnalyticsButton
            // 
            this.AnalyticsButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.AnalyticsButton.Image = global::ALPRibbon.Properties.Resources.PlaceHolder;
            this.AnalyticsButton.Label = "Analytics";
            this.AnalyticsButton.Name = "AnalyticsButton";
            this.AnalyticsButton.ShowImage = true;
            // 
            // HelpButton
            // 
            this.HelpButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.HelpButton.Image = global::ALPRibbon.Properties.Resources.PlaceHolder;
            this.HelpButton.Label = "Help";
            this.HelpButton.Name = "HelpButton";
            this.HelpButton.ShowImage = true;
            this.HelpButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.Help_Click);
            // 
            // ALPRibbon
            // 
            this.Name = "ALPRibbon";
            this.RibbonType = "Microsoft.PowerPoint.Presentation";
            this.Tabs.Add(this.ALPRibbonTab);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.ALPRibbon_Load);
            this.ALPRibbonTab.ResumeLayout(false);
            this.ALPRibbonTab.PerformLayout();
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

        internal Microsoft.Office.Tools.Ribbon.RibbonTab ALPRibbonTab;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup Account;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup Content;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup AddInteractivity;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup Sources;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton UploadButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton PublishButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton UpdateButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton MultipleChoiceButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton ImageQuizButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton FreeResponseButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton AnalyticsButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton HelpButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton SignInButton;
    }

    partial class ThisRibbonCollection
    {
        internal ALPRibbon ALPRibbon
        {
            get { return this.GetRibbon<ALPRibbon>(); }
        }
    }
}
