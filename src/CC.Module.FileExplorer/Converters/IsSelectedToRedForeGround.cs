using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CC.Module.FileExplorer.Converters
{
    public class IsSelectedToRedForeGround : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool selected = value != null && (bool)value;

            if (selected)
            {
                return new SolidColorBrush(Colors.Red);
            }

            return new SolidColorBrush(Colors.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}