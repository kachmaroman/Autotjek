using System;
using System.Linq;
using Xamarin.Forms;

namespace _100autotjek.Controls
{
    public class CustomStepProgressBar : StackLayout
    {
        Button _lastStepSelected;
        public static readonly BindableProperty StepsProperty = BindableProperty.Create(nameof(Steps), typeof(int), typeof(CustomStepProgressBar), 0);
        public static readonly BindableProperty StepSelectedProperty = BindableProperty.Create(nameof(StepSelected), typeof(int), typeof(CustomStepProgressBar), 0, BindingMode.TwoWay);
        public static readonly BindableProperty StepColorProperty = BindableProperty.Create(nameof(StepColor), typeof(Color), typeof(CustomStepProgressBar), Color.Black, BindingMode.TwoWay);

        public Color StepColor
        {
            get => (Color)GetValue(StepColorProperty);
            set => SetValue(StepColorProperty, value);
        }

        public int Steps
        {
            get => (int)GetValue(StepsProperty);
            set => SetValue(StepsProperty, value);
        }

        public int StepSelected
        {
            get => (int)GetValue(StepSelectedProperty);
            set => SetValue(StepSelectedProperty, value);
        }

        public CustomStepProgressBar()
        {
            Orientation = StackOrientation.Horizontal;
            HorizontalOptions = LayoutOptions.CenterAndExpand;
            Padding = new Thickness(10, 5);
            Spacing = 2;
            AddStyles();
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == StepsProperty.PropertyName)
            {
                for (int i = 0; i < Steps; i++)
                {
                    var button = new Button
                    {
                        ClassId = $"{i + 1}",
                        Style = Resources["unSelectedStyle"] as Style
                    };

                    this.Children.Add(button);
                }   
            }
            else if (propertyName == StepSelectedProperty.PropertyName)
            {
                var children = this.Children.First(p => (!string.IsNullOrEmpty(p.ClassId) && Convert.ToInt32(p.ClassId) == StepSelected));
                if (children != null) SelectElement(children as Button);

            }
            else if (propertyName == StepColorProperty.PropertyName)
            {
                AddStyles();
            }
        }

        private void SelectElement(Button elementSelected)
        {
            if (_lastStepSelected != null) _lastStepSelected.Style = Resources["unSelectedStyle"] as Style;

            elementSelected.Style = Resources["selectedStyle"] as Style;

            _lastStepSelected = elementSelected;
        }

        private void AddStyles()
        {
            var width = 15;
            var height = 15;

            var cornerRadius = 8;

            var unSelectedStyle = new Style(typeof(Button))
            {
                Setters =
                {
                    new Setter {Property = BackgroundColorProperty, Value = Color.Transparent},
                    new Setter {Property = Button.BorderColorProperty, Value = StepColor},
                    new Setter {Property = Button.BorderWidthProperty, Value = 0.5},
                    new Setter {Property = Button.CornerRadiusProperty, Value = cornerRadius},
                    new Setter {Property = HeightRequestProperty, Value = height},
                    new Setter {Property = WidthRequestProperty, Value = width},
                }
            };

            var selectedStyle = new Style(typeof(Button))
            {
                Setters =
                {
                    new Setter {Property = BackgroundColorProperty, Value = StepColor},
                    new Setter {Property = Button.BorderColorProperty, Value = StepColor},
                    new Setter {Property = Button.BorderWidthProperty, Value = 0.5},
                    new Setter {Property = Button.CornerRadiusProperty, Value = cornerRadius},
                    new Setter {Property = HeightRequestProperty, Value = height},
                    new Setter {Property = WidthRequestProperty, Value = width},
                }
            };

            Resources = new ResourceDictionary {{"unSelectedStyle", unSelectedStyle }, {"selectedStyle", selectedStyle}};
        }
    }
}
