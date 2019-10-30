using System;
using System.Globalization;
using System.Windows.Data;
using Business.Layer.Model;

namespace UI.Layer.Converters
{
    public class EntryHourVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Hour hour = values[0] as Hour;
            Driver driver = values[1] as Driver;
            if (driver == null)
                return false;
            return driver.Equals(hour.EntryDriver);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
