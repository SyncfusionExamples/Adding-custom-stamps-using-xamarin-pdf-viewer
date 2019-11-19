using CustomStamps;
using CustomStamps.UWP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(SfFontButton), typeof(SfFontButtonRenderer))]
namespace CustomStamps.UWP
{
    public class SfFontButtonRenderer : ButtonRenderer
    {
        public Thickness Padding { get; set; }
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                var nativeButton = (Xamarin.Forms.Button)e.NewElement;
                Control.Padding = new Windows.UI.Xaml.Thickness(0, 0, 0, 0);
            }
        }

    }
}
