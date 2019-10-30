using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace UI.Layer.Converters
{
    public class MultiRowStateConverter : IMultiValueConverter
    {
        readonly Brush Green = (SolidColorBrush)(new BrushConverter().ConvertFrom("#08fc4b"));

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool isCanceled = (bool)values[2];
            if (isCanceled)
                return Brushes.Red;
            object entryDriver = values[0];
            object exitDriver = values[1];
            if (entryDriver != null && exitDriver != null)
                return Green;
            if (entryDriver != null && exitDriver == null)
                return Brushes.Yellow;
            if (entryDriver == null && exitDriver != null)
                return Brushes.Yellow;
            return Brushes.White;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}