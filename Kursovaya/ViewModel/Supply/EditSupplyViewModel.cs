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

namespace Kursovaya.ViewModel
{
    public class EditSupplyViewModel : ViewModelBase
    {

        #region Fields
        ApplicationContext context;
        private SupplyViewModel _currentSupplyViewModel { get; set; }

        //Supply fields
        private ISupplyRepository _supplyRepository = new SupplyRepository();
        private SupplyModel _selectedSupply;
        private SupplyModel _editableSupply;

        //Factory fields
        private IFactoryRepository factoryRepository = new FactoryRepository();
        private List<FactoryModel> _allFactory;

        //Worker fields
        private IWorkerRepository _workerRepository = new WorkerRepository();
        private List<WorkerModel> _availableWorker;
        private WorkerModel? _selectedForAdditionWorker;
        private List<WorkerModel> _editableSupplyWorkers = new List<WorkerModel>();
        private int _supplyWorkersPostId;

        //Product and place fields
        private ISupply_ProductRepository _supplyProductRepository = new Supply_ProductRepository();

        private IProductRepository _productRepository = new ProductRepository();
        private List<SupplyProductModel> _selectedSupplyProducts;
        private List<ProductModel> _allProducts;
        private SupplyProductModel _selectedSupplyProduct;
        private ProductModel _selectedAddProduct;
        private int _quantityAddProduct;

        private IPlaceRepository _placeRepository = new PlaceRepository();
        private List<PlaceModel> _allPlaces;
        private List<SupplyProductPlaceModel> _selectedPlaces;
        private PlaceModel _selectedAddPlace;
        private int _quantityAddPlace;
        private SupplyProductPlaceModel _selectedPlace;

        private List<SupplyProductModel> AddSupplyProduct = new List<SupplyProductModel>();
        private List<int> DeleteSupplyProduct = new List<int>();
        private List<SupplyProductModel> EditSupplyProduct = new List<SupplyProductModel>();

        #endregion Fields

        #region Properties
        //Supply properties
        public SupplyModel EditableSupply
        {
            get => _editableSupply;
            set
            {
                _editableSupply = value;
                OnPropertyChanged(nameof(EditableSupply));
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
                if(value != null)
                    AddWorker();
            }
        }
        public ObservableCollection<WorkerModel> EditableSupplyWorkers
        {
            get => new ObservableCollection<WorkerModel>(_editableSupplyWorkers);
            set
            {
                _editableSupplyWorkers = new List<WorkerModel>(value);
                OnPropertyChanged(nameof(EditableSupplyWorkers));
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
        public ObservableCollection<SupplyProductModel> SelectedSupplyProducts
        {
            get => new ObservableCollection<SupplyProductModel>(_selectedSupply.SupplyProducts);
            set
            {
                _selectedSupplyProducts = new List<SupplyProductModel>(value);
                OnPropertyChanged(nameof(SelectedSupplyProducts));
            }
        }
        public List<ProductModel> AllProducts
        {
            get => _allProducts;
            set
            {
                _allProducts = value;
                OnPropertyChanged(nameof(AllProducts));
            }
        }
        public SupplyProductModel SelectedSupplyProduct
        {
            get => _selectedSupplyProduct;
            set
            {
                _selectedSupplyProduct = value;
                OnPropertyChanged(nameof(SelectedSupplyProduct));
                if (value != null)
                {
                    _selectedPlaces = (List<SupplyProductPlaceModel>)value.SupplyProductPlaces;
                    OnPropertyChanged(nameof(SelectedPlaces));
                }
            }
        }
        public ProductModel SelectedAddProduct
        {
            get => _selectedAddProduct;
            set
            {
                _selectedAddProduct = value;
                OnPropertyChanged(nameof(SelectedAddProduct));
            }
        }
        public int QuantityAddProduct
        {
            get => _quantityAddProduct;
            set
            {
                _quantityAddProduct = value;
                OnPropertyChanged(nameof(QuantityAddProduct));
            }
        }

        public ObservableCollection<SupplyProductPlaceModel> SelectedPlaces
        {
            get 
            {
                if (_selectedPlaces != null)
                    return new ObservableCollection<SupplyProductPlaceModel>(_selectedPlaces);
                else return null;
            }
            set
            {
                if(value != null)
                    _selectedPlaces = new List<SupplyProductPlaceModel>(value);
                OnPropertyChanged(nameof(SelectedPlaces));
            }
        }
        public List<PlaceModel> AllPlaces
        {
            get => _allPlaces;
            set
            {
                _allPlaces = value;
                OnPropertyChanged(nameof(AllPlaces));
            }
        }
        public PlaceModel SelectedAddPlace
        {
            get => _selectedAddPlace;
            set
            {
                _selectedAddPlace = value;
                OnPropertyChanged(nameof(SelectedAddPlace));
            }
        }
        public int QuantityAddPlace
        {
            get => _quantityAddPlace;
            set
            {
                _quantityAddPlace = value;
                OnPropertyChanged(nameof(QuantityAddPlace));
            }
        }
        public SupplyProductPlaceModel SelectedPlace
        {
            get => _selectedPlace;
            set
            {
                _selectedPlace = value;
                OnPropertyChanged(nameof(SelectedPlace));
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
            _currentSupplyViewModel.GoBackCommand.Execute(true);
        }
        public void ExecuteDeleteWorkerCommand(object? obj)
        {
            EditableSupply.Workers.Remove(obj as WorkerModel);
            SortSupplyWorkersByPost();
        }
        public void ExecuteDeleteSupplyProductCommand(object? obj)
        {
            _selectedSupply.SupplyProducts.Remove(SelectedSupplyProduct);
            DeleteSupplyProduct.Add(SelectedSupplyProduct.SupplyProductId);

            OnPropertyChanged(nameof(SelectedSupplyProducts));
            SelectedPlaces = null;

        }
        public void ExecuteAddSupplyProductCommand(object? obj)
        {
            AddSupplyProductViewModel addSupplyProductViewModel = new AddSupplyProductViewModel(context);
            _currentSupplyViewModel.MainViewModel.ShowDialog(addSupplyProductViewModel);
        }

        //Constructor
        public EditSupplyViewModel(SupplyModel selectedSupply, SupplyViewModel supplyViewModel, ApplicationContext context)
        {
            this.context = context;
            _selectedSupply = selectedSupply;
            _editableSupply = _supplyRepository.GetById(selectedSupply.SupplyId, context);
            _currentSupplyViewModel = supplyViewModel;
            SortSupplyWorkersByPost();

            _allProducts = _productRepository.GetByAll(context);

            _allPlaces = _placeRepository.GetByAll(context);

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
                _editableSupplyWorkers = _editableSupply.Workers.Where(w => w.PostId == _supplyWorkersPostId).ToList();
                _availableWorker = (_workerRepository.GetByAll(context)).Where(w => !_editableSupplyWorkers.Select(w => w.WorkerId).Contains(w.WorkerId)).Where(w => w.PostId == _supplyWorkersPostId).ToList();
            }
            else
            {
                _editableSupplyWorkers = _editableSupply.Workers.ToList();
                _availableWorker = (_workerRepository.GetByAll(context)).Where(w => !_editableSupplyWorkers.Select(w => w.WorkerId).Contains(w.WorkerId)).ToList();
            }

            OnPropertyChanged(nameof(EditableSupplyWorkers));
            OnPropertyChanged(nameof(AvailableWorker));
        }
        public void AddWorker()
        {
            EditableSupply.Workers.Add(SelectedForAdditionWorker);
            SortSupplyWorkersByPost();
            SelectedForAdditionWorker = null;
        }
    }
}