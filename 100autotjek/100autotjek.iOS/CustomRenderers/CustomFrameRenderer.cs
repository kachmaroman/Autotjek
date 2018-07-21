using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using _100autotjek.Controls;
using _100autotjek.iOS.CustomRenderers;

[assembly: ExportRenderer(typeof(CustomFrame), typeof(CustomFrameRenderer))]
namespace _100autotjek.iOS.CustomRenderers
{
    public sealed class CustomFrameRenderer : VisualElementRenderer<Frame>
    {
        public CustomFrameRenderer()
        {
            Layer.BorderColor = UIColor.FromRGB(41, 56, 98).CGColor;
            Layer.BorderWidth = 0.5f;
        }
    }
}
