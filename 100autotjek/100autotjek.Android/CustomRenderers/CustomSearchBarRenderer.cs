using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using _100autotjek.Droid.CustomRenderers;
using System.ComponentModel;
using Android.Content.Res;
using Android.Graphics.Drawables;

[assembly: ExportRenderer(typeof(SearchBar), typeof(CustomSearchBarRenderer))]
namespace _100autotjek.Droid.CustomRenderers
{
    public class CustomSearchBarRenderer : SearchBarRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control != null)
            {
                var searchView = Control as SearchView;
                var gradient = new GradientDrawable();
                gradient.SetColor(Android.Graphics.Color.White);

                int searchIconId = Context.Resources.GetIdentifier("android:id/search_mag_icon", null, null);
                var icon = searchView.FindViewById(searchIconId);
                icon.LayoutParameters = new LinearLayout.LayoutParams(0, 0);

                int searchPlateId = searchView.Context.Resources.GetIdentifier("android:id/search_plate", null, null);
                Android.Views.View searchPlateView = searchView.FindViewById(searchPlateId);
                searchPlateView.SetBackground(gradient);
            }
        }
    }
}