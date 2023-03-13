using Kursovaya.Model;
using Kursovaya.Model.Supply;
using Kursovaya.Repositories;
using System.Collections.Generic;

namespace Kursovaya.ViewModel
{
    public class SupplyViewModel : ViewModelBase
    {
        //Fields
        private ISupplyRepository _supplyRepository;
        private List<SupplyModel> _supplys;

        //Properties
        public List<SupplyModel> Supplys
        {
            get => _supplys;
            set
            {
                _supplys = value;
                OnPropertyChanged(nameof(Supplys));
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
