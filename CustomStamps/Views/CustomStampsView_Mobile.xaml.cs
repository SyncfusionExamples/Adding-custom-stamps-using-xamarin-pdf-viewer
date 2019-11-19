using Syncfusion.XForms.Expander;
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
    public partial class CustomStampsView_Mobile : ContentView
    {
        Syncfusion.SfPdfViewer.XForms.SfPdfViewer pdfViewer;
        SfPopupLayout popupLayout;
        private TapGestureRecognizer recognizer;
        private string dynamicStampText;
        private Dictionary<int, Element> stampcollection = new Dictionary<int, Element>();

        public CustomStampsView_Mobile(Syncfusion.SfPdfViewer.XForms.SfPdfViewer pdfViewer, SfPopupLayout popupLayout)
        {
            this.pdfViewer = pdfViewer;
            this.popupLayout = popupLayout;
            InitializeComponent();
            recognizer = new TapGestureRecognizer() { NumberOfTapsRequired = 1 };
            recognizer.Tapped += (s, e) =>
            {
                if (s is Image)
                {
                    CreateAndAddCustomStamp(s as Image);
                }
                else if (s is Grid)
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
                Device.BeginInvokeOnMainThread(() => DynamicStampText = "on " + DateTime.Now.ToString());
                return true;
            });

        }

        public string DynamicStampText
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
            DependencyService.Get<CustomImage>().ConvertToImage(selectedStamp as Grid);
            Stream stream = DependencyService.Get<CustomImage>().GetStream();
            stream.Position = 0;
            Image customImage = new Image();
            customImage.Source = ImageSource.FromStream(() => stream);
            customImage.HeightRequest = 90;
            customImage.WidthRequest = 300;
            pdfViewer.AddStamp(customImage, pdfViewer.PageNumber, new Point(250, 400));
            popupLayout.IsOpen = false;
        }

    }
}