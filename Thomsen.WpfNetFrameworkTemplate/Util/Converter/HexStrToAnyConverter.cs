using System;
using System.Globalization;
using System.Windows.Data;

namespace Thomsen.WpfTools.Util.Converter {
    public class HexStrToAnyConverter : IValueConverter {
        #region IValueConverter
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            try {
                /* Dodge different Input Types */
                if (value is not long val) {
                    val = long.Parse(value.ToString(), CultureInfo.InvariantCulture);
                }

                return $"0x{val:X}";
            } catch (Exception) {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is string strValue && !string.IsNullOrEmpty(strValue)) {
                strValue = strValue.Trim().TrimStart(new char[] { '0', 'x' }).Trim();

                try {
                    long val = long.Parse(strValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);

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