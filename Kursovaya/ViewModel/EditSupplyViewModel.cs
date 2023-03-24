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

        #region Fields
        //Supply fields
        private ISupplyRepository _supplyRepository = new SupplyRepository();
        private SupplyModel _selectedSupply;
        private SupplyModel? _editableSelectedSupply;

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
        private int _quantityAddProduct;

        private IPlaceRepository _placeRepository = new PlaceRepository();
        private List<PlaceModel> _allPlaces;

        private List<SupplyProductPlaceModel> _selectedPlaces;
        private ProductModel _selectedAddProduct;
        private PlaceModel _selectedAddPlace;
        private int _quantityAddPlace;


        #endregion Fields

        #region Properties

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
                    SelectedPlaces = value.SupplyProductPlaces;
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

        public List<SupplyProductPlaceModel> SelectedPlaces
        {
            get => _selectedPlaces;
            set
            {
                _selectedPlaces = value;
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

        #endregion Properties

        //Commands
        public ICommand SaveCommand { get; }
        public ICommand DeleteSupplyProductCommand { get; }
        public ICommand AddSupplyProductCommand { get; }
        public ICommand AddSupplyProductPlaceCommand { get; }

        //Commands execution
        public void ExecuteSaveCommand(object? obj)
        {
            //SupplyModel? supplyModel = _supplyRepository.GetById(SelectedSupply.SupplyId, context);

            //if (SelectedSupply.Workers != null)
            //{
            //    supplyModel.Workers.Clear();
            //    WorkerModel? workerModel = null;

            //    foreach (WorkerModel addWorker in SelectedSupply.Workers)
            //    {
            //        workerModel = _workerRepository.GetById(addWorker.WorkerId, context);
            //        supplyModel.Workers.Add(workerModel);
            //    };
            //}
            //else
            //    supplyModel.Workers.Clear();



            //_supplyProductRepository = new Supply_ProductRepository();

            //SupplyProductModel? supplyProductModel = null;
            //foreach(SupplyProductModel supplyProduct in SelectedSupply.SupplyProducts)
            //{
            //    supplyProductModel = _supplyProductRepository.GetById(supplyProduct.SupplyProductId, context);
            //    supplyProductModel.SupplyProductPlaces.Clear();
            //    context.SupplyProducts.Remove(supplyProductModel);
            //}

            //foreach (SupplyProductModel addSupply in SelectedSupply.SupplyProducts)
            //{
            //    context.SupplyProducts.Add(addSupply);
            //};

            //supplyProductModel = _supplyProductRepository.GetById(SelectedSupply.SupplyProducts[0].SupplyProductId, context);
            //supplyProductModel.SupplyProductPlaces.Clear();
            //context.SupplyProducts.Remove(supplyProductModel);
            context.SaveChanges();
        }
        public void ExecuteDeleteSupplyProductCommand(object? obj)
        {
            SelectedSupply.SupplyProducts.Remove(SelectedSupplyProduct);

            OnPropertyChanged(nameof(SelectedSupplyProducts));
            SelectedPlaces = null;
        }
        public void ExecuteAddSupplyProductCommand(object? obj)
        {
            SupplyProductModel addSupplyProductModel = new SupplyProductModel
            {
                SupplyId = SelectedSupply.SupplyId,
                Supply = SelectedSupply,
                ProductId = SelectedAddProduct.ProductId,
                Product = SelectedAddProduct,
                Quantity = QuantityAddProduct
            };
            SelectedSupply.SupplyProducts.Add(addSupplyProductModel);

            OnPropertyChanged(nameof(SelectedSupplyProducts));
            SelectedPlaces = null;
            SelectedAddProduct = null;
            SelectedSupplyProduct = addSupplyProductModel;

            //SupplyModel? supplyModel = _supplyRepository.GetById(SelectedSupply.SupplyId, context);
            //supplyModel.SupplyProducts.Add(SelectedSupply.SupplyProducts[6]);
            ////context.SupplyProducts.Add(addSupplyProductModel);
            //context.SaveChanges();
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
            SelectedSupply.SupplyProducts[SelectedSupplyProduct.SupplyProductId].SupplyProductPlaces.Add(addSupplyProductPlaceModel);

            OnPropertyChanged(nameof(SelectedSupplyProducts));
            SelectedPlaces = null;
            SelectedAddProduct = null;
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
        public EditSupplyViewModel(SupplyModel selectedSupply)
        {
            SelectedSupply = selectedSupply;
            UpdateSelectedWorkers();

            AllProducts = _productRepository.GetByAll(context);

            AllPlaces = _placeRepository.GetByAll(context);

            SaveCommand = new ViewModelCommand(ExecuteSaveCommand);
            DeleteSupplyProductCommand = new ViewModelCommand(ExecuteDeleteSupplyProductCommand);
            AddSupplyProductCommand = new ViewModelCommand(ExecuteAddSupplyProductCommand);
            AddSupplyProductPlaceCommand = new ViewModelCommand(ExecuteAddSupplyProductPlaceCommand);
        }
    }
}
