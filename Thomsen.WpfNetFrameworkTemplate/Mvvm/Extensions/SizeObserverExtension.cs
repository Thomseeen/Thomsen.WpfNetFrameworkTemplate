using System;
using System.Windows;

namespace Thomsen.WpfTools.Mvvm.Extensions {
    public static class SizeObserverExtension {
        #region DP Fields
        public static readonly DependencyProperty ObserveProperty = DependencyProperty.RegisterAttached(
            "Observe",
            typeof(bool),
            typeof(SizeObserverExtension),
            new FrameworkPropertyMetadata(OnObserveChanged));

        public static readonly DependencyProperty ObservedWidthProperty = DependencyProperty.RegisterAttached(
            "ObservedWidth",
            typeof(double),
            typeof(SizeObserverExtension));

        public static readonly DependencyProperty ObservedHeightProperty = DependencyProperty.RegisterAttached(
            "ObservedHeight",
            typeof(double),
            typeof(SizeObserverExtension));
        #endregion DP Fields

        #region Public Methods
        public static bool GetObserve(FrameworkElement fe) {
            return (bool)fe.GetValue(ObserveProperty);
        }

        public static void SetObserve(FrameworkElement fe, bool observe) {
            fe.SetValue(ObserveProperty, observe);
        }

        public static double GetObservedWidth(FrameworkElement fe) {
            return (double)fe.GetValue(ObservedWidthProperty);
        }

        public static void SetObservedWidth(FrameworkElement fe, double width) {
            fe.SetValue(ObservedWidthProperty, width);
        }

        public static double GetObservedHeight(FrameworkElement fe) {
            return (double)fe.GetValue(ObservedHeightProperty);
        }

        public static void SetObservedHeight(FrameworkElement fe, double height) {
            fe.SetValue(ObservedHeightProperty, height);
        }
        #endregion Public Methods

        #region Event Handler
        private static void OnObserveChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
            if (obj is not FrameworkElement fe) {
                throw new InvalidOperationException();
            }

            if ((bool)e.NewValue) {
                fe.SizeChanged += Fe_SizeChanged;
                UpdateObservedSizesForFrameworkElement(fe);
            } else {
                fe.SizeChanged -= Fe_SizeChanged;
            }
        }

        private static void Fe_SizeChanged(object sender, SizeChangedEventArgs e) {
            UpdateObservedSizesForFrameworkElement((FrameworkElement)sender);
        }

        private static void UpdateObservedSizesForFrameworkElement(FrameworkElement fe) {
            fe.SetCurrentValue(ObservedWidthProperty, fe.ActualWidth);
            fe.SetCurrentValue(ObservedHeightProperty, fe.ActualHeight);
        }
        #endregion Event Handler
    }
}
