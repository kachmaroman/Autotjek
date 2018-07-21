using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using _100autotjek.Controls;
using _100autotjek.iOS.CustomRenderers;

[assembly: ExportRenderer(typeof(CustomImageEntry), typeof(CustomImageEntryRenderer))]
namespace _100autotjek.iOS.CustomRenderers
{
    public class CustomImageEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || e.NewElement == null)
                return;

            var element = (CustomImageEntry)Element;

            if (!string.IsNullOrEmpty(element.Image))
            {
                switch (element.ImageAlignment)
                {
                    case ImageAlignment.Left:
                        Control.LeftViewMode = UITextFieldViewMode.Always;
                        Control.LeftView = GetImageView(element.Image, element.ImageHeight, element.ImageWidth);
                        break;
                    case ImageAlignment.Right:
                        Control.RightViewMode = UITextFieldViewMode.Always;
                        Control.RightView = GetImageView(element.Image, element.ImageHeight, element.ImageWidth);
                        break;
                }
            }
            
            Control.BorderStyle = UITextBorderStyle.RoundedRect;
            Control.Layer.BorderWidth = 2;
            Control.Layer.CornerRadius = 7;
            Control.Layer.BorderColor = element.LineColor.ToCGColor();
        }

        private UIView GetImageView(string imagePath, int height, int width)
        {
            var uiImageView = new UIImageView(UIImage.FromBundle(imagePath))
            {
                Frame = new RectangleF(5, 0, width, height)
            };
            UIView objLeftView = new UIView(new System.Drawing.Rectangle(0, 0, width + 3, height));
            
            objLeftView.AddSubview(uiImageView);

            return objLeftView;
        }
    }
}