using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace Thomsen.WpfTools.Mvvm {
    public abstract class BaseViewModel<TWindow> : INotifyPropertyChanged, IDisposable where TWindow : Window, new() {
        #region Private Fields
        private bool _disposed;

        protected Window _view;
        #endregion Private Fields

        #region Public Properties
        public bool IsViewLoaded => _view?.IsLoaded ?? false;

        public static string DefaultWindowTitle => $"{Assembly.GetExecutingAssembly().GetName().Name} ({Assembly.GetExecutingAssembly().GetName().Version})";
        #endregion Public Properties

        #region Constructors
        public BaseViewModel() {
        }
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

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetProperty<T>(ref T backingFieldRef, T value, [CallerMemberName] string propertyName = "") {
            backingFieldRef = value;
            OnPropertyChanged(propertyName);
        }
        #endregion INotifyPropertyChanged

        #region IDisposable
        protected virtual void Dispose(bool disposing) {
            if (!_disposed) {
                if (disposing) { }
                _disposed = true;
            }
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion IDisposable
    }
}
