using Kursovaya.Model.Supply;
using Kursovaya.Model.User;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kursovaya.ViewModel
{
    public class EditSupplyViewModel : ViewModelBase
    {
        private SupplyModel _supply;
        public SupplyModel Supply
        {
            get => _supply;

            set
            {
                _supply = value;
                OnPropertyChanged(nameof(Supply));
            }
        }
        public EditSupplyViewModel() 
        {
        }
    }
}
