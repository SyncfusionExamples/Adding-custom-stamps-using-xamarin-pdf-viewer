using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using UIKit;

using Xamarin.Forms;
[assembly: Dependency(typeof(CustomStamps.iOS.AlertViewiOS))]
namespace CustomStamps.iOS
{
    class AlertViewiOS : IAlertView
    {
        public void Show(string message)
        {
            UIAlertView popUpView = new UIAlertView();
            popUpView.Message = message;
            popUpView.AddButton("OK");
            popUpView.Show();
        }

    }
}