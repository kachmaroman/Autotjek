using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using _100autotjek.iOS.CustomRenderers;


[assembly: ExportRenderer(typeof(NavigationPage), typeof(CustomContentPageRenderer))]
namespace _100autotjek.iOS.CustomRenderers
{
    public class CustomContentPageRenderer : NavigationRenderer
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationBar.TopItem.TitleView = new UIImageView(UIImage.FromFile("AutotjekLogo.png"))
            {
                ContentMode = UIViewContentMode.Center
            };
        }
    }
}
