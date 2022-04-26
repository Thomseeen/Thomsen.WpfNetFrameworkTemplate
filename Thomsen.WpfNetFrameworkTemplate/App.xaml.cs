using System.Windows;

using Thomsen.WpfNetFrameworkTemplate.ViewModels;
using Thomsen.WpfNetFrameworkTemplate.Views;

namespace Thomsen.WpfNetFrameworkTemplate {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        #region Private Fields
        private readonly MainViewModel _viewModel = new();
        #endregion Private Fields

        #region Application Overrides
        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            _viewModel.Show();
        }

        protected override void OnExit(ExitEventArgs e) {
            base.OnExit(e);

            _viewModel.Dispose();
        }
        #endregion Application Overrides
    }
}
