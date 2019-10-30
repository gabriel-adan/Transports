using System;
using System.Windows.Data;
using System.Globalization;

namespace UI.Layer.Converters
{
    public class RowStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isCanceled = (bool)value;
            if (isCanceled)
                return "/UI.Layer;component/Images/icon-ok.ico";
            else
                return "/UI.Layer;component/Images/icon-cancel.ico";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}