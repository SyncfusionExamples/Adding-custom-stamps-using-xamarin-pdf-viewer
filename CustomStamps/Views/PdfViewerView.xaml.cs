using CustomStamps;
using Syncfusion.SfPdfViewer.XForms;
using Syncfusion.XForms.Expander;
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

    public partial class PdfViewerView : ContentPage
    {
        private StampAnnotation selectedStampAnnotation;
        public PdfViewerView()
        {
            InitializeComponent();
            this.PdfViewerControl.Toolbar.Enabled = false;
            PopupLayout.StaysOpen = false;
            DeleteButton.IsVisible = false;
            PopupLayout.PopupView.ShowCloseButton = true;
            PopupLayout.PopupView.HeaderTitle = "Custom Stamps";
            PopupLayout.PopupView.ShowFooter = false;
            PopupLayout.PopupView.PopupStyle.HeaderFontAttribute = FontAttributes.Bold;
            PdfViewerControl.StampAnnotationSelected += PdfViewerControl_StampAnnotationSelected;
            PdfViewerControl.StampAnnotationDeselected += PdfViewerControl_StampAnnotationDeselected;
            PdfViewerControl.StampAnnotationRemoved += PdfViewerControl_StampAnnotationRemoved;
        }

        private void PdfViewerControl_StampAnnotationRemoved(object sender, StampAnnotationRemovedEventArgs e)
        {
            DeleteButton.IsVisible = false;
        }

        private void PdfViewerControl_StampAnnotationDeselected(object sender, StampAnnotationDeselectedEventArgs e)
        {
            DeleteButton.IsVisible = false;
            selectedStampAnnotation = null;
        }

        private void PdfViewerControl_StampAnnotationSelected(object sender, StampAnnotationSelectedEventArgs e)
        {
            selectedStampAnnotation = sender as StampAnnotation;
            DeleteButton.IsVisible = true;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            Stream savedStream = null;
            if (Device.RuntimePlatform == Device.UWP)
            {
                savedStream = await PdfViewerControl.SaveDocumentAsync();
            }
            else
            {
                savedStream = PdfViewerControl.SaveDocument();
            }
            string filePath = DependencyService.Get<ISave>().Save(savedStream as MemoryStream);
            string message = "The PDF has been saved to " + filePath;
            DependencyService.Get<IAlertView>().Show(message);
        }

        private void StampButton_Clicked(object sender, EventArgs e)
        {
            CustomStampsView_Mobile customStamp = new CustomStampsView_Mobile(PdfViewerControl, PopupLayout);
            PopupLayout.PopupView.ContentTemplate = new DataTemplate(() =>
            {

                return new ViewCell { View = customStamp };
            });
            if (Device.Idiom == TargetIdiom.Phone)
            {
                PopupLayout.PopupView.HeightRequest = this.Height - 50;
                PopupLayout.PopupView.WidthRequest = this.Width - 50;
                PopupLayout.Show();
            }
            if (Device.Idiom == TargetIdiom.Tablet)
            {
                PopupLayout.PopupView.HeightRequest = this.Height / 2;
                PopupLayout.PopupView.WidthRequest = this.Width / 1.5;
                PopupLayout.ShowOverlayAlways = true;
                PopupLayout.Show();

            }
            if (Device.Idiom == TargetIdiom.Desktop)
            {
                CustomStampsView_Desktop customStampsViewDesktop = new CustomStampsView_Desktop(PdfViewerControl, PopupLayout);
                PopupLayout.PopupView.ContentTemplate = new DataTemplate(() =>
                {

                    return new ViewCell { View = customStampsViewDesktop };
                });
                PopupLayout.PopupView.WidthRequest = 400;
                PopupLayout.PopupView.HeightRequest = 330;
                PopupLayout.PopupView.VerticalOptions = LayoutOptions.CenterAndExpand;
                PopupLayout.ShowOverlayAlways = true;
                PopupLayout.Show();
            }
        }

        private void Delete_Clicked(object sender, EventArgs e)
        {
            if (selectedStampAnnotation != null)
            {
                PdfViewerControl.RemoveAnnotation(selectedStampAnnotation);
            }
        }
    }
}