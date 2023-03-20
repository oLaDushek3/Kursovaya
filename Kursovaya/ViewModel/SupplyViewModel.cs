using FontAwesome.Sharp;
using Kursovaya.Model;
using Kursovaya.Model.Supply;
using Kursovaya.Model.Worker;
using Kursovaya.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
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
        private List<WorkerModel> _workerModel;

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
            }
        }

        public ObservableCollection<WorkerModel> SelectedSupplyWorkers
        {
            get => new ObservableCollection<WorkerModel>(_selectedSupply.Workers);
            set
            {
                _selectedSupply.Workers = new List<WorkerModel>(value);
                OnPropertyChanged(nameof(SelectedSupply));
            }
        }

        public List<WorkerModel>? WorkerModel
        {
            get => _workerModel;
            set
            {
                _workerModel = value;
                OnPropertyChanged(nameof(WorkerModel));
            }
        }

        //Constructor
        public SupplyViewModel()
        {
            ApplicationContext context = new ApplicationContext();
            _supplyRepository = new SupplyRepository();
            Supplys = _supplyRepository.GetByAll();
            ShowDeleteViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            SelectedSupply = _supplyRepository.GetById(3);

            WorkerModel = context.Workers.Include(w => w.Post).ToList();
        }

        public void editSupply()
        {
            //ApplicationContext context = new ApplicationContext();

            //WorkerModel workerModel = context.Workers.Where(w => w.WorkerId == 3).FirstOrDefault();

            //SupplyModel supplyModel = context.Supplies.Where(s => s.SupplyId == SelectedSupply.SupplyId).FirstOrDefault();

            //supplyModel.Workers.Add(workerModel);
            //context.SaveChanges();
            _supplys[0].Workers.Add(WorkerModel[0]);
            OnPropertyChanged(nameof(SelectedSupplyWorkers));
        }

        public ICommand ShowDeleteViewCommand { get; }
        private void ExecuteShowHomeViewCommand(object? obj)
        {
            editSupply();
            
        }
    }
}
