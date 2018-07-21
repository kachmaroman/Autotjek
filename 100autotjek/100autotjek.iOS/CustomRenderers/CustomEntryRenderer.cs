using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using _100autotjek.iOS.CustomRenderers;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]
namespace _100autotjek.iOS.CustomRenderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BackgroundColor = UIColor.FromRGB(255, 255, 255);
                Control.BorderStyle = UITextBorderStyle.RoundedRect;
                Control.Layer.BorderWidth = 1;
                Control.Layer.CornerRadius = 0;
                Control.Layer.BorderColor = UIColor.FromRGB(211, 211, 211).CGColor;    
            }
        }
    }
}
