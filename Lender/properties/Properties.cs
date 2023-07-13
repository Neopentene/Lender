using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Lender.UI
{
    internal static class Properties
    {
        // ----------------------- Background Definitions -----------------------

        // Background Primary Definition
        public static DependencyProperty BackgroundPrimaryProperty =
            DependencyProperty
                .RegisterAttached("BackgroundPrimary", typeof(Brush), typeof(Properties), new PropertyMetadata(default(Brush)));

        public static Brush GetBackgroundPrimary(UIElement target) => (Brush)target.GetValue(BackgroundPrimaryProperty);

        public static void SetBackgroundPrimary(UIElement button, Brush value) => button.SetValue(BackgroundPrimaryProperty, value);

        // Background Secondary Definition
        public static readonly DependencyProperty BackgroundSecondaryProperty =
            DependencyProperty
                .RegisterAttached("BackgroundSecondary", typeof(Brush), typeof(Properties), new PropertyMetadata(default(Brush)));

        public static Brush GetBackgroundSecondary(UIElement target) => (Brush)target.GetValue(BackgroundSecondaryProperty);

        public static void SetBackgroundSecondary(UIElement button, Brush value) => button.SetValue(BackgroundSecondaryProperty, value);

        // Background Accent Definition
        public static readonly DependencyProperty BackgroundAccentProperty =
            DependencyProperty
                .RegisterAttached("BackgroundAccent", typeof(Brush), typeof(Properties), new PropertyMetadata(default(Brush)));

        public static Brush GetBackgroundAccent(UIElement target) => (Brush)target.GetValue(BackgroundAccentProperty);


        public static void SetBackgroundAccent(UIElement button, Brush value) => button.SetValue(BackgroundAccentProperty, value);


        // Background Disabled Definition
        public static readonly DependencyProperty BackgroundDisabledProperty =
            DependencyProperty
                .RegisterAttached("BackgroundDisabled", typeof(Brush), typeof(Properties), new PropertyMetadata(default(Brush)));

        public static Brush GetBackgroundDisabled(UIElement target) => (Brush)target.GetValue(BackgroundDisabledProperty);

        public static void SetBackgroundDisabled(UIElement button, Brush value) => button.SetValue(BackgroundDisabledProperty, value);

        // ----------------------- Foreground Definitions -----------------------

        // Foreground Primary Definition
        public static readonly DependencyProperty ForegroundPrimaryProperty =
            DependencyProperty
                .RegisterAttached("ForegroundPrimary", typeof(Brush), typeof(Properties), new PropertyMetadata(default(Brush)));

        public static Brush GetForegroundPrimary(UIElement target) => (Brush)target.GetValue(ForegroundPrimaryProperty);

        public static void SetForegroundPrimary(UIElement button, Brush value) => button.SetValue(ForegroundPrimaryProperty, value);

        // Foreground Secondary Definition
        public static readonly DependencyProperty ForegroundSecondaryProperty =
            DependencyProperty
                .RegisterAttached("ForegroundSecondary", typeof(Brush), typeof(Properties), new PropertyMetadata(default(Brush)));

        public static Brush GetForegroundSecondary(UIElement target) => (Brush)target.GetValue(ForegroundSecondaryProperty);

        public static void SetForegroundSecondary(UIElement button, Brush value) => button.SetValue(ForegroundSecondaryProperty, value);

        // Foreground Accent Definition
        public static readonly DependencyProperty ForegroundAccentProperty =
            DependencyProperty
                .RegisterAttached("ForegroundAccent", typeof(Brush), typeof(Properties), new PropertyMetadata(default(Brush)));

        public static Brush GetForegroundAccent(UIElement target) => (Brush)target.GetValue(ForegroundAccentProperty);

        public static void SetForegroundAccent(UIElement button, Brush value) => button.SetValue(ForegroundAccentProperty, value);

        // Foreground Disabled Definition
        public static readonly DependencyProperty ForegroundDisabledProperty =
            DependencyProperty
                .RegisterAttached("ForegroundDisabled", typeof(Brush), typeof(Properties), new PropertyMetadata(default(Brush)));

        public static Brush GetForegroundDisabled(UIElement target) => (Brush)target.GetValue(ForegroundDisabledProperty);

        public static void SetForegroundDisabled(UIElement button, Brush value) => button.SetValue(ForegroundDisabledProperty, value);

        // ----------------------- Border Definitions -----------------------

        // Border Primary Definition
        public static readonly DependencyProperty BorderPrimaryProperty =
            DependencyProperty
                .RegisterAttached("BorderPrimary", typeof(Brush), typeof(Properties), new PropertyMetadata(default(Brush)));

        public static Brush GetBorderPrimary(UIElement target) => (Brush)target.GetValue(BorderPrimaryProperty);

        public static void SetBorderPrimary(UIElement button, Brush value) => button.SetValue(BorderPrimaryProperty, value);

        // Border Secondary Definition
        public static readonly DependencyProperty BorderSecondaryProperty =
            DependencyProperty
                .RegisterAttached("BorderSecondary", typeof(Brush), typeof(Properties), new PropertyMetadata(default(Brush)));

        public static Brush GetBorderSecondary(UIElement target) => (Brush)target.GetValue(BorderSecondaryProperty);

        public static void SetBorderSecondary(UIElement button, Brush value) => button.SetValue(BorderSecondaryProperty, value);

        // Border Accent Definition
        public static readonly DependencyProperty BorderAccentProperty =
            DependencyProperty
                .RegisterAttached("BorderAccent", typeof(Brush), typeof(Properties), new PropertyMetadata(default(Brush)));

        public static Brush GetBorderAccent(UIElement target) => (Brush)target.GetValue(BorderAccentProperty);

        public static void SetBorderAccent(UIElement button, Brush value) => button.SetValue(BorderAccentProperty, value);

        // Border Disabled Definition
        public static readonly DependencyProperty BorderDisabledProperty =
            DependencyProperty
                .RegisterAttached("BorderDisabled", typeof(Brush), typeof(Properties), new PropertyMetadata(default(Brush)));

        public static Brush GetBorderDisabled(UIElement target) => (Brush)target.GetValue(BorderDisabledProperty);

        public static void SetBorderDisabled(UIElement button, Brush value) => button.SetValue(BorderDisabledProperty, value);

        // Circular Border Definition and Resolutions
        public static readonly DependencyProperty CircularBorderProperty =
            DependencyProperty
                .RegisterAttached("CircularBorder", typeof(bool), typeof(Properties), new PropertyMetadata(false, OnCircularBorderPropertyChanged));

        public static bool GetCircularBorder(Border target) => (bool)target.GetValue(CircularBorderProperty);

        public static void SetCircularBorder(Border button, bool value) => button.SetValue(CircularBorderProperty, value);

        public static readonly DependencyProperty ConsiderRadiusMaxProperty =
                    DependencyProperty
                        .RegisterAttached("ConsiderRadiusMax", typeof(bool), typeof(Properties), new PropertyMetadata(false, OnConsiderRadiusMaxPropertyChanged));

        public static bool GetConsiderCornerRadiusMax(Border target) => (bool)target.GetValue(ConsiderRadiusMaxProperty);

        public static void SetConsiderCornerRadiusMax(Border button, bool value) => button.SetValue(ConsiderRadiusMaxProperty, value);

        private static Action<FrameworkElement?> SizeEqualizer = EqualizeElementSizeMin;

        private static void OnCircularBorderPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is not Border element) return;

            bool value = (bool)args.NewValue;

            switch (value)
            {
                case true: { InitiateCircularBorder(element); break; }
                case false: { CeaseCircularBorder(element); break; }
            }
        }

        private static void OnConsiderRadiusMaxPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is not Border element) return;

            bool value = (bool)args.NewValue;

            switch (value)
            {
                case true: { SizeEqualizer = EqualizeElementSizeMax; break; }
                case false: { SizeEqualizer = EqualizeElementSizeMin; break; }
            }
        }

        private static void InitiateCircularBorder(Border element)
        {
            element.SizeChanged += Border_SizeChanged;
            RenderCircularBorder(element);
        }

        private static void CeaseCircularBorder(Border element)
        {
            element.SizeChanged += Border_SizeChanged;
            var localValues = element.GetLocalValueEnumerator();
            while (localValues.MoveNext())
            {
                if (localValues.Current.Value is CornerRadius localRadius)
                {
                    RenderCircularBorder(element, localRadius);
                    return;
                }
            }
            RenderCircularBorder(element, new(0));
        }

        private static void Border_SizeChanged(object sender, SizeChangedEventArgs e)
        { if (sender is Border element) { SizeEqualizer?.Invoke(element); RenderCircularBorder(element); } }

        private static void EqualizeElementSizeMax(FrameworkElement? element)
        {
            if (element == null) return;
            double size = Math.Max(element.ActualHeight, element.ActualWidth);
            Console.WriteLine($"Size: {size}");
            size = double.IsNaN(size) ? 0 : size;
            element.Height = element.Width = size;
        }

        private static void EqualizeElementSizeMin(FrameworkElement? element)
        {
            if (element == null) return;
            double size = Math.Min(element.ActualHeight, element.ActualWidth);
            Console.WriteLine($"Size: {size}");
            size = double.IsNaN(size) ? 0 : size;
            element.Height = element.Width = size;
        }

        private static void RenderCircularBorder(Border element, CornerRadius? radius = null)
        {
            if (radius == null)
            {
                double size = Math.Max(element.Height, element.Width) / 2;
                size = double.IsNaN(size) ? 0 : size;
                element.CornerRadius = new(size);
                return;
            }
            element.CornerRadius = (CornerRadius)radius;
        }
    }
}
