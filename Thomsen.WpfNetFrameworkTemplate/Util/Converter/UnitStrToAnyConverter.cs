using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Thomsen.WpfTools.Util.Converter {
    internal class UnitStrToAnyConverter : IValueConverter {
        #region Public Properties
        public string Unit { get; set; } = "";

        public char SiSuffix { get; set; }

        public string DisplayFormat { get; set; } = "N2";

        public bool IncludeUnit { get; set; } = false;

        private char[] UnitChars => Unit.ToCharArray();
        #endregion Public Properties

        #region IValueConverter
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            try {
                /* Dodge different Input Types */
                if (value is not double val) {
                    val = double.Parse(value.ToString(), CultureInfo.InvariantCulture);
                }

                val = ApplySiSuffix(val, SiSuffix);

                return val.ToString(DisplayFormat, CultureInfo.InvariantCulture) + (IncludeUnit ? $" {SiSuffix}{Unit}" : "");
            } catch (Exception) {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is string strValue && !string.IsNullOrEmpty(strValue)) {
                strValue = strValue.Trim().TrimEnd(UnitChars).Trim();

                char suffix = GrabSuffixFromStringOrUseDefault(ref strValue, SiSuffix);

                try {
                    double val = RemoveSiSuffix(double.Parse(strValue, CultureInfo.InvariantCulture), suffix);

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

        #region Private Methods
        private static char GrabSuffixFromStringOrUseDefault(ref string strValue, char defaultSuffix) {
            char suffix = defaultSuffix;

            if (char.IsLetter(strValue.Last())) {
                suffix = strValue.Last();
                strValue = strValue.Trim(suffix).Trim();
            }

            return suffix;
        }

        private static double ApplySiSuffix(double val, char suffix) {
            return suffix switch {
                'Y' => val / 1E24,
                'Z' => val / 1E21,
                'E' => val / 1E18,
                'P' => val / 1E15,
                'T' => val / 1E12,
                'G' => val / 1E9,
                'M' => val / 1E6,
                'k' => val / 1E3,
                'h' => val / 1E2,
                'd' => val / 1E-1,
                'c' => val / 1E-2,
                'm' => val / 1E-3,
                'u' => val / 1E-6,
                'μ' => val / 1E-6,
                'n' => val / 1E-9,
                'p' => val / 1E-12,
                'f' => val / 1E-15,
                'a' => val / 1E-18,
                'z' => val / 1E-21,
                'y' => val / 1E-24,
                _ => val
            };
        }

        private static double RemoveSiSuffix(double val, char suffix) {
            return suffix switch {
                'Y' => val * 1E24,
                'Z' => val * 1E21,
                'E' => val * 1E18,
                'P' => val * 1E15,
                'T' => val * 1E12,
                'G' => val * 1E9,
                'M' => val * 1E6,
                'k' => val * 1E3,
                'h' => val * 1E2,
                'd' => val * 1E-1,
                'c' => val * 1E-2,
                'm' => val * 1E-3,
                'u' => val * 1E-6,
                'μ' => val * 1E-6,
                'n' => val * 1E-9,
                'p' => val * 1E-12,
                'f' => val * 1E-15,
                'a' => val * 1E-18,
                'z' => val * 1E-21,
                'y' => val * 1E-24,
                _ => val
            };
        }
        #endregion Private Methods
    }
}
