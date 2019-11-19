using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Dependency(typeof(CustomStamps.Droid.CustomImageAndroid))]
namespace CustomStamps.Droid
{
  public class CustomImageAndroid:CustomImage
    {
      static  Context context= Android.App.Application.Context;
        private Stream stream;

        internal static global::Android.Views.View GetNativeView(Xamarin.Forms.View formsView, VisualElement parent,
        object bindingContext, Context androidContext, bool canMeasureAndLayout = true)
        {
            if (formsView == null)
                return null;
            formsView.Parent = parent;
            var size = formsView.Measure(double.PositiveInfinity, double.PositiveInfinity);
            formsView.Layout(new Rectangle(0, 0, size.Request.Width, size.Request.Height));
            var renderer = Platform.CreateRendererWithContext(formsView, context);
            renderer.Tracker.UpdateLayout();
            renderer.Element.Layout(new Rectangle(0, 0, size.Request.Width, size.Request.Height));
            Platform.SetRenderer(formsView, renderer);
            BindableObject.SetInheritedBindingContext(formsView, bindingContext);
            return renderer as global::Android.Views.View;
        }

        public Stream GetImageStream(Android.Views.View view)
        {
            MemoryStream ViewStream = new MemoryStream();
            view.SetBackgroundColor(Android.Graphics.Color.Transparent);
            Bitmap bitmapImage = GetBitmapFromView(view);
            bitmapImage.Compress(Bitmap.CompressFormat.Png, 50, ViewStream);
            return ViewStream;
        }

        private Bitmap GetBitmapFromView(Android.Views.View view)
        {
            Bitmap returnedBitmap = null;

              if (view.Width > 0 && view.Height > 0)
                returnedBitmap = Bitmap.CreateBitmap(view.Width, view.Height, Bitmap.Config.Argb8888);
            else
                returnedBitmap = Bitmap.CreateBitmap(200, 200, Bitmap.Config.Argb8888);

            int[] allpixels = new int[returnedBitmap.Height * returnedBitmap.Width];
            returnedBitmap.GetPixels(allpixels, 0, returnedBitmap.Width, 0, 0, returnedBitmap.Width, returnedBitmap.Height);
            returnedBitmap.SetPixels(allpixels, 0, returnedBitmap.Width, 0, 0, returnedBitmap.Width, returnedBitmap.Height);
            Canvas canvas = new Canvas(returnedBitmap);
              Drawable bgDrawable = view.Background;
            if (bgDrawable != null)
                   bgDrawable.Draw(canvas);
            else
                  canvas.DrawColor(Android.Graphics.Color.White);
                  view.Draw(canvas);            
            return returnedBitmap;
        }

        public void ConvertToImage(Grid view)
        {
            Android.Views.View AndroidView = GetNativeView(view, null, view.BindingContext, null, true);
            stream = GetImageStream(AndroidView);
        }

        public Stream GetStream()
        {
            return stream;
        }
    }
}