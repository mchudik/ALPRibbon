using System;
using System.Collections.Generic;
using System.IO;
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
        public Microsoft.Office.Tools.CustomTaskPane ALPMultipleChoiceTaskPane
        {
            get
            {
                return ALPPaneMultipleChoiceTaskPane;
            }
        }
        public Microsoft.Office.Tools.CustomTaskPane ALPImageQuizTaskPane
        {
            get
            {
                return ALPPaneImageQuizTaskPane;
            }
        }


        // Custom Pane Controls
        private ALPPaneLogIn ALPPaneLogInControl;
        private Microsoft.Office.Tools.CustomTaskPane ALPPaneLogInTaskPane;
        private ALPPaneUpload ALPPaneUploadControl;
        private Microsoft.Office.Tools.CustomTaskPane ALPPaneUploadTaskPane;
        private ALPPaneMultipleChoice ALPPaneMultipleChoiceControl;
        private Microsoft.Office.Tools.CustomTaskPane ALPPaneMultipleChoiceTaskPane;
        private ALPPaneImageQuiz ALPPaneImageQuizControl;
        private Microsoft.Office.Tools.CustomTaskPane ALPPaneImageQuizTaskPane;

        // Event Handlers
        private void RibbonAddIn_Startup(object sender, System.EventArgs e)
        {
            // generate working directory
            WORKING_DIR = ALPGeneralUtils.GetTemporaryDirectory();

            // hook into powerpoint events
            this.Application.SlideSelectionChanged +=
                new PowerPoint.EApplication_SlideSelectionChangedEventHandler(Application_SlideSelectionChanged);

            // hook into slideshow events
            this.Application.SlideShowBegin +=
                new PowerPoint.EApplication_SlideShowBeginEventHandler(Application_SlideShowBegin);
            this.Application.SlideShowNextSlide +=
                new PowerPoint.EApplication_SlideShowNextSlideEventHandler(Application_SlideShowNextSlide);
            this.Application.SlideShowOnPrevious +=
                new PowerPoint.EApplication_SlideShowOnPreviousEventHandler(Application_SlideShowOnPrevious);
            this.Application.SlideShowEnd +=
                new PowerPoint.EApplication_SlideShowEndEventHandler(Application_SlideShowEnd);
            
            // LogIn Custom Pane
            ALPPaneLogInControl = new ALPPaneLogIn();
            ALPPaneLogInTaskPane = this.CustomTaskPanes.Add(ALPPaneLogInControl, "User Sign In");
            ALPPaneLogInTaskPane.VisibleChanged += new EventHandler(ALPPaneLogInTaskPane_VisibleChanged);
            // Set default for floating view    
            ALPPaneLogInTaskPane.DockPosition = Microsoft.Office.Core.MsoCTPDockPosition.msoCTPDockPositionFloating;
            ALPPaneLogInTaskPane.Width = 275;
            ALPPaneLogInTaskPane.Height = 550;
            // Set default for docked view    
            ALPPaneLogInTaskPane.DockPosition = Microsoft.Office.Core.MsoCTPDockPosition.msoCTPDockPositionRight;
            ALPPaneLogInTaskPane.Width = 275;
            // Set docking restrictions
            ALPPaneLogInTaskPane.DockPositionRestrict = Microsoft.Office.Core.MsoCTPDockPositionRestrict.msoCTPDockPositionRestrictNoHorizontal;

            // Upload Custom Pane
            ALPPaneUploadControl = new ALPPaneUpload();
            ALPPaneUploadTaskPane = this.CustomTaskPanes.Add(ALPPaneUploadControl, "Upload Presentation");
            ALPPaneUploadTaskPane.VisibleChanged += new EventHandler(ALPPaneUploadTaskPane_VisibleChanged);
            // Set default for floating view    
            ALPPaneUploadTaskPane.DockPosition = Microsoft.Office.Core.MsoCTPDockPosition.msoCTPDockPositionFloating;
            ALPPaneUploadTaskPane.Width = 450;
            ALPPaneUploadTaskPane.Height = 600;
            // Set default for docked view    
            ALPPaneUploadTaskPane.DockPosition = Microsoft.Office.Core.MsoCTPDockPosition.msoCTPDockPositionRight;
            ALPPaneUploadTaskPane.Width = 450;
            // Set docking restrictions
            ALPPaneUploadTaskPane.DockPositionRestrict = Microsoft.Office.Core.MsoCTPDockPositionRestrict.msoCTPDockPositionRestrictNoHorizontal;

            // MultipleChoice Custom Pane
            ALPPaneMultipleChoiceControl = new ALPPaneMultipleChoice();
            ALPPaneMultipleChoiceTaskPane = this.CustomTaskPanes.Add(ALPPaneMultipleChoiceControl, "Multiple Choice");
            ALPPaneMultipleChoiceTaskPane.VisibleChanged += new EventHandler(ALPPaneMultipleChoiceTaskPane_VisibleChanged);
            // Set default for floating view    
            ALPPaneMultipleChoiceTaskPane.DockPosition = Microsoft.Office.Core.MsoCTPDockPosition.msoCTPDockPositionFloating;
            ALPPaneMultipleChoiceTaskPane.Width = 400;
            ALPPaneMultipleChoiceTaskPane.Height = 600;
            // Set default for docked view    
            ALPPaneMultipleChoiceTaskPane.DockPosition = Microsoft.Office.Core.MsoCTPDockPosition.msoCTPDockPositionRight;
            ALPPaneMultipleChoiceTaskPane.Width = 400;
            // Set docking restrictions
            ALPPaneMultipleChoiceTaskPane.DockPositionRestrict = Microsoft.Office.Core.MsoCTPDockPositionRestrict.msoCTPDockPositionRestrictNoHorizontal;

            // ImageQuiz Custom Pane
            ALPPaneImageQuizControl = new ALPPaneImageQuiz();
            ALPPaneImageQuizTaskPane = this.CustomTaskPanes.Add(ALPPaneImageQuizControl, "Image Quiz");
            ALPPaneImageQuizTaskPane.VisibleChanged += new EventHandler(ALPPaneImageQuizTaskPane_VisibleChanged);
            // Set default for floating view    
            ALPPaneImageQuizTaskPane.DockPosition = Microsoft.Office.Core.MsoCTPDockPosition.msoCTPDockPositionFloating;
            ALPPaneImageQuizTaskPane.Width = 400;
            ALPPaneImageQuizTaskPane.Height = 600;
            // Set default for docked view    
            ALPPaneImageQuizTaskPane.DockPosition = Microsoft.Office.Core.MsoCTPDockPosition.msoCTPDockPositionRight;
            ALPPaneImageQuizTaskPane.Width = 400;
            // Set docking restrictions
            ALPPaneImageQuizTaskPane.DockPositionRestrict = Microsoft.Office.Core.MsoCTPDockPositionRestrict.msoCTPDockPositionRestrictNoHorizontal;

        }

        private void RibbonAddIn_Shutdown(object sender, System.EventArgs e)
        {
            Directory.Delete(RibbonAddIn.WORKING_DIR, true);
        }

        // powerpoint events
        private void Application_SlideSelectionChanged(PowerPoint.SlideRange SldRange)
        {
            _currentSlideNum = SldRange.SlideIndex;
            if (Globals.Ribbons.ALPRibbon.MultipleChoiceButton.Checked)
            {
                Globals.RibbonAddIn.ALPPaneMultipleChoiceControl.OnInitialize();
            }
            if (Globals.Ribbons.ALPRibbon.ImageQuizButton.Checked)
            {
                Globals.RibbonAddIn.ALPPaneImageQuizControl.OnInitialize();
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

        private void ALPPaneLogInTaskPane_VisibleChanged(object sender, System.EventArgs e)
        {
            Globals.Ribbons.ALPRibbon.SignInButton.Checked = ALPPaneLogInTaskPane.Visible;
        }

        private void ALPPaneUploadTaskPane_VisibleChanged(object sender, System.EventArgs e)
        {
            Globals.Ribbons.ALPRibbon.UploadButton.Checked = ALPPaneUploadTaskPane.Visible;
 /*
            if (ALPPaneUploadTaskPane.Visible == true)
            {
                var window = FindWindowW("MsoCommandBar", ALPPaneUploadTaskPane.Title); //MLHIDE
                if (window == null) return;
                MoveWindow(window, 600, 200, ALPPaneUploadTaskPane.Width, ALPPaneUploadTaskPane.Height, true);
            }
*/        }

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
