using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using ALPRibbon.Properties;
using Microsoft.Office.Tools.Ribbon;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

namespace ALPRibbon
{
    public partial class ALPRibbon
    {
        private void ALPRibbon_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void Help_Click(object sender, RibbonControlEventArgs e)
        {
            ALPAboutBox dlg = new ALPAboutBox();
            dlg.ShowDialog();
        }

        private void SignIn_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.RibbonAddIn.ALPLogInTaskPane.Visible = ((RibbonToggleButton)sender).Checked;
        }

        private void UploadButton_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.RibbonAddIn.ALPUploadTaskPane.Visible = ((RibbonToggleButton)sender).Checked;
        }

        private void PublishButton_Click(object sender, RibbonControlEventArgs e)
        {
            ALPPowerpointUtils.ExportLectureSlides();
            MessageBox.Show(Resources.Slides_Exported, Resources.Publish_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MultipleChoiceButton_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.RibbonAddIn.ALPMultipleChoiceTaskPane.Visible = ((RibbonToggleButton)sender).Checked;
/*
            if (Globals.Ribbons.ALPRibbon.MultipleChoiceButton.Checked)
            {
                PowerPoint.Application oApp = Globals.RibbonAddIn.Application;
                PowerPoint.Presentation oPres = oApp.ActivePresentation;
                PowerPoint.PpSlideLayout oLayout = PowerPoint.PpSlideLayout.ppLayoutBlank;
                PowerPoint.View oView = oApp.ActiveWindow.View;
                
                // Insert Slide after the current slide and select it
                PowerPoint.Slide oSlide = oPres.Slides.Add(RibbonAddIn.ALPCurrentSlide + 1, oLayout);
                oView.GotoSlide(oSlide.SlideIndex);

                // Display Question Title
                PowerPoint.PageSetup oPageSetup = oPres.PageSetup;
                float nSlideWidth = oPageSetup.SlideWidth;
                float nSlideHeight = oPageSetup.SlideHeight;
                PowerPoint.Shapes oShapes = oSlide.Shapes;
                PowerPoint.Shape oShapeArt = oShapes.AddTextEffect(Microsoft.Office.Core.MsoPresetTextEffect.msoTextEffect16, "Define Question", "Tahoma", 42, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoFalse, 100, 100);
                oShapeArt.Left = (nSlideWidth - oShapeArt.Width) / 2;
                oShapeArt.Top = (nSlideHeight - oShapeArt.Height) / 7;

                // Display bulleted answer list
                PowerPoint.Shape oShapeText = oShapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, 100, 100, 500, 500);
                PowerPoint.TextRange oTextRange = oShapeText.TextFrame.TextRange;
                oTextRange.Text = "Answer One\nAnswer Two\nAnswer Three\nAnswer Four";
                oTextRange.ParagraphFormat.Alignment = PowerPoint.PpParagraphAlignment.ppAlignLeft;
                PowerPoint.ParagraphFormat oParagraphFormat = oTextRange.ParagraphFormat;
                oParagraphFormat.SpaceWithin = 2;
                oParagraphFormat.Bullet.Type = PowerPoint.PpBulletType.ppBulletNumbered;
                oTextRange.Font.Name = "Tahoma";
                oTextRange.Font.Size = 24;
                oShapeText.Width = 8*(nSlideWidth / 10);
                oShapeText.Height = oShapeText.TextFrame.TextRange.BoundHeight;
                oShapeText.Left = nSlideWidth / 10;
                oShapeText.Top = 3*(nSlideHeight / 9);
            }
  */      }
    }
}
