using System;
using System.Drawing;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using _100autotjek.Controls;
using _100autotjek.iOS.CustomRenderers;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace _100autotjek.iOS.CustomRenderers
{
    public class CustomPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            var element = (CustomPicker)this.Element;

            if (Control != null && Element != null && !string.IsNullOrEmpty(element.Image))
            {
                var downArrow = UIImage.FromBundle(element.Image);

                var imageView = new UIImageView(downArrow)
                {
                    Frame = new RectangleF(-5, 0, (float) downArrow.Size.Width, (float) downArrow.Size.Height)
                };

                var rightView = new UIView(new System.Drawing.Rectangle(0, 0, (int)imageView.Frame.Width, (int)imageView.Frame.Height));
                rightView.AddSubview(imageView);

                Control.Layer.BorderWidth = 1;
                Control.Layer.CornerRadius = 0;
                Control.RightView = rightView;
                Control.RightViewMode = UITextFieldViewMode.Always;
                Control.Layer.BorderColor = UIColor.FromRGB(211, 211, 211).CGColor;
            }
        }
    }
}