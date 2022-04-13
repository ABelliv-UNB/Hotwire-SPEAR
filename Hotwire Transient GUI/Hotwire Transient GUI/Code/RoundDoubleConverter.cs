using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Hotwire_Transient_GUI.Code
{
    [ValueConversion(typeof(double), typeof(string))]
    public class RoundDoubleConverter : IValueConverter
    {
        public static int SignificantDigits = 4;
        public static int ExponentLowerBound = -2;
        public static int ExponentUpperBound = 4;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            double doubleValue = (double)value;
            if(doubleValue == 0) { return "0.000"; }

            doubleValue = RoundToSignificantDigits(doubleValue);
            double exponent = Math.Floor(Math.Log10(Math.Abs(doubleValue)));

            if (exponent < ExponentUpperBound && exponent > ExponentLowerBound)
            {
                string format;
                if(exponent >= 0)
                {
                    format = "N" + (SignificantDigits - exponent - 1).ToString();
                }
                else
                {
                    format = "N" + SignificantDigits.ToString();
                }
                return doubleValue.ToString(format);
            }
            else
            {
                string format1 = "G" + SignificantDigits.ToString();
                string format2 = "N" + (SignificantDigits - 1).ToString();
                return (doubleValue/(Math.Pow(10, exponent))).ToString(format2) + "E" + exponent.ToString(format1);
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double result;
            if (double.TryParse((string) value, out result))
            {
                return result;
            }
            else
            {
                //idk lol. shoudn't even need this function anyway
                return -1;
            }
        }

        public double RoundToSignificantDigits(double d)
        {
            if (d == 0)
                return 0;

            double scale = Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(d))) + 1);
            return scale * Math.Round(d / scale, SignificantDigits);
        }
    }
}
