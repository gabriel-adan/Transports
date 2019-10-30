using System;
using System.Windows.Data;
using System.Globalization;

namespace UI.Layer.Converters
{
    public class HourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().Substring(0, 5);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan time;
            if (TimeSpan.TryParse(value + "", out time))
                return time;
            return string.Empty;
        }
    }
}
