using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Tools = Microsoft.Office.Tools;
using Office = Microsoft.Office.Core;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ALPRibbon
{
    public partial class RibbonAddIn
    {
        // Working Directories
        public static string WORKING_DIR;
        public const string EXPORT_DIR = "export";
        public static string DESKTOP_DIR = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        // Presentation variables
        public bool bDebug = false;

        // PPT Document Window List
        private List<ALPCurrentWindow> ALPCurrentWindowList = new List<ALPCurrentWindow>();

        // Properties
        public static int ALPCurrentSlide
        {
            get
            {
                foreach (ALPCurrentWindow window in Globals.RibbonAddIn.ALPCurrentWindowList)
                {
                    if (window.currentWindow == Globals.RibbonAddIn.Application.ActiveWindow)
                    {
                        return window.currentSlideNum;
                    }
                }
                // Add new Window
                ALPCurrentWindow currentWindow = new ALPCurrentWindow();
                currentWindow.currentWindow = Globals.RibbonAddIn.Application.ActiveWindow;
                currentWindow.currentSlideNum = 0;
                Globals.RibbonAddIn.ALPCurrentWindowList.Add(currentWindow);
                return currentWindow.currentSlideNum = 0;
            }
            set
            {
                foreach (ALPCurrentWindow window in Globals.RibbonAddIn.ALPCurrentWindowList)
                {
                    if (window.currentWindow == Globals.RibbonAddIn.Application.ActiveWindow)
                    {
                        window.currentSlideNum = value;
                    }
                }
                // Add new Window
                ALPCurrentWindow currentWindow = new ALPCurrentWindow();
                currentWindow.currentWindow = Globals.RibbonAddIn.Application.ActiveWindow;
                currentWindow.currentSlideNum = value;
                Globals.RibbonAddIn.ALPCurrentWindowList.Add(currentWindow);
            }
        }

        // Custom Pane Lists
        public List<ALPPaneLogIn> ALPPaneLogInList = new List<ALPPaneLogIn>();
        public List<ALPPaneUpload> ALPPaneUploadList = new List<ALPPaneUpload>();
        public List<ALPPaneMultipleChoice> ALPPaneMultipleChoiceList = new List<ALPPaneMultipleChoice>();
        public List<ALPPaneImageQuiz> ALPPaneImageQuizList = new List<ALPPaneImageQuiz>();
        public List<ALPPaneFreeResponse> ALPPaneFreeResponseList = new List<ALPPaneFreeResponse>();

        // Event Handlers
        private void RibbonAddIn_Startup(object sender, System.EventArgs e)
        {
            // generate working directory
            WORKING_DIR = ALPGeneralUtils.GetTemporaryDirectory();

            // hook into powerpoint events
            this.Application.SlideSelectionChanged +=
                new PowerPoint.EApplication_SlideSelectionChangedEventHandler(Application_SlideSelectionChanged);
            this.Application.AfterNewPresentation +=
                new PowerPoint.EApplication_AfterNewPresentationEventHandler(Application_PresentationNew);
            this.Application.PresentationOpen +=
                new PowerPoint.EApplication_PresentationOpenEventHandler(Application_PresentationOpen);
            this.Application.PresentationClose +=
                new PowerPoint.EApplication_PresentationCloseEventHandler(Application_PresentationClose);
            this.Application.WindowActivate +=
                new PowerPoint.EApplication_WindowActivateEventHandler(Application_WindowActivate);

            // hook into slideshow events
            this.Application.SlideShowBegin +=
                new PowerPoint.EApplication_SlideShowBeginEventHandler(Application_SlideShowBegin);
            this.Application.SlideShowNextSlide +=
                new PowerPoint.EApplication_SlideShowNextSlideEventHandler(Application_SlideShowNextSlide);
            this.Application.SlideShowOnPrevious +=
                new PowerPoint.EApplication_SlideShowOnPreviousEventHandler(Application_SlideShowOnPrevious);
            this.Application.SlideShowEnd +=
                new PowerPoint.EApplication_SlideShowEndEventHandler(Application_SlideShowEnd);
        }

        private void RibbonAddIn_Shutdown(object sender, System.EventArgs e)
        {
            if(Directory.Exists(RibbonAddIn.WORKING_DIR))
                Directory.Delete(RibbonAddIn.WORKING_DIR, true);
        }

        // PowerPoint events
        private void Application_PresentationNew(PowerPoint.Presentation Pres)
        {
            ALPCurrentSlide = 1;
            TurnOffButtons(Pres);
        }

        private void Application_PresentationOpen(PowerPoint.Presentation Pres)
        {
            ALPCurrentSlide = 1;
            TurnOffButtons(Pres);
        }

        private void Application_PresentationClose(PowerPoint.Presentation Pres)
        {
            DeleteCustomPanes(Pres);
        }

        private void Application_SlideSelectionChanged(PowerPoint.SlideRange SldRange)
        {
            // Set slide number in the current window
            ALPCurrentSlide = SldRange.SlideIndex;

            // Reflect change in all custom panes of current window
            if (Globals.Ribbons.ALPRibbon.SignInButton.Checked)
            {
                foreach (ALPPaneLogIn pane in Globals.RibbonAddIn.ALPPaneLogInList)
                {
                    if (pane.DocWindow == Globals.RibbonAddIn.Application.ActiveWindow)
                    {
                        pane.InitVariables();
                    }
                }
            }
            if (Globals.Ribbons.ALPRibbon.UploadButton.Checked)
            {
                foreach (ALPPaneUpload pane in Globals.RibbonAddIn.ALPPaneUploadList)
                {
                    if (pane.DocWindow == Globals.RibbonAddIn.Application.ActiveWindow)
                    {
                        pane.InitVariables();
                    }
                }
            }
            if (Globals.Ribbons.ALPRibbon.MultipleChoiceButton.Checked)
            {
                foreach (ALPPaneMultipleChoice pane in Globals.RibbonAddIn.ALPPaneMultipleChoiceList)
                {
                    if (pane.DocWindow == Globals.RibbonAddIn.Application.ActiveWindow)
                    {
                        pane.InitVariables();
                        pane.TaskPane.Visible = ALPPowerpointUtils.IsPlaceholderSlide("Multiple_Choice");
                    }
                }
            }
            if (Globals.Ribbons.ALPRibbon.ImageQuizButton.Checked)
            {
                foreach (ALPPaneImageQuiz pane in Globals.RibbonAddIn.ALPPaneImageQuizList)
                {
                    if (pane.DocWindow == Globals.RibbonAddIn.Application.ActiveWindow)
                    {
                        pane.InitVariables();
                        pane.TaskPane.Visible = ALPPowerpointUtils.IsPlaceholderSlide("Image_Quiz");
                    }
                }
            }
            if (Globals.Ribbons.ALPRibbon.FreeResponseButton.Checked)
            {
                foreach (ALPPaneFreeResponse pane in Globals.RibbonAddIn.ALPPaneFreeResponseList)
                {
                    if (pane.DocWindow == Globals.RibbonAddIn.Application.ActiveWindow)
                    {
                        pane.InitVariables();
                        pane.TaskPane.Visible = ALPPowerpointUtils.IsPlaceholderSlide("Free_Response");
                    }
                }
            }
        }

        private void Application_WindowActivate(PowerPoint.Presentation Pres, PowerPoint.DocumentWindow Wn)
        {
            // Start with Off setting as Custom panes might not exist
            TurnOffButtons(Pres);

            foreach (ALPPaneLogIn pane in Globals.RibbonAddIn.ALPPaneLogInList) {
                if (pane.DocWindow == Wn) {
                    Globals.Ribbons.ALPRibbon.SignInButton.Checked = pane.TaskPane.Visible;
                    break;
                }
            }
            foreach (ALPPaneUpload pane in Globals.RibbonAddIn.ALPPaneUploadList) {
                if (pane.DocWindow == Wn) {
                    Globals.Ribbons.ALPRibbon.UploadButton.Checked = pane.TaskPane.Visible;
                    break;
                }
            }
            foreach (ALPPaneMultipleChoice pane in Globals.RibbonAddIn.ALPPaneMultipleChoiceList) {
                if (pane.DocWindow == Wn) {
                    Globals.Ribbons.ALPRibbon.MultipleChoiceButton.Checked = pane.TaskPane.Visible;
                    break;
                }
            }
            foreach (ALPPaneImageQuiz pane in Globals.RibbonAddIn.ALPPaneImageQuizList) {
                if (pane.DocWindow == Wn) {
                    Globals.Ribbons.ALPRibbon.ImageQuizButton.Checked = pane.TaskPane.Visible;
                    break;
                }
            }
            foreach (ALPPaneFreeResponse pane in Globals.RibbonAddIn.ALPPaneFreeResponseList) {
                if (pane.DocWindow == Wn) {
                    Globals.Ribbons.ALPRibbon.FreeResponseButton.Checked = pane.TaskPane.Visible;
                    break;
                }
            }
        }

        // SlideShow events
        void Application_SlideShowBegin(PowerPoint.SlideShowWindow wnd)
        {
        }

        void Application_SlideShowNextSlide(PowerPoint.SlideShowWindow wnd)
        {
            // Remove Temporary pictures
            foreach (PowerPoint.Shape shape in wnd.View.Slide.Shapes)
            {
                if (shape.AlternativeText.Equals("Temporary"))
                {
                    shape.Delete();
                }
            }

            // Create picture out of the URL and add it to the slide
            if (ALPPowerpointUtils.GetSlideNotesText(wnd.View.Slide).Contains("http"))
            {
                WebsiteToImage websiteToImage = new WebsiteToImage(ALPPowerpointUtils.GetSlideNotesText(wnd.View.Slide), @RibbonAddIn.WORKING_DIR + "\\" + RibbonAddIn.EXPORT_DIR + "\\PageHtml.jpg");
                websiteToImage.Generate();
                PowerPoint.Shape oShape = wnd.View.Slide.Shapes.AddPicture(@RibbonAddIn.WORKING_DIR + "\\" + RibbonAddIn.EXPORT_DIR + "\\PageHtml.jpg", Microsoft.Office.Core.MsoTriState.msoTrue, Microsoft.Office.Core.MsoTriState.msoFalse, 0, 0, wnd.View.Slide.Master.Width, wnd.View.Slide.Master.Height);
                oShape.AlternativeText = "Temporary";
            }
        }

        void Application_SlideShowOnPrevious(PowerPoint.SlideShowWindow wnd)
        {
        }

        void Application_SlideShowEnd(PowerPoint.Presentation pres)
        {
            PowerPoint.Application oApp = Globals.RibbonAddIn.Application;
            PowerPoint.Presentation oPres = oApp.ActivePresentation;
            for (int i = 1; i < oPres.Slides.Count + 1; i++)
            {
                PowerPoint.Slide currentSlide = oPres.Slides[i];
                foreach (PowerPoint.Shape shape in currentSlide.Shapes)
                {
                    if (shape.AlternativeText.Equals("Temporary"))
                    {
                        shape.Delete();
                    }
                }
            }
            // clean  the export directory
            ALPGeneralUtils.ClearDirectory(RibbonAddIn.EXPORT_DIR);
        }

        // Methods
        private void TurnOffButtons(PowerPoint.Presentation Pres)
        {
            if (Pres.Windows.Count > 0)
            {
                Globals.Ribbons.ALPRibbon.SignInButton.Checked = false;
                Globals.Ribbons.ALPRibbon.UploadButton.Checked = false;
                Globals.Ribbons.ALPRibbon.MultipleChoiceButton.Checked = false;
                Globals.Ribbons.ALPRibbon.ImageQuizButton.Checked = false;
                Globals.Ribbons.ALPRibbon.FreeResponseButton.Checked = false;
            }
        }

        private void DeleteCustomPanes(PowerPoint.Presentation Pres)
        {
            // If opening from template document might not exist
            if (Pres.Windows.Count == 0)
                return;

            foreach (ALPPaneLogIn pane in Globals.RibbonAddIn.ALPPaneLogInList)
            {
                if (pane.DocWindow == Globals.RibbonAddIn.Application.ActiveWindow)
                {
                    pane.ALPPaneDelete();
                    Globals.Ribbons.ALPRibbon.SignInButton.Checked = false;
                    break;
                }
            }
            foreach (ALPPaneUpload pane in Globals.RibbonAddIn.ALPPaneUploadList)
            {
                if (pane.DocWindow == Globals.RibbonAddIn.Application.ActiveWindow)
                {
                    pane.ALPPaneDelete();
                    Globals.Ribbons.ALPRibbon.UploadButton.Checked = false;
                    break;
                }
            }
            foreach (ALPPaneMultipleChoice pane in Globals.RibbonAddIn.ALPPaneMultipleChoiceList)
            {
                if (pane.DocWindow == Globals.RibbonAddIn.Application.ActiveWindow)
                {
                    pane.ALPPaneDelete();
                    Globals.Ribbons.ALPRibbon.MultipleChoiceButton.Checked = false;
                    break;
                }
            }
            foreach (ALPPaneImageQuiz pane in Globals.RibbonAddIn.ALPPaneImageQuizList)
            {
                if (pane.DocWindow == Globals.RibbonAddIn.Application.ActiveWindow)
                {
                    pane.ALPPaneDelete();
                    Globals.Ribbons.ALPRibbon.ImageQuizButton.Checked = false;
                    break;
                }
            }
            foreach (ALPPaneFreeResponse pane in Globals.RibbonAddIn.ALPPaneFreeResponseList)
            {
                if (pane.DocWindow == Globals.RibbonAddIn.Application.ActiveWindow)
                {
                    pane.ALPPaneDelete();
                    Globals.Ribbons.ALPRibbon.FreeResponseButton.Checked = false;
                    break;
                }
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
            this.Startup += new System.EventHandler(RibbonAddIn_Startup);
            this.Shutdown += new System.EventHandler(RibbonAddIn_Shutdown);
        }
        
        #endregion
    }
}
