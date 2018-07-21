using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using _100autotjek.iOS.CustomRenderers;

[assembly: ExportRenderer(typeof(Editor), typeof(CustomEditorRenderer))]
namespace _100autotjek.iOS.CustomRenderers
{
    public class CustomEditorRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BackgroundColor = UIColor.FromRGB(255, 255, 255);
                Control.Layer.BorderWidth = 1;
                Control.Layer.CornerRadius = 7;
                Control.ShowsVerticalScrollIndicator = false;
                Control.Layer.BorderColor = UIColor.FromRGB(211, 211, 211).CGColor; 
            }
        }
    }
}