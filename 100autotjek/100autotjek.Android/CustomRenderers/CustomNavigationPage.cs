using System.Linq;
using Android.App;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;
using _100autotjek.Droid.CustomRenderers;
using Application = Xamarin.Forms.Application;
using AToolbar = Android.Support.V7.Widget.Toolbar;
using _100autotjek.Views;
using _100autotjek.Views.MainMenu;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(CustomNavigationPage))]
namespace _100autotjek.Droid.CustomRenderers
{
#pragma warning disable CS0618 // Type or member is obsolete
    public class CustomNavigationPage : NavigationPageRenderer
    {
        private AToolbar toolbar;
        private Activity context;

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            context = (Activity)Xamarin.Forms.Forms.Context;
            toolbar = context.FindViewById<Android.Support.V7.Widget.Toolbar>(Droid.Resource.Id.toolbar);

            var page = Application.Current?.MainPage?.Navigation?.NavigationStack?.Last();

            if (!(page is HomePage))
            {
                toolbar.SetPadding(0, 0, 0, 0);
                toolbar.Logo = null;
                toolbar.SetBackgroundColor(Android.Graphics.Color.ParseColor("#293862"));

                if (toolbar.NavigationIcon != null)
                    toolbar.SetNavigationIcon(Resource.Drawable.BackIcon);
            }
            else
            {
                toolbar.SetLogo(Resource.Drawable.AutotjekLogo);
                var paddingLeft = (toolbar.Width - toolbar.Logo.MinimumWidth) / 2;
                toolbar.SetPadding(paddingLeft, 0, 0, 0);
            }
        }
    }
#pragma warning restore CS0618 // Type or member is obsolete
}