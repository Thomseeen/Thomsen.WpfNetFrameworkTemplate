using System.Windows;

namespace Thomsen.WpfTools.Mvvm.Extensions {
    public class BindingProxy : Freezable {
        #region Properties
        public static readonly DependencyProperty DataProperty = DependencyProperty.Register("Data", typeof(object), typeof(BindingProxy), new UIPropertyMetadata(null));

        public object Data {
            get => GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }
        #endregion Properties

        #region Freezable
        protected override Freezable CreateInstanceCore() {
            return new BindingProxy();
        }
        #endregion Freezable
    }
}
