using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using _100autotjek.iOS.CustomRenderers;

[assembly: ExportRenderer(typeof(SearchBar), typeof(CustomSearchBarRenderer))]
namespace _100autotjek.iOS.CustomRenderers
{
    public class CustomSearchBarRenderer : SearchBarRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control != null)
            {
                Control.ShowsCancelButton = false;
                Control.BackgroundImage = new UIImage();

                UITextField txSearchField = (UITextField)Control.ValueForKey(new NSString("searchField"));
                txSearchField.BackgroundColor = UIColor.White;
                txSearchField.BorderStyle = UITextBorderStyle.Line;
                txSearchField.Layer.BorderWidth = 1.0f;
                txSearchField.Layer.CornerRadius = 0f;
                txSearchField.Layer.BorderColor = UIColor.LightGray.CGColor;
                txSearchField.LeftView = null;
                txSearchField.LeftViewMode = UITextFieldViewMode.Always;
            }
        }
    }
}