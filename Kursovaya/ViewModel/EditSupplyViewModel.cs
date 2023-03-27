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
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace Kursovaya.ViewModel
{
    public class EditSupplyViewModel : ViewModelBase
    {
        ApplicationContext context = new ApplicationContext();
        public SupplyViewModel SupplyViewModel { get; set; }

        #region Fields
        private string _errorMessage;
        //Supply fields
        private ISupplyRepository _supplyRepository = new SupplyRepository();
        private SupplyModel _selectedSupply;
        private SupplyModel? _editableSelectedSupply;

        //Factory fields
        private IFactoryRepository factoryRepository = new FactoryRepository();
        private List<FactoryModel> _allFactory;
        private FactoryModel _selectedFactory;

        //Worker fields
        private IWorkerRepository _workerRepository = new WorkerRepository();
        private List<WorkerModel> _allWorkers;
        private List<WorkerModel> _availableWorker;
        private WorkerModel? _selectedForAdditionWorker;
        private WorkerModel? _selectedForDeletionWorker;

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
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        //Supply properties
        public SupplyModel SelectedSupply
        {
            get => _selectedSupply;
            set
            {
                _selectedSupply = value;
                OnPropertyChanged(nameof(SelectedSupply));
            }
        }
        public SupplyModel? EditableSelectedSupply
        {
            get => _editableSelectedSupply;
            set
            {
                _editableSelectedSupply = value;
                OnPropertyChanged(nameof(EditableSelectedSupply));
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
        public FactoryModel SelectedFactory
        {
            get => _selectedFactory;
            set
            {
                _selectedFactory = value;
                OnPropertyChanged(nameof(SelectedFactory));
            }
        }

        //Worker properties
        public ObservableCollection<WorkerModel> AllWorkers
        {
            get => new ObservableCollection<WorkerModel>(SelectedSupply.Workers);
            set
            {
                _allWorkers = new List<WorkerModel>(value);
                OnPropertyChanged(nameof(AllWorkers));
            }
        }
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
        public WorkerModel? SelectedForDeletionWorker
        {
            get => _selectedForDeletionWorker;
            set
            {
                _selectedForDeletionWorker = value;
                OnPropertyChanged(nameof(SelectedForDeletionWorker));
                if (value != null)
                    DeleteWorker();
            }
        }

        //Product and place properties
        public ObservableCollection<SupplyProductModel> SelectedSupplyProducts
        {
            get => new ObservableCollection<SupplyProductModel>(SelectedSupply.SupplyProducts);
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
                    _selectedPlaces = value.SupplyProductPlaces;
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
        public ICommand DeleteSupplyProductCommand { get; }
        public ICommand AddSupplyProductCommand { get; }
        public ICommand DeleteSupplyProductPlaceCommand { get; }
        public ICommand AddSupplyProductPlaceCommand { get; }

        //Commands execution
        public void ExecuteSaveCommand(object? obj)
        {
            int quantityOnPlaces = 0;
            int allQuantity = 0;
            foreach (SupplyProductModel supplyProductModel in SelectedSupply.SupplyProducts)
            {

                foreach(SupplyProductPlaceModel supplyProductPlaceModel in supplyProductModel.SupplyProductPlaces)
                {
                    quantityOnPlaces += supplyProductPlaceModel.Quantity;
                }
                allQuantity += supplyProductModel.Quantity;
            }
            if (quantityOnPlaces != allQuantity)
                ErrorMessage = "Не вся продукция распределена!";
            else if (SelectedSupply.Workers.Count == 0)
                ErrorMessage = "Не выбран ни один работник!";
            else
            {
                SupplyModel? supplyModel = _supplyRepository.GetById(SelectedSupply.SupplyId, context);

                supplyModel.Factory = SelectedFactory;
                supplyModel.Date = SelectedSupply.Date;

                //Workers
                if (SelectedSupply.Workers != null)
                {
                    supplyModel.Workers.Clear();
                    WorkerModel? workerModel = null;

                    foreach (WorkerModel addWorker in SelectedSupply.Workers)
                    {
                        workerModel = _workerRepository.GetById(addWorker.WorkerId, context);
                        supplyModel.Workers.Add(workerModel);
                    };
                }
                else
                    supplyModel.Workers.Clear();

                //Products
                //Add
                foreach (SupplyProductModel addSupplyProduct in AddSupplyProduct)
                {
                    supplyModel.SupplyProducts.Add(addSupplyProduct);
                    context.SupplyProducts.AddAsync(addSupplyProduct);
                }

                //Delete
                foreach (int idForDelete in DeleteSupplyProduct)
                {
                    SupplyProductModel deleteSupplyProduct = context.SupplyProducts.Where(p => p.SupplyProductId == idForDelete).FirstOrDefault();
                    supplyModel.SupplyProducts.Remove(deleteSupplyProduct);

                    deleteSupplyProduct.SupplyProductPlaces.Clear();
                    deleteSupplyProduct.Supply = null;

                    context.SupplyProducts.Remove(deleteSupplyProduct);
                }

                //Edit
                foreach (SupplyProductModel EditSupplyProduct in EditSupplyProduct)
                {
                    SupplyProductModel editSupplyProduct = context.SupplyProducts.Where(p => p.SupplyProductId == EditSupplyProduct.SupplyProductId).FirstOrDefault();
                    editSupplyProduct.SupplyProductPlaces.Clear();
                    foreach (SupplyProductPlaceModel supplyProductPlace in EditSupplyProduct.SupplyProductPlaces)
                        editSupplyProduct.SupplyProductPlaces.Add(supplyProductPlace);
                }

                context.SaveChanges();

                SupplyViewModel.GoBackCommand.Execute(true);
            }
        }
        public void ExecuteDeleteSupplyProductCommand(object? obj)
        {
            SelectedSupply.SupplyProducts.Remove(SelectedSupplyProduct);
            DeleteSupplyProduct.Add(SelectedSupplyProduct.SupplyProductId);

            OnPropertyChanged(nameof(SelectedSupplyProducts));
            SelectedPlaces = null;

        }
        public void ExecuteAddSupplyProductCommand(object? obj)
        {
            SupplyProductModel addSupplyProductModel = new SupplyProductModel
            {
                ProductId = SelectedAddProduct.ProductId,
                Product = SelectedAddProduct,
                Quantity = QuantityAddProduct
            };
            SelectedSupply.SupplyProducts.Add(addSupplyProductModel);

            OnPropertyChanged(nameof(SelectedSupplyProducts));
            SelectedPlaces = null;
            SelectedAddProduct = null;
            SelectedSupplyProduct = addSupplyProductModel;

            AddSupplyProduct.Add(addSupplyProductModel);
        }
        public void ExecuteDeleteSupplyProductPlaceCommand(object? obj)
        {
            SelectedSupply.SupplyProducts.Where(s => s.SupplyProductId == SelectedSupplyProduct.SupplyProductId).FirstOrDefault().SupplyProductPlaces.Remove(SelectedPlace);
            EditSupplyProduct.Add(SelectedSupplyProduct);

            OnPropertyChanged(nameof(SelectedPlaces));
            SelectedPlace = null;
        }
        public void ExecuteAddSupplyProductPlaceCommand(object? obj)
        {
            SupplyProductPlaceModel addSupplyProductPlaceModel = new SupplyProductPlaceModel
            {
                Place = SelectedAddPlace,
                PlaceId = SelectedAddPlace.PlaceId,
                SupplyProductId = SelectedSupplyProduct.SupplyProductId,
                SupplyProduct = SelectedSupplyProduct,
                Quantity = QuantityAddPlace
            };
            SelectedSupply.SupplyProducts.Where(s => s.SupplyProductId == SelectedSupplyProduct.SupplyProductId).FirstOrDefault().SupplyProductPlaces.Add(addSupplyProductPlaceModel);
            EditSupplyProduct.Add(SelectedSupplyProduct);

            OnPropertyChanged(nameof(SelectedSupplyProducts));
            OnPropertyChanged(nameof(SelectedPlaces));
        }

        //Methods
        public void UpdateSelectedWorkers()
        {
            _availableWorker = context.Workers.Where(w => !SelectedSupply.Workers.Select(w => w.WorkerId).Contains(w.WorkerId)).Include(w => w.Post).ToList();
            OnPropertyChanged(nameof(AllWorkers));
            OnPropertyChanged(nameof(AvailableWorker));
        }
        public void AddWorker()
        {
            SelectedSupply.Workers.Add(SelectedForAdditionWorker);
            UpdateSelectedWorkers();
            SelectedForAdditionWorker = null;
        }
        public void DeleteWorker()
        {
            SelectedSupply.Workers.Remove(SelectedForDeletionWorker);
            
            UpdateSelectedWorkers();
            SelectedForDeletionWorker = null;
        }

        //Constructor
        public EditSupplyViewModel(SupplyModel selectedSupply, SupplyViewModel supplyViewModel)
        {
            SelectedSupply = selectedSupply;
            UpdateSelectedWorkers();

            AllProducts = _productRepository.GetByAll(context);

            AllPlaces = _placeRepository.GetByAll(context);

            AllFactory = factoryRepository.GetByAll(context);
            SelectedFactory = AllFactory.Where(f => f.FactoryId == SelectedSupply.FactoryId).FirstOrDefault();

            SaveCommand = new ViewModelCommand(ExecuteSaveCommand);
            DeleteSupplyProductCommand = new ViewModelCommand(ExecuteDeleteSupplyProductCommand);
            AddSupplyProductCommand = new ViewModelCommand(ExecuteAddSupplyProductCommand);
            AddSupplyProductPlaceCommand = new ViewModelCommand(ExecuteAddSupplyProductPlaceCommand);
            DeleteSupplyProductPlaceCommand = new ViewModelCommand(ExecuteDeleteSupplyProductPlaceCommand);
            
            OnPropertyChanged(nameof(SelectedFactory));
            SupplyViewModel = supplyViewModel;
        }
    }
}