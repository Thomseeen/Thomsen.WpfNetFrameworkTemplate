using System;
using System.Globalization;
using System.Windows.Data;

namespace Thomsen.WpfTools.Util.Converter {
    internal class PerStrToAnyConverter : IValueConverter {
        #region Public Properties
        public string DisplayFormat { get; set; } = "N2";

        public bool IncludeSymbol { get; set; } = false;
        #endregion Public Properties

        #region IValueConverter
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            try {
                /* Dodge different Input Types */
                if (value is not double val) {
                    val = double.Parse(value.ToString(), CultureInfo.InvariantCulture);
                }

                val *= 100;

                return val.ToString(DisplayFormat, CultureInfo.InvariantCulture) + (IncludeSymbol ? $" %" : "");
            } catch (Exception) {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is string strValue && !string.IsNullOrEmpty(strValue)) {
                strValue = strValue.Trim().TrimEnd('%').Trim();

                try {
                    double val = double.Parse(strValue, CultureInfo.InvariantCulture) / 100.0;

                    /* Dodge different Target Types */
                    dynamic ret = System.Convert.ChangeType(val, targetType);

                    return ret;
                } catch (Exception) {
                    return null;
                }
            } else {
                return null;
            }
        }
        #endregion IValueConverter
    }
}
