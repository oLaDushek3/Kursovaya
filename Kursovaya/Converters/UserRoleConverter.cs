using Kursovaya.Model.User;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Kursovaya.Converters
{
    public class UserRoleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string role = value as string;
            if (role == "Admin")
                return "Администратор";
            else
                return "Пользователь";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string role = value as string;
            if (role == "Администратор")
                return "Admin";
            else
                return "User";
        }
    }
}
