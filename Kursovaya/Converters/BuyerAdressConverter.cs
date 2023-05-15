using Kursovaya.Model.Buyer;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Kursovaya.Converters
{
    public class BuyerAdressConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BuyerModel buyerModel = value as BuyerModel;
            if (buyerModel.Individual != null)
                return TextLengthConverter.Convert(buyerModel.Individual.BuyerAddresses.Last()?.Adress);
            else
                return TextLengthConverter.Convert(buyerModel.LegalEntity.BuyerAddresses.Last()?.Adress);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
