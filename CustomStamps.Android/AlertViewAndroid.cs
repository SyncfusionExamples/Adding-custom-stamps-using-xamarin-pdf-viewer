using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;

[assembly: Dependency(typeof(CustomStamps.Droid.AlertViewAndroid))]
namespace CustomStamps.Droid
{
    public class AlertViewAndroid : IAlertView
    {
        public void Show(string message)
        {
            Xamarin.Forms.Application.Current.MainPage.DisplayAlert("", message, "OK");
        }
    }
}
