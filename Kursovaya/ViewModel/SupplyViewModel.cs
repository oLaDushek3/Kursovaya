using Kursovaya.Model.Supply;
using Kursovaya.Model.Worker;
using Kursovaya.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Kursovaya.ViewModel
{
    public class SupplyViewModel : ViewModelBase
    {

        ApplicationContext context = new ApplicationContext();
        //Fields
        private ISupplyRepository _supplyRepository;
        private List<SupplyModel>? _supplys;
        private SupplyModel? _selectedSupply;
        private List<WorkerModel> _addWorker;
        private WorkerModel? _selectedAddWorker;

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
        public SupplyModel? SelectedSupply
        {
            get => _selectedSupply;
            set
            {
                _selectedSupply = value;
                OnPropertyChanged(nameof(SelectedSupply));

                updateSelectedWorkers();
                OnPropertyChanged(nameof(SelectedSupplyWorkers));
            }
        }
        public ObservableCollection<WorkerModel> SelectedSupplyWorkers
        {
            get
            {
                updateSelectedWorkers();
                return new ObservableCollection<WorkerModel>(_selectedSupply.Workers);
            }
            set
            {
                _selectedSupply.Workers = new List<WorkerModel>(value);
                OnPropertyChanged(nameof(SelectedSupply));
            }
        }
        public ObservableCollection<WorkerModel>? AddWorker
        {
            get => new ObservableCollection<WorkerModel>(_addWorker);
            set
            {
                _addWorker = new List<WorkerModel>(value);
                OnPropertyChanged(nameof(AddWorker));
            }
        }
        public WorkerModel? SelectedAddWorker
        {
            get
            {
                return _selectedAddWorker;
            }
            set
            {
                if (value != null)
                    editSupply(value);

                _selectedAddWorker = value;
                OnPropertyChanged(nameof(SelectedAddWorker));
            }
        }

        //Constructor
        public SupplyViewModel()
        {
            _supplyRepository = new SupplyRepository();
            Supplys = _supplyRepository.GetByAll();
            ShowDeleteViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            SelectedSupply = _supplyRepository.GetById(Supplys[0].SupplyId);
        }

        public void editSupply(WorkerModel workerModel)
        {
            _supplys[Supplys.IndexOf(SelectedSupply)].Workers.Add(workerModel);
            updateSelectedWorkers();
            OnPropertyChanged(nameof(SelectedSupplyWorkers));


            //_supplys[1].Workers.Add(AddWorker[0]);
            //OnPropertyChanged(nameof(SelectedSupplyWorkers));

           SupplyModel supplyModel = context.Supplies.Where(s => s.SupplyId == SelectedSupply.SupplyId).FirstOrDefault();

            supplyModel.Workers.Add(workerModel);
            context.SaveChanges();

        }

        public void updateSelectedWorkers()
        {
            _addWorker = context.Workers.Where(w => !_selectedSupply.Workers.Select(w => w.WorkerId).Contains(w.WorkerId)).Include(w => w.Post).ToList();
            OnPropertyChanged(nameof(AddWorker));
        }

        public ICommand ShowDeleteViewCommand { get; }
        private void ExecuteShowHomeViewCommand(object? obj)
        {
            //editSupply();
        }
    }
}
