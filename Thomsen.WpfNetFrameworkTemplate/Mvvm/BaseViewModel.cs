using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Thomsen.WpfTools.Mvvm {
    public abstract class BaseViewModel<T> : INotifyPropertyChanged, IDisposable where T : Window, new() {
        #region Private Fields
        private bool _disposed;

        private readonly Window _view;
        #endregion Private Fields

        #region Public Properties
        public bool IsViewActive => _view.IsActive;

        public static string DefaultWindowTitle => $"{Assembly.GetExecutingAssembly().GetName().Name} ({Assembly.GetExecutingAssembly().GetName().Version})";
        #endregion Public Properties

        #region Constructors
        public BaseViewModel() {
            _view = new T {
                DataContext = this
            };
        }
        #endregion Constructors

        #region Public Methods
        public void Show() => _view.Show();

        public bool? ShowDialog() => _view.ShowDialog();

        protected void ExitDialog(bool? result) {
            _view.DialogResult = result;
            _view.Close();
        }
        #endregion Public Methods

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
