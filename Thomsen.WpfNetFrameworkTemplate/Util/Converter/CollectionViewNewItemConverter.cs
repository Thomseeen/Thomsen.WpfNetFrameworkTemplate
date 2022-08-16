using System;
using System.Windows.Data;

namespace Thomsen.WpfTools.Util.Converter {
    public class CollectionViewNewItemConverter : IValueConverter {
        #region IValueConverter
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value?.Equals(CollectionView.NewItemPlaceholder) ?? false) {
                return null;
            }

            return value;
        }
        #endregion IValueConverter
    }
}
