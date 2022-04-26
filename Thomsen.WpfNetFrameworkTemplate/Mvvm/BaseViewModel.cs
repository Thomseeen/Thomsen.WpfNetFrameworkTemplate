using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Thomsen.WpfTools.Mvvm {
    public abstract class BaseViewModel : INotifyPropertyChanged {
        #region Public Properties
        public static string DefaultWindowTitle => $"{Assembly.GetExecutingAssembly().GetName().Name} ({Assembly.GetExecutingAssembly().GetName().Version})";
        #endregion Public Properties

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion INotifyPropertyChanged
    }
}
