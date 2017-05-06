using System;
using System.Globalization;
using System.Threading;
using System.Windows.Data;

namespace CC.Module.FileExplorer.Converters
{
    public class DateToCultureDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date;

            if (value != null && DateTime.TryParse(value.ToString(), out date))
            { 
                return date.ToString("d", Thread.CurrentThread.CurrentCulture);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
