using Android.Graphics.Drawables;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using _100autotjek.Droid.CustomRenderers;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]
namespace _100autotjek.Droid.CustomRenderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(Android.Graphics.Color.White);
                gd.SetCornerRadius(0);
                gd.SetStroke(2, Android.Graphics.Color.LightGray);
                Control.SetBackgroundDrawable(gd);
                Control.SetPadding(5, 12, 5, 12);
            }
        }
    }
}