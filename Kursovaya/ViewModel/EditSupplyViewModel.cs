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
        private ISupplyRepository _supplyRepository;
        private SupplyModel _selectedSupply;
        private SupplyModel? _editableSelectedSupply;

        //Worker fields
        private IWorkerRepository _workerRepository;
        private List<WorkerModel> _allWorkers;
        private List<WorkerModel> _availableWorker;
        private WorkerModel? _selectedForAdditionWorker;
        private WorkerModel? _selectedForDeletionWorker;

        //Product and place fields
        private ISupply_ProductRepository _supplyProductRepository;

        private IProductRepository _productRepository;
        private List<SupplyProductModel> _allProducts;

        private IPlaceRepository _placeRepository;
        private List<PlaceModel> _allPlaces;

        private List<SupplyProductPlaceModel> _selectedPlaces;
        private SupplyProductModel _selectedProduct;

        private SupplyProductModel _selectedSupplyProduct;

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
        public ObservableCollection<SupplyProductModel> AllProducts
        {
            get => new ObservableCollection<SupplyProductModel>(SelectedSupply.SupplyProducts);
            set
            {
                _allProducts = new List<SupplyProductModel>(value);
                OnPropertyChanged(nameof(AllProducts));
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

        public List<SupplyProductPlaceModel> SelectedPlaces
        {
            get => _selectedPlaces;
            set
            {
                _selectedPlaces = value;
                OnPropertyChanged(nameof(SelectedPlaces));
            }
        }
        public SupplyProductModel SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
                if (value != null)
                    SelectedPlaces = value.SupplyProductPlaces;
            }
        }

        public SupplyProductModel SelectedSupplyProduct
        {
            get => _selectedSupplyProduct;
            set
            {
                _selectedSupplyProduct = value;
                OnPropertyChanged(nameof(SelectedSupplyProduct));
            }
        }

        #endregion Properties

        //Commands
        public ICommand SaveCommand { get; }
        public ICommand DeleteSupplyProductCommand { get; }

        //Commands execution
        public void ExecuteSaveCommand(object? obj)
        {
            _supplyRepository = new SupplyRepository();
            _workerRepository = new WorkerRepository();

            SupplyModel? supplyModel = _supplyRepository.GetById(SelectedSupply.SupplyId, context);

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



            _supplyProductRepository = new Supply_ProductRepository();

            if (SelectedSupply.SupplyProducts != null)
            {
                foreach(SupplyProductModel model in supplyModel.SupplyProducts)
                {
                    model.SupplyProductPlaces.Clear();
                }
                supplyModel.SupplyProducts.Clear();
                SupplyProductModel? supplyProductModel = null;

                foreach (SupplyProductModel addSupply in SelectedSupply.SupplyProducts)
                {
                    supplyProductModel = _supplyProductRepository.GetById(addSupply.ProductId, context);
                    supplyModel.SupplyProducts.Add(supplyProductModel);
                };
            }
            else
                supplyModel.SupplyProducts.Clear();

            //SupplyProductModel? supplyProductModel = null;
            //supplyProductModel = _supplyProductRepository.GetById(SelectedSupply.SupplyProducts[0].SupplyId, context);
            //supplyProductModel.SupplyProductPlaces.Clear();
            //supplyModel.SupplyProducts.Remove(supplyProductModel);

            //foreach (var entity in context.SupplyProducts) 
            //{ 
            //    context.Entry(entity).State = EntityState.Deleted; 
            //};

            //context.SupplyProducts.Remove(supplyProductModel);

            context.SaveChanges();
        }
        public void ExecuteDeleteSupplyProductCommand(object? obj)
        {
            SelectedSupply.SupplyProducts.Remove(SelectedProduct);

            OnPropertyChanged(nameof(AllProducts));
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

            SaveCommand = new ViewModelCommand(ExecuteSaveCommand);
            DeleteSupplyProductCommand = new ViewModelCommand(ExecuteDeleteSupplyProductCommand);
        }
    }
}
