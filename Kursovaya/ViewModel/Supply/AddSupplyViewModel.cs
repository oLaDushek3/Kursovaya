using Humanizer;
using Kursovaya.DialogView.AddSupplyProduct;
using Kursovaya.Model.Factory;
using Kursovaya.Model.Place;
using Kursovaya.Model.Product;
using Kursovaya.Model.Supply;
using Kursovaya.Model.Worker;
using Kursovaya.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Kursovaya.ViewModel.Supply
{
    public class AddSupplyViewModel : ViewModelBase
    {
        public SupplyViewModel currentSupplyViewModel;

        #region Fields
        ApplicationContext context = new();

        //Supply fields
        private SupplyModel _createdSupply = new() { Date = DateTime.Today};

        //Factory fields
        private IFactoryRepository factoryRepository = new FactoryRepository();
        private List<FactoryModel> _allFactory;

        //Worker fields
        private IWorkerRepository _workerRepository = new WorkerRepository();
        private List<WorkerModel> _availableWorker;
        private WorkerModel? _selectedForAdditionWorker;
        private List<WorkerModel> _createdSupplyWorkers = new();
        private int _supplyWorkersPostId;

        //Product and place fields
        private List<SupplyProductModel> _createdSupplyProduct = new();
        #endregion Fields

        #region Properties
        //Supply properties
        public SupplyModel CreatedSupply
        {
            get => _createdSupply;
            set
            {
                _createdSupply = value;
                OnPropertyChanged(nameof(CreatedSupply));
            }
        }

        //Factory properties
        public List<FactoryModel> AllFactory
        {
            get => _allFactory;
            set
            {
                _allFactory = value;
                OnPropertyChanged(nameof(AllFactory));
            }
        }

        //Worker properties
        public ObservableCollection<WorkerModel> AvailableWorker
        {
            get => new ObservableCollection<WorkerModel>(_availableWorker);
            set
            {
                _availableWorker = new List<WorkerModel>(value);
                OnPropertyChanged(nameof(AvailableWorker));
            }
        }
        public WorkerModel? SelectedForAdditionWorker
        {
            get => _selectedForAdditionWorker;
            set
            {
                _selectedForAdditionWorker = value;
                OnPropertyChanged(nameof(SelectedForAdditionWorker));
                if (value != null)
                    AddWorker();
            }
        }
        public ObservableCollection<WorkerModel> CreatedSupplyWorkers
        {
            get => new ObservableCollection<WorkerModel>(_createdSupplyWorkers);
            set
            {
                _createdSupplyWorkers = new List<WorkerModel>(value);
                OnPropertyChanged(nameof(CreatedSupplyWorkers));
            }
        }
        public int SupplyWorkersPostId
        {
            get => _supplyWorkersPostId;
            set
            {
                _supplyWorkersPostId = value;
                OnPropertyChanged(nameof(SupplyWorkersPostId));
                SortSupplyWorkersByPost();
            }
        }

        //Product and place properties
        public ObservableCollection<SupplyProductModel> CreatedSupplyProduct
        {
            get => new ObservableCollection<SupplyProductModel>(_createdSupplyProduct);
            set
            {
                _createdSupplyProduct = new List<SupplyProductModel>(value);
                OnPropertyChanged(nameof(CreatedSupplyProduct));
            }
        }

        #endregion Properties

        //Commands
        public ICommand SaveCommand { get; }
        public ICommand DeleteWorkerCommand { get; }
        public ICommand DeleteSupplyProductCommand { get; }
        public ICommand AddSupplyProductCommand { get; }

        //Commands execution
        public void ExecuteSaveCommand(object? obj)
        {
            context.Supplies.Add(_createdSupply);
            CreatedSupply.SupplyProducts = _createdSupplyProduct;
            context.SaveChanges();
            currentSupplyViewModel.AddNewSupply(CreatedSupply);
        }
        public void ExecuteDeleteWorkerCommand(object obj)
        {
            CreatedSupply.Workers.Remove(obj as WorkerModel);
            SortSupplyWorkersByPost();
        }
        public void ExecuteDeleteSupplyProductCommand(object obj)
        {
            _createdSupplyProduct.Remove(obj as SupplyProductModel);
            OnPropertyChanged(nameof(CreatedSupplyProduct));
        }
        public void ExecuteAddSupplyProductCommand(object? obj)
        {
            AddSupplyProductViewModel addSupplyProductViewModel = new AddSupplyProductViewModel(context, this);
            currentSupplyViewModel.MainViewModel.ShowDialog(addSupplyProductViewModel);
        }

        //Constructor
        public AddSupplyViewModel(SupplyViewModel supplyViewModel)
        {
            currentSupplyViewModel = supplyViewModel;
            SortSupplyWorkersByPost();

            _allFactory = factoryRepository.GetByAll(context);

            SaveCommand = new ViewModelCommand(ExecuteSaveCommand);
            DeleteSupplyProductCommand = new ViewModelCommand(ExecuteDeleteSupplyProductCommand);
            AddSupplyProductCommand = new ViewModelCommand(ExecuteAddSupplyProductCommand);
            DeleteWorkerCommand = new ViewModelCommand(ExecuteDeleteWorkerCommand);
        }

        //Methods
        private void SortSupplyWorkersByPost()
        {
            if (_supplyWorkersPostId != 0)
            {
                _createdSupplyWorkers = _createdSupply.Workers.Where(w => w.PostId == _supplyWorkersPostId).ToList();
                _availableWorker = (_workerRepository.GetByAll(context)).Where(w => !_createdSupplyWorkers.Select(w => w.WorkerId).Contains(w.WorkerId)).Where(w => w.PostId == _supplyWorkersPostId).ToList();
            }
            else
            {
                _createdSupplyWorkers = _createdSupply.Workers.ToList();
                _availableWorker = (_workerRepository.GetByAll(context)).Where(w => !_createdSupplyWorkers.Select(w => w.WorkerId).Contains(w.WorkerId)).ToList();
            }

            OnPropertyChanged(nameof(CreatedSupplyWorkers));
            OnPropertyChanged(nameof(AvailableWorker));
        }
        private void AddWorker()
        {
            CreatedSupply.Workers.Add(SelectedForAdditionWorker);
            SortSupplyWorkersByPost();
            SelectedForAdditionWorker = null;
        }
        public void AddSupplyProduct(SupplyProductModel supplyProductModel)
        {
            _createdSupplyProduct.Add(supplyProductModel);
            OnPropertyChanged(nameof(CreatedSupplyProduct));
        }
    }
}
