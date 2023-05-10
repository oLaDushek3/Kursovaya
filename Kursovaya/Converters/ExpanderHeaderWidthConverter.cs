using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Kursovaya.Converters
{
    public class ExpanderHeaderWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value - 25;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
