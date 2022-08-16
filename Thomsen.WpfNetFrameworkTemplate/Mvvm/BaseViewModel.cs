using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Thomsen.WpfTools.Mvvm {
    internal abstract class BaseViewModel : INotifyPropertyChanged, IDisposable {
        #region Private Fields
        private bool _disposed;
        #endregion Private Fields

        #region Public Properties
        public static string DefaultTitle => $"{Assembly.GetExecutingAssembly().GetName().Name} ({Assembly.GetExecutingAssembly().GetName().Version})";
        #endregion Public Properties

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
