using System;
using System.Globalization;
using System.Windows.Data;

namespace Kursovaya.Converters
{
    public class MoneyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                decimal result = (decimal)value;
                return Math.Round(result, 2) + "₽";
            }
            else
                return null;
        }

        public static string Convert(string value)
        {
            if (value != null)
            {
                decimal result = decimal.Parse(value);
                return Math.Round(result, 2) + "₽";
            }
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
