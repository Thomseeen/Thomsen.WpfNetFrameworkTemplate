using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Thomsen.WpfTools.Mvvm {
    internal abstract class BaseModel : INotifyPropertyChanged {
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
    }
}
