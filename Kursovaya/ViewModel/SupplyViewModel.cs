using FontAwesome.Sharp;
using Kursovaya.Model;
using Kursovaya.Model.Supply;
using Kursovaya.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
        }
    }
}
