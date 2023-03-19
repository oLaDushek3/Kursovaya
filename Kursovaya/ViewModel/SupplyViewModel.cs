using FontAwesome.Sharp;
using Kursovaya.Model;
using Kursovaya.Model.Supply;
using Kursovaya.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Kursovaya.ViewModel
{
    public class SupplyViewModel : ViewModelBase
    {
        //Fields
        private ISupplyRepository _supplyRepository;
        private List<SupplyModel>? _supplys;
        private SupplyModel? _selectedSupply;

        //Properties
        public List<SupplyModel>? Supplys
        {
            get => _supplys;
            set
            {
                _supplys = value;
                OnPropertyChanged(nameof(Supplys));
            }
        }

        public SupplyModel? SelectedSupply
        {
            get => _selectedSupply;
            set
            {
                _selectedSupply = value;
                OnPropertyChanged(nameof(SelectedSupply));
            }
        }

        //Constructor
        public SupplyViewModel()
        {
            _supplyRepository = new SupplyRepository();
            Supplys = _supplyRepository.GetByAll();
            ShowDeleteViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
        }

        public void editSupply()
        {
            ApplicationContext context = new ApplicationContext();

            var supply = context.Supplies.Include(s => s.Factory).
                Include(s => s.SupplyProducts).
                    ThenInclude(s => s.SupplyProductPlaces).
                        ThenInclude(s => s.Place).

                Include(s => s.SupplyProducts).
                    ThenInclude(s => s.Product).

                Include(s => s.Workers).
                    ThenInclude(s => s.Post).Where(s => s.SupplyId == 3).FirstOrDefault();

            if (supply != null)
            {
                //supply = SelectedSupply;
                //context.SaveChanges();
                context.Entry(supply).CurrentValues.SetValues(SelectedSupply);
            };
        }

        public ICommand ShowDeleteViewCommand { get; }
        private void ExecuteShowHomeViewCommand(object? obj)
        {
            editSupply();
        }
    }
}
