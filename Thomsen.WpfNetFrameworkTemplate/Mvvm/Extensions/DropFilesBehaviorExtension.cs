using System;
using System.Diagnostics;
using System.Windows;

namespace Thomsen.WpfTools.Mvvm.Extensions {
    internal class DropFilesBehaviorExtension {
        #region DP Fields
        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached(
            "IsEnabled",
            typeof(bool),
            typeof(DropFilesBehaviorExtension),
            new FrameworkPropertyMetadata(default(bool), OnDependencyPropertyChanged) {
                BindsTwoWayByDefault = false,
            });
        #endregion DP Fields

        #region Public Methods
        public static void SetIsEnabled(DependencyObject element, bool value) {
            element.SetValue(IsEnabledProperty, value);
        }

        public static bool GetIsEnabled(DependencyObject element) {
            return (bool)element.GetValue(IsEnabledProperty);
        }
        #endregion Public Methods

        #region Event Handler
        private static void OnDependencyPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
            if (obj is not FrameworkElement fe) {
                throw new InvalidOperationException();
            }

            if ((bool)e.NewValue) {
                fe.AllowDrop = true;
                fe.Drop += Fe_DropAsync;
                fe.PreviewDragOver += Fe_PreviewDragOver;
            } else {
                fe.AllowDrop = false;
                fe.Drop -= Fe_DropAsync;
                fe.PreviewDragOver -= Fe_PreviewDragOver;
            }
        }

        private static void Fe_PreviewDragOver(object sender, DragEventArgs e) {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) {
                e.Effects = DragDropEffects.None;
            }

            e.Effects = DragDropEffects.Copy;
            e.Handled = true;
        }

        private static async void Fe_DropAsync(object sender, DragEventArgs e) {
            object dataContext = ((FrameworkElement)sender).DataContext;

            if (dataContext is not IFilesDropped filesDropped) {
                Trace.TraceError($"Binding error, '{dataContext.GetType().Name}' doesn't implement '{nameof(IFilesDropped)}'.");
                return;
            }

            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) {
                return;
            }

            if (e.Data.GetData(DataFormats.FileDrop) is string[] files) {
                await filesDropped.OnFilesDroppedAsync(files);
            }
        }
        #endregion Event Handler
    }
}
