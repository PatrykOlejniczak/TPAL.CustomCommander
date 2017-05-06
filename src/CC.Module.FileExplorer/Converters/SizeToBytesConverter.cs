using System;
using System.Globalization;
using System.Windows.Data;

namespace CC.Module.FileExplorer.Converters
{
    public class SizeToBytesConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            long? size = (long?)values[0];
            string extension = (string)values[1];

            if (!string.IsNullOrEmpty(extension) && extension.Equals("dir"))
            {
                return "";
            }

            return size + " B";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
