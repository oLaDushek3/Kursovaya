using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Kursovaya.Converters
{
    public class FullNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var split = ((string)value).Split(' ');
            if (split.Length == 3)
            {
                return split.First() + " " +
                       string.Join(" ",
                                    split.Skip(1)
                                    .Select(s => string.Format("{0}.", s.First())));
            }
            else return value;
        }

        public static string Convert(string value)
        {
            var split = ((string)value).Split(' ');
            if (split.Length == 3)
            {
                return split.First() + " " +
                       string.Join(" ",
                                    split.Skip(1)
                                    .Select(s => string.Format("{0}.", s.First())));
            }
            else return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
