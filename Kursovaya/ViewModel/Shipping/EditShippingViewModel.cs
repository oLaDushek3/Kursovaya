using Kursovaya.DialogView;
using Kursovaya.DialogView.AddShippingProduct;
using Kursovaya.Model.Buyer;
using Kursovaya.Model.Factory;
using Kursovaya.Model.Product;
using Kursovaya.Model.Shipping;
using Kursovaya.Model.Worker;
using Kursovaya.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace Kursovaya.ViewModel.Shipping
{
    public class EditShippingViewModel : ViewModelBase
    {
        public ShippingViewModel CurrentShippingViewModel;

        #region Fields
        ApplicationContext context = new ApplicationContext();

        //Shipping fields
        private IShippingRepository _shippingRepository = new ShippingRepository();
        private ShippingModel _selectedShipping;
        private ShippingModel _editableShipping = new ShippingModel();

        //Buyer fields
        private IBuyerRepository _buyerRepository = new BuyerRepository();
        private List<BuyerModel> _allBuyers;
        private string _selectedBuyerType;

        //Worker fields
        private IWorkerRepository _workerRepository = new WorkerRepository();
        private List<WorkerModel> _availableWorker;
        private WorkerModel? _selectedForAdditionWorker;
        private List<WorkerModel> _editableShippingWorkers = new List<WorkerModel>();
        private int _shippingWorkersPostId;

        //Product and place fields
        private List<ShippingProductModel> _editableShippingProduct;
        #endregion Fields

        #region Properties
        //Shipping properties
        public ShippingModel EditableShipping
        {
            get => _editableShipping;
            set
            {
                _editableShipping = value;
                OnPropertyChanged(nameof(EditableShipping));
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
        public ObservableCollection<WorkerModel> EditableShippingWorkers
        {
            get => new ObservableCollection<WorkerModel>(_editableShippingWorkers);
            set
            {
                _editableShippingWorkers = new List<WorkerModel>(value);
                OnPropertyChanged(nameof(EditableShippingWorkers));
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
        public ObservableCollection<ShippingProductModel> EditableShippingProduct
        {
            get => new ObservableCollection<ShippingProductModel>(_editableShippingProduct);
            set
            {
                _editableShippingProduct = new List<ShippingProductModel>(value);
                OnPropertyChanged(nameof(EditableShippingProduct));
            }
        }

        #endregion Properties

        //Commands
        public ICommand SaveCommand { get; }
        public ICommand DeleteWorkerCommand { get; }
        public ICommand DeleteShippingProductCommand { get; }
        public ICommand AddShippingProductCommand { get; }

        //Commands execution
        public async void ExecuteSaveCommand(object? obj)
        {
            ConfirmationDialogViewModel confirmationDialogViewModel = new ConfirmationDialogViewModel(CurrentShippingViewModel.MainViewModel);
            bool result = await CurrentShippingViewModel.MainViewModel.ShowDialog(confirmationDialogViewModel);

            if (result)
            {
                List<ShippingProductModel> deleteShippingProducts;
                List<ShippingProductModel> addShippingProducts;

                deleteShippingProducts = _editableShipping.ShippingProducts.Except(_editableShippingProduct).ToList();
                foreach (ShippingProductModel shippingProduct in deleteShippingProducts)
                {
                    ProductModel product = shippingProduct.Product;
                    product.Quantity += shippingProduct.Quantity;

                    context.ShippingProducts.Remove(shippingProduct);
                }

                addShippingProducts = _editableShippingProduct.Except(_editableShipping.ShippingProducts).ToList();
                foreach (ShippingProductModel shippingProduct in addShippingProducts)
                {
                    ProductModel product = shippingProduct.Product;
                    product.Quantity -= shippingProduct.Quantity;

                    shippingProduct.Shipping = _editableShipping;
                    context.ShippingProducts.Add(shippingProduct);
                }

                context.SaveChanges();
                CurrentShippingViewModel.SaveModifiedShipping(_editableShipping);
            }
        }
        public void ExecuteDeleteWorkerCommand(object? obj)
        {
            EditableShipping.Workers.Remove(obj as WorkerModel);
            SortShippingWorkersByPost();
        }
        public void ExecuteDeleteShippingProductCommand(object obj)
        {
            _editableShippingProduct.Remove(obj as ShippingProductModel);
            _editableShipping.Amount -= (obj as ShippingProductModel).Product.PricePerUnit * (obj as ShippingProductModel).Quantity;
            OnPropertyChanged(nameof(EditableShippingProduct));
            OnPropertyChanged(nameof(EditableShipping));
        }
        public void ExecuteAddShippingProductCommand(object? obj)
        {
            AddShippingProductViewModel addShippingProductViewModel = new AddShippingProductViewModel(context, this);
            CurrentShippingViewModel.MainViewModel.ShowDialog(addShippingProductViewModel);
        }

        //Constructor
        public EditShippingViewModel(ShippingModel selectedShipping, ShippingViewModel shippingViewModel)
        {
            _selectedShipping = selectedShipping;
            _editableShipping = _shippingRepository.GetById(_selectedShipping.ShippingId, context);
            CurrentShippingViewModel = shippingViewModel;
            SortShippingWorkersByPost();

            _allBuyers = _buyerRepository.GetByAll(context);
            _editableShippingProduct = _editableShipping.ShippingProducts.ToList();

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
                _editableShippingWorkers = _editableShipping.Workers.Where(w => w.PostId == _shippingWorkersPostId).ToList();
                _availableWorker = (_workerRepository.GetByAll(context)).Where(w => !_editableShippingWorkers.Select(w => w.WorkerId).Contains(w.WorkerId)).Where(w => w.PostId == _shippingWorkersPostId).ToList();
            }
            else
            {
                _editableShippingWorkers = _editableShipping.Workers.ToList();
                _availableWorker = (_workerRepository.GetByAll(context)).Where(w => !_editableShippingWorkers.Select(w => w.WorkerId).Contains(w.WorkerId)).ToList();
            }

            OnPropertyChanged(nameof(EditableShippingWorkers));
            OnPropertyChanged(nameof(AvailableWorker));
        }
        private void AddWorker()
        {
            EditableShipping.Workers.Add(SelectedForAdditionWorker);
            SortShippingWorkersByPost();
            SelectedForAdditionWorker = null;
        }
        public void AddShippingProduct(ShippingProductModel shippingProductModel)
        {
            _editableShippingProduct.Add(shippingProductModel);
            _editableShipping.Amount += shippingProductModel.Product.PricePerUnit * shippingProductModel.Quantity;
            OnPropertyChanged(nameof(EditableShippingProduct));
            OnPropertyChanged(nameof(EditableShipping));
        }
    }
}
