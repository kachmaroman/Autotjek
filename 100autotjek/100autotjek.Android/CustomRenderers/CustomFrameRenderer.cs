using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using _100autotjek.Controls;
using _100autotjek.Droid.CustomRenderers;

[assembly: ExportRenderer(typeof(CustomFrame), typeof(CustomFrameRenderer))]
namespace _100autotjek.Droid.CustomRenderers
{
    public class CustomFrameRenderer : VisualElementRenderer<Frame>
    {
        public CustomFrameRenderer()
        {
            SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.blue_rect));
        }
    }
}