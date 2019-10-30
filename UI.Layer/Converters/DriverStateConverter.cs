using System;
using System.Windows.Data;
using System.Globalization;

namespace UI.Layer.Converters
{
    public class DriverStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isCanceled = (bool)value;
            return !isCanceled;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
