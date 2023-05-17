using Kursovaya.Model.Shipping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kursovaya.Print
{
    /// <summary>
    /// Логика взаимодействия для PrintPage.xaml
    /// </summary>
    public partial class PrintPage : Window
    {
        public PrintPage(ShippingModel printShipping)
        {
            InitializeComponent();

            WorkerList.ItemsSource = printShipping.Workers;
            ProductList.ItemsSource = printShipping.ShippingProducts;

            Title.Text = "Заказа номер " + printShipping.ShippingId;

            if(printShipping.BuyerNavigation.Individual != null)
            {
                Client.Text = printShipping.BuyerNavigation.Individual.Name;
                ClientPhone.Text = printShipping.BuyerNavigation.Individual.PhoneNumber;
            }
            else
            {
                Client.Text = printShipping.BuyerNavigation.LegalEntity.Organization;
                ClientPhone.Text = printShipping.BuyerNavigation.LegalEntity.PhoneNumber;
            }

            TotalCost.Text = printShipping.Amount.ToString();

            Date.Text = printShipping.Date.ToString("d MMMM yyyy");

            PrintDialog printDialog = new PrintDialog();
            if (printShipping != null)
            {
                printDialog.PrintVisual(PrintList, "Печать");
                this.Close();
            }
        }
    }
}
