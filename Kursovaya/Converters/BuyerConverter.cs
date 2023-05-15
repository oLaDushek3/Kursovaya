using Kursovaya.Model.Buyer;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Kursovaya.Converters
{
    public class BuyerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BuyerModel buyerModel = value as BuyerModel;
            if (buyerModel.Individual != null)
                return FullNameConverter.Convert(buyerModel.Individual.Name);
            else
                return TextLengthConverter.Convert(buyerModel.LegalEntity.Organization);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
