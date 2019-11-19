using Syncfusion.XForms.PopupLayout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CustomStamps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomStampsView_Desktop : ContentView
    {
        Syncfusion.SfPdfViewer.XForms.SfPdfViewer pdfViewer;
        SfPopupLayout popupLayout;
        private string dynamicStampText;
        private TapGestureRecognizer recognizer;
        public CustomStampsView_Desktop(Syncfusion.SfPdfViewer.XForms.SfPdfViewer pdfViewer, SfPopupLayout popupLayout)
        {
            this.pdfViewer = pdfViewer;
            this.popupLayout = popupLayout;
            InitializeComponent();
            recognizer = new TapGestureRecognizer()
            {
                NumberOfTapsRequired = 1
            };
            recognizer.Tapped += (s, e) =>
            {
                if (s is Image)
                {
                    CreateAndAddCustomStamp(s as Image);
                }
                if (s is Grid)
                {
                    AddDynamicCustomStamp(s as Grid);
                }
            };
            Approved.GestureRecognizers.Add(recognizer);
            NotApproved.GestureRecognizers.Add(recognizer);
            Draft.GestureRecognizers.Add(recognizer);
            Confidential.GestureRecognizers.Add(recognizer);
            Expired.GestureRecognizers.Add(recognizer);
            DynamicExpired.GestureRecognizers.Add(recognizer);
            DynamicApproved.GestureRecognizers.Add(recognizer);
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Device.BeginInvokeOnMainThread(() => Text = "on " + DateTime.Now.ToString());
                return true;
            });
        }
        public string Text
        {
            set
            {
                dynamicStampText = value;
                DynamicApprovedLabel.Text = value;
                DynamicExpiredLabel.Text = value;
            }

        }
        private void CreateAndAddCustomStamp(Image selectedImage)
        {
            Image customImage = new Image();
            customImage.HeightRequest = 90;
            customImage.WidthRequest = 300;
            if (selectedImage == Approved)
                customImage.Source = "Approved.png";
            else if (selectedImage == NotApproved)
                customImage.Source = "NotApproved.png";
            else if (selectedImage == Draft)
                customImage.Source = "Draft.png";
            else if (selectedImage == Expired)
                customImage.Source = "Expired.png";
            else if (selectedImage == Confidential)
                customImage.Source = "Confidential.png";

            pdfViewer.AddStamp(customImage, pdfViewer.PageNumber, new Point(250, 400));
            popupLayout.IsOpen = false;

        }
        private void AddDynamicCustomStamp(Grid selectedStamp)
        {
            Grid grid = new Grid();
            grid.HeightRequest = 90;
            grid.WidthRequest = 300;
            Image customImage = new Image();
            Label label = new Label();
            label.VerticalOptions = LayoutOptions.StartAndExpand;
            label.HorizontalOptions = LayoutOptions.FillAndExpand;
            customImage.VerticalOptions = LayoutOptions.FillAndExpand;
            customImage.HorizontalOptions = LayoutOptions.FillAndExpand;
            label.VerticalTextAlignment = TextAlignment.End;
            label.HorizontalTextAlignment = TextAlignment.Center;
            label.HeightRequest = 80;
            label.WidthRequest = 300;
            customImage.HeightRequest = 90;
            customImage.WidthRequest = 300;
            if (selectedStamp == DynamicApproved)
            {
                customImage.Source = "DynamicApproved.png";
                label.Text = DynamicApprovedLabel.Text;
                label.FontSize = 24;
                label.Opacity = 125;
                label.TextColor = Color.ForestGreen;
                label.FontAttributes = FontAttributes.Italic;

            }
            else if (selectedStamp == DynamicExpired)
            {
                customImage.Source = "DynamicExpired.png";
                label.Text = DynamicExpiredLabel.Text;
                label.FontSize = 24;
                label.TextColor = Color.DarkBlue;
                label.FontAttributes = FontAttributes.Italic;
            }
            grid.Children.Add(customImage);
            grid.Children.Add(label);
            Grid.SetRow(customImage, 0);
            Grid.SetRow(label, 0);
            Grid.SetColumn(customImage, 0);
            Grid.SetRow(label, 0);
            pdfViewer.AddStamp(grid, pdfViewer.PageNumber, new Point(250, 400));
            popupLayout.IsOpen = false;
        }
    }
}