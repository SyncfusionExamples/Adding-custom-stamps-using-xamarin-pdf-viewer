using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: Dependency(typeof(CustomStamps.iOS.CustomViewiOS))]
namespace CustomStamps.iOS
{
   public class CustomViewiOS:CustomImage
    {
        private Stream imgStream;

        public void ConvertToImage(Grid view)
        {
            var renderer = Platform.CreateRenderer(view);
            var size = view.Measure(double.PositiveInfinity, double.PositiveInfinity);
            renderer.NativeView.Frame = new CGRect(view.X, view.Y, size.Request.Width, size.Request.Height);
            renderer.NativeView.AutoresizingMask = UIViewAutoresizing.All;
            renderer.NativeView.ContentMode = UIViewContentMode.ScaleToFill;
            renderer.Element.Layout(new Rectangle(view.X, view.Y, size.Request.Width, size.Request.Height));
            var nativeView = renderer.NativeView;
            var image= ConvertToImage(nativeView);
             imgStream = image.AsJPEG().AsStream();
        }
        private UIImage ConvertToImage(UIView view)
        {
            UIGraphics.BeginImageContextWithOptions(view.Bounds.Size, true, 0);
            view.Layer.RenderInContext(UIGraphics.GetCurrentContext());
            var img = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return img;

        }
        public Stream GetStream()
        {
            return imgStream;
        }
        
    }
}