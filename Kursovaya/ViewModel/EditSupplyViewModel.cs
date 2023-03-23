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
        private List<WorkerModel> _allWorkers;
        private List<WorkerModel> _availableWorker;
        private WorkerModel? _selectedForAdditionWorker;
        private WorkerModel? _selectedForDeletionWorker;

        //Product and place fields
        private IProductRepository _productRepository;
        private List<ProductModel> _allProducts;
        private IPlaceRepository _placeRepository;
        private List<PlaceModel> _allPlaces;

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
        public List<ProductModel> AllProducts
        {
            get => _allProducts;
            set
            {
                _allProducts = value;
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

        #endregion Properties

        //Commands
        public ICommand SaveCommand { get; }

        //Commands execution
        public void ExrecutesaveCommand(object? obj)
        {
            //context.Supplies.AsNoTracking().FirstOrDefault(s => s.SupplyId == SelectedSupply.SupplyId).St
            context.Entry(SelectedSupply).State = EntityState.Modified;

            //saveSupplise.Workers = SelectedSupply.Workers;
            //SupplyModel SaveSupply = context.Supplies.Where(s => s.SupplyId == SelectedSupply.SupplyId).First();
            //SaveSupply.Workers = SelectedSupply.Workers;
            //context.SaveChanges();
            //context.Supplies.Update(SelectedSupply);
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

            _productRepository = new ProductRepository();
            AllProducts = _productRepository.GetByAll();

            _placeRepository = new PlaceRepository();
            AllPlaces =  _placeRepository.GetByAll();

            SaveCommand = new ViewModelCommand(ExrecutesaveCommand);
        }
    }
}
