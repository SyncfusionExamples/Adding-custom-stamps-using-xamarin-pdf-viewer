using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CustomStamps
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //if(Device.Idiom==TargetIdiom.Phone && Device.Idiom==TargetIdiom.Tablet)
            {
                MainPage = new CustomStamps.PdfViewerView();
            }
           
             
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
