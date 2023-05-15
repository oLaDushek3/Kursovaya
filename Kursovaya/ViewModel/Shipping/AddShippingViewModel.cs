using Kursovaya.DialogView;
using Kursovaya.DialogView.AddShippingProduct;
using Kursovaya.Model.Buyer;
using Kursovaya.Model.Factory;
using Kursovaya.Model.Shipping;
using Kursovaya.Model.Worker;
using Kursovaya.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Kursovaya.ViewModel.Shipping
{
    public class AddShippingViewModel : ViewModelBase
    {
        public ShippingViewModel CurrentShippingViewModel;

        #region Fields
        ApplicationContext context = new();

        //Shipping fields
        private ShippingModel _createdShipping = new() { Date = DateTime.Today };

        //Buyer fields
        private IBuyerRepository _buyerRepository = new BuyerRepository();
        private List<BuyerModel> _allBuyers;
        private string _selectedBuyerType;

        //Worker fields
        private IWorkerRepository _workerRepository = new WorkerRepository();
        private List<WorkerModel> _availableWorker;
        private WorkerModel? _selectedForAdditionWorker;
        private List<WorkerModel> _createdShippingWorkers = new();
        private int _shippingWorkersPostId;

        //Product and place fields
        private List<ShippingProductModel> _createdShippingProduct = new();
        #endregion Fields

        #region Properties
        //Shipping properties
        public ShippingModel CreatedShipping
        {
            get => _createdShipping;
            set
            {
                _createdShipping = value;
                OnPropertyChanged(nameof(CreatedShipping));
            }
        }

        //Buyer properties
        public List<BuyerModel> AllBuyers
        {
            get => _allBuyers;
            set
            {
                _allBuyers = value;
                OnPropertyChanged(nameof(AllBuyers));
            }
        }
        public List<string> BuyerTypeList
        {
            get
            {
                return new List<string>() { "Физ. лицо", "Юр. Лицо" };
            }
        }
        public string SelectedBuyerType
        {
            get => _selectedBuyerType;
            set
            {
                _selectedBuyerType = value;
                OnPropertyChanged(nameof(SelectedBuyerType));
                //FilterByFactory();
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
        public ObservableCollection<WorkerModel> CreatedShippingWorkers
        {
            get => new ObservableCollection<WorkerModel>(_createdShippingWorkers);
            set
            {
                _createdShippingWorkers = new List<WorkerModel>(value);
                OnPropertyChanged(nameof(CreatedShippingWorkers));
            }
        }
        public int ShippingWorkersPostId
        {
            get => _shippingWorkersPostId;
            set
            {
                _shippingWorkersPostId = value;
                OnPropertyChanged(nameof(ShippingWorkersPostId));
                SortShippingWorkersByPost();
            }
        }

        //Product and place properties
        public ObservableCollection<ShippingProductModel> CreatedShippingProduct
        {
            get => new ObservableCollection<ShippingProductModel>(_createdShippingProduct);
            set
            {
                _createdShippingProduct = new List<ShippingProductModel>(value);
                OnPropertyChanged(nameof(CreatedShippingProduct));
            }
        }

        #endregion Properties

        //Commands
        public ICommand SaveCommand { get; }
        public ICommand DeleteWorkerCommand { get; }
        public ICommand DeleteShippingProductCommand { get; }
        public ICommand AddShippingProductCommand { get; }

        //Commands execution
        public void ExecuteSaveCommand(object? obj)
        {
            context.Shippings.Add(_createdShipping);
            CreatedShipping.ShippingProducts = _createdShippingProduct;
            context.SaveChanges();
            CurrentShippingViewModel.AddNewShipping(CreatedShipping);
        }
        public void ExecuteDeleteWorkerCommand(object obj)
        {
            CreatedShipping.Workers.Remove(obj as WorkerModel);
            SortShippingWorkersByPost();
        }
        public void ExecuteDeleteShippingProductCommand(object obj)
        {
            _createdShippingProduct.Remove(obj as ShippingProductModel);
            _createdShipping.Amount -= (obj as ShippingProductModel).Product.PricePerUnit * (obj as ShippingProductModel).Quantity;
            OnPropertyChanged(nameof(CreatedShippingProduct));
            OnPropertyChanged(nameof(CreatedShipping));
        }
        public void ExecuteAddShippingProductCommand(object? obj)
        {
            AddShippingProductViewModel addShippingProductViewModel = new(context, this);
            CurrentShippingViewModel.MainViewModel.ShowDialog(addShippingProductViewModel);
        }

        //Constructor
        public AddShippingViewModel(ShippingViewModel shippingViewModel)
        {
            CurrentShippingViewModel = shippingViewModel;
            SortShippingWorkersByPost();

            _allBuyers = _buyerRepository.GetByAll(context);

            SaveCommand = new ViewModelCommand(ExecuteSaveCommand);
            DeleteShippingProductCommand = new ViewModelCommand(ExecuteDeleteShippingProductCommand);
            AddShippingProductCommand = new ViewModelCommand(ExecuteAddShippingProductCommand);
            DeleteWorkerCommand = new ViewModelCommand(ExecuteDeleteWorkerCommand);
        }

        //Methods
        private void SortShippingWorkersByPost()
        {
            if (_shippingWorkersPostId != 0)
            {
                _createdShippingWorkers = _createdShipping.Workers.Where(w => w.PostId == _shippingWorkersPostId).ToList();
                _availableWorker = (_workerRepository.GetByAll(context)).Where(w => !_createdShippingWorkers.Select(w => w.WorkerId).Contains(w.WorkerId)).Where(w => w.PostId == _shippingWorkersPostId).ToList();
            }
            else
            {
                _createdShippingWorkers = _createdShipping.Workers.ToList();
                _availableWorker = (_workerRepository.GetByAll(context)).Where(w => !_createdShippingWorkers.Select(w => w.WorkerId).Contains(w.WorkerId)).ToList();
            }

            OnPropertyChanged(nameof(CreatedShippingWorkers));
            OnPropertyChanged(nameof(AvailableWorker));
        }
        private void AddWorker()
        {
            CreatedShipping.Workers.Add(SelectedForAdditionWorker);
            SortShippingWorkersByPost();
            SelectedForAdditionWorker = null;
        }
        public void AddShippingProduct(ShippingProductModel shippingProductModel)
        {
            _createdShippingProduct.Add(shippingProductModel);
            _createdShipping.Amount += shippingProductModel.Product.PricePerUnit * shippingProductModel.Quantity;
            OnPropertyChanged(nameof(CreatedShippingProduct));
            OnPropertyChanged(nameof(CreatedShipping));
        }
    }
}