using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Hotwire_Transient_GUI.Code
{
    [ValueConversion(typeof(long), typeof(string))]
    public class FileLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            long fileLength = (long)value;
            string sLen;
            if (fileLength >= (1 << 30))
                sLen = string.Format("{0} GB", fileLength >> 30);
            else if (fileLength >= (1 << 20))
                sLen = string.Format("{0} MB", fileLength >> 20);
            else if (fileLength >= (1 << 10))
                sLen = string.Format("{0} KB", fileLength >> 10);
            else
                sLen = string.Format("{0} B", fileLength);
            return sLen;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string lengthString = (string)value;
            long length;
            if (lengthString[lengthString.Length - 2] == 'G')
                length = long.Parse(lengthString.Substring(0, lengthString.Length - 3)) * (1 << 30);
            else if (lengthString[lengthString.Length - 2] == 'M')
                length = long.Parse(lengthString.Substring(0, lengthString.Length - 3)) * (1 << 20);
            else if (lengthString[lengthString.Length - 2] == 'K')
                length = long.Parse(lengthString.Substring(0, lengthString.Length - 3)) * (1 << 10);
            else
                length = long.Parse(lengthString.Substring(0, lengthString.Length - 3));
            return length;
        }
    }
}
