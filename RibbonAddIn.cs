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
        private static int _currentSlideNum = 0;
        public bool bDebug = false;

        // Properties
        public static int ALPCurrentSlide
        {
            get
            {
                return _currentSlideNum;
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
            this.Application.PresentationCloseFinal +=
                new PowerPoint.EApplication_PresentationCloseFinalEventHandler(Application_PresentationClose);
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
            Directory.Delete(RibbonAddIn.WORKING_DIR, true);
        }

        private void CreateCustomPanes(PowerPoint.Presentation Pres)
        {
            // If opening from template document might not exist
            if (Pres.Windows.Count == 0)
                return;

            // LogIn Custom Pane
            ALPPaneLogIn ALPPaneLogInControl = new ALPPaneLogIn("User Sign In", Globals.RibbonAddIn.Application.ActiveWindow);
            ALPPaneLogInControl.ALPPaneConfigure(275, 550, 275);
            // Upload Custom Pane
            ALPPaneUpload ALPPaneUploadControl = new ALPPaneUpload("Upload Presentation", Globals.RibbonAddIn.Application.ActiveWindow);
            ALPPaneUploadControl.ALPPaneConfigure(450, 600, 450);
            // MultipleChoice Custom Pane
            ALPPaneMultipleChoice ALPPaneMultipleChoiceControl = new ALPPaneMultipleChoice("Multiple Choice", Globals.RibbonAddIn.Application.ActiveWindow);
            ALPPaneMultipleChoiceControl.ALPPaneConfigure(500, 600, 300);
            // ImageQuiz Custom Pane
            ALPPaneImageQuiz ALPPaneImageQuizControl = new ALPPaneImageQuiz("Image Quiz", Globals.RibbonAddIn.Application.ActiveWindow);
            ALPPaneImageQuizControl.ALPPaneConfigure(700, 900, 300);
            // FreeResponse Custom Pane
            ALPPaneFreeResponse ALPPaneFreeResponseControl = new ALPPaneFreeResponse("Free Response", Globals.RibbonAddIn.Application.ActiveWindow);
            ALPPaneFreeResponseControl.ALPPaneConfigure(500, 600, 300);
        }

        // powerpoint events
        private void Application_SlideSelectionChanged(PowerPoint.SlideRange SldRange)
        {
            _currentSlideNum = SldRange.SlideIndex;
            if (Globals.Ribbons.ALPRibbon.MultipleChoiceButton.Checked)
            {
                foreach (ALPPaneMultipleChoice pane in Globals.RibbonAddIn.ALPPaneMultipleChoiceList)
                {
                    if (pane.DocWindow == Globals.RibbonAddIn.Application.ActiveWindow)
                    {
                        pane.OnInitialize();
                    }
                }
            }
            if (Globals.Ribbons.ALPRibbon.ImageQuizButton.Checked)
            {
                foreach (ALPPaneImageQuiz pane in Globals.RibbonAddIn.ALPPaneImageQuizList)
                {
                    if (pane.DocWindow == Globals.RibbonAddIn.Application.ActiveWindow)
                    {
                        pane.OnInitialize();
                    }
                }
            }
            if (Globals.Ribbons.ALPRibbon.FreeResponseButton.Checked)
            {
                foreach (ALPPaneFreeResponse pane in Globals.RibbonAddIn.ALPPaneFreeResponseList)
                {
                    if (pane.DocWindow == Globals.RibbonAddIn.Application.ActiveWindow)
                    {
                        pane.OnInitialize();
                    }
                }
            }
        }
        private void Application_PresentationNew(PowerPoint.Presentation Pres)
        {
            _currentSlideNum = 0;
            CreateCustomPanes(Pres);
        }

        private void Application_PresentationOpen(PowerPoint.Presentation Pres)
        {
            _currentSlideNum = 0;
            CreateCustomPanes(Pres);
        }

        private void Application_PresentationClose(PowerPoint.Presentation Pres)
        {
            _currentSlideNum = 0;
        }

        private void Application_WindowActivate(PowerPoint.Presentation Pres, PowerPoint.DocumentWindow Wn)
        {
            foreach (ALPPaneLogIn pane in Globals.RibbonAddIn.ALPPaneLogInList) {
                if (pane.DocWindow == Wn) {
                    Globals.Ribbons.ALPRibbon.SignInButton.Checked = pane.TaskPane.Visible;
                    break;
                }
            }
            foreach (ALPPaneUpload pane in Globals.RibbonAddIn.ALPPaneUploadList)
            {
                if (pane.DocWindow == Wn)
                {
                    Globals.Ribbons.ALPRibbon.UploadButton.Checked = pane.TaskPane.Visible;
                    break;
                }
            }
            foreach (ALPPaneMultipleChoice pane in Globals.RibbonAddIn.ALPPaneMultipleChoiceList)
            {
                if (pane.DocWindow == Wn)
                {
                    Globals.Ribbons.ALPRibbon.MultipleChoiceButton.Checked = pane.TaskPane.Visible;
                    break;
                }
            }
            foreach (ALPPaneImageQuiz pane in Globals.RibbonAddIn.ALPPaneImageQuizList)
            {
                if (pane.DocWindow == Wn)
                {
                    Globals.Ribbons.ALPRibbon.ImageQuizButton.Checked = pane.TaskPane.Visible;
                    break;
                }
            }
            foreach (ALPPaneFreeResponse pane in Globals.RibbonAddIn.ALPPaneFreeResponseList)
            {
                if (pane.DocWindow == Wn)
                {
                    Globals.Ribbons.ALPRibbon.FreeResponseButton.Checked = pane.TaskPane.Visible;
                    break;
                }
            }
        }

        // slideshow events
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
/*
        private void ALPPaneLogInTaskPane_VisibleChanged(object sender, System.EventArgs e)
        {
            Globals.Ribbons.ALPRibbon.SignInButton.Checked = ALPPaneLogInTaskPane.Visible;
        }

        private void ALPPaneUploadTaskPane_VisibleChanged(object sender, System.EventArgs e)
        {
            Globals.Ribbons.ALPRibbon.UploadButton.Checked = ALPPaneUploadTaskPane.Visible;
        }

        private void ALPPaneMultipleChoiceTaskPane_VisibleChanged(object sender, System.EventArgs e)
        {
            Globals.Ribbons.ALPRibbon.MultipleChoiceButton.Checked = ALPPaneMultipleChoiceTaskPane.Visible;
            if (Globals.Ribbons.ALPRibbon.MultipleChoiceButton.Checked)
            {
                Globals.RibbonAddIn.ALPPaneMultipleChoiceControl.OnInitialize();
            }
            else
            {
                Globals.RibbonAddIn.ALPPaneMultipleChoiceControl.OnExit();
            }
        }

        private void ALPPaneImageQuizTaskPane_VisibleChanged(object sender, System.EventArgs e)
        {
            Globals.Ribbons.ALPRibbon.ImageQuizButton.Checked = ALPPaneImageQuizTaskPane.Visible;
            if (Globals.Ribbons.ALPRibbon.ImageQuizButton.Checked)
            {
                Globals.RibbonAddIn.ALPPaneImageQuizControl.OnInitialize();
            }
            else
            {
                Globals.RibbonAddIn.ALPPaneImageQuizControl.OnExit();
            }
        }

        private void ALPPaneFreeResponseTaskPane_VisibleChanged(object sender, System.EventArgs e)
        {
            Globals.Ribbons.ALPRibbon.FreeResponseButton.Checked = ALPPaneFreeResponseTaskPane.Visible;
            if (Globals.Ribbons.ALPRibbon.FreeResponseButton.Checked)
            {
                Globals.RibbonAddIn.ALPPaneFreeResponseControl.OnInitialize();
            }
            else
            {
                Globals.RibbonAddIn.ALPPaneFreeResponseControl.OnExit();
            }
        }
*/
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
