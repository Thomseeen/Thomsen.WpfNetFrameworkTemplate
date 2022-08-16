using System;
using System.ComponentModel;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace Thomsen.WpfTools.Mvvm {
    internal abstract class BaseWindowViewModel<TWindow> : BaseViewModel, INotifyPropertyChanged, IDisposable where TWindow : Window, new() {
        #region Private Fields
        protected Window _view;
        #endregion Private Fields

        #region Public Properties
        public bool IsViewLoaded => _view?.IsLoaded ?? false;

        public static string DefaultWindowTitle => $"{Assembly.GetExecutingAssembly().GetName().Name} ({Assembly.GetExecutingAssembly().GetName().Version})";
        #endregion Public Properties

        #region Constructors
        public BaseWindowViewModel() { }
        #endregion Constructors

        #region Public Methods
        public void Focus() {
            if (_view is not null) {
                _view.Focus();
            }
        }

        public void Show() {
            if (_view is null) {
                _view = new TWindow {
                    DataContext = this
                };

                _view.Closed += (s, e) => {
                    _view = null;
                };
            }

            _view.Loaded += View_Loaded;
            _view.Closing += View_Closing;
            _view.Closed += View_Closed;

            _view.Show();
        }

        public bool? ShowDialog() {
            if (_view is not null) {
                throw new InvalidOperationException("ShowDialog can only be called once.");
            }

            _view = new TWindow {
                DataContext = this
            };

            _view.Loaded += View_Loaded;
            _view.Closing += View_Closing;
            _view.Closed += View_Closed;

            return _view.ShowDialog();
        }

        public void Close() {
            if (_view is not null) {
                _view.Loaded -= View_Loaded;

                _view.Close();
            }
        }

        public void ExitDialog(bool? result) {
            if (_view is not null) {
                _view.Loaded -= View_Loaded;

                _view.DialogResult = result;
                _view.Close();
            }
        }
        #endregion Public Methods

        #region Protected Methods
        private async void View_Loaded(object sender, RoutedEventArgs e) {
            await OnLoadedAsync();
        }

        private async void View_Closing(object sender, CancelEventArgs e) {
            await OnClosingAsync(e);
        }

        private async void View_Closed(object sender, EventArgs e) {
            await OnClosedAsync();
        }

        protected virtual Task OnLoadedAsync() {
            return Task.FromResult(default(object));
        }

        protected virtual Task OnClosingAsync(CancelEventArgs e) {
            return Task.FromResult(default(object));
        }

        protected virtual Task OnClosedAsync() {
            return Task.FromResult(default(object));
        }
        #endregion Protected Methods

        #region IDisposable
        protected override void Dispose(bool disposing) {
            if (disposing) { }
        }
        #endregion IDisposable
    }
}
