using System;
using System.Windows;

using Thomsen.WpfNetFrameworkTemplate.Views;

namespace Thomsen.WpfNetFrameworkTemplate {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        #region Private Fields
        private readonly MainWindowView _view = new();
        #endregion Private Fields

        #region Application Overrides
        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            Current.MainWindow = _view;
            Current.MainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e) {
            base.OnExit(e);
        }
        #endregion Application Overrides
    }
}
