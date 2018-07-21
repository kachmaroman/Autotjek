using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using _100autotjek.Droid.CustomRenderers;
using TimePicker = Xamarin.Forms.TimePicker;

[assembly: ExportRenderer(typeof(TimePicker), typeof(CustomTimePickerRenderer))]
namespace _100autotjek.Droid.CustomRenderers
{
    public class CustomTimePickerRenderer : TimePickerRenderer
    {
        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(global::Android.Graphics.Color.Transparent);
                //gd.SetStroke(2, global::Android.Graphics.Color.Gray);
                this.Control.SetBackgroundDrawable(gd);
            }
        }
    }
}