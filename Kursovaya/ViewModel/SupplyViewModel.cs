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
using System.Windows;
using System.Windows.Input;

namespace Kursovaya.ViewModel
{
    public class SupplyViewModel : ViewModelBase
    {
        ApplicationContext context = new ApplicationContext();

        #region Fields
        private ViewModelBase _currentChildView;
        private bool _isEnabled;
        private Visibility _backVisibility;
        private bool _animationAction;
        private bool _reverseAnimationAction;

        //Supply fields
        private ISupplyRepository _supplyRepository;
        private List<SupplyModel> _supplys;
        private SupplyModel? _selectedSupply;
        private SupplyModel? _addEditSelectedSupply;

        //Worker fields
        private List<WorkerModel> _addEditWorker;
        private WorkerModel? _selectedAddWorker;
        private WorkerModel? _selectedDeleteWoreker;

        //Product and place fields
        private List<ProductModel> _allProduct;
        private string? _quantityProduct;
        private List<PlaceModel> _allPlace;
        private string? _quantityOnPlace;
        #endregion Fields

        #region Properties
        public ViewModelBase CurrentChildView
        {
            get => _currentChildView;

            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }
        public bool IsEnabled
        {
            get => _isEnabled;

            set
            {
                _isEnabled = value;
                OnPropertyChanged(nameof(IsEnabled));
            }
        }
        public Visibility BackVisibility
        {
            get => _backVisibility;

            set
            {
                _backVisibility = value;
                OnPropertyChanged(nameof(BackVisibility));
            }
        }
        public bool AnimationAction
        {
            get => _animationAction;

            set
            {
                _animationAction = value;
                OnPropertyChanged(nameof(AnimationAction));
            }
        }
        public bool ReverseAnimationAction
        {
            get => _reverseAnimationAction;

            set
            {
                _reverseAnimationAction = value;
                OnPropertyChanged(nameof(ReverseAnimationAction));
            }
        }

        //Supply properties
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
        public SupplyModel? AddEditSelectedSupply
        {
            get => _addEditSelectedSupply;
            set
            {
                _addEditSelectedSupply = value;
                OnPropertyChanged(nameof(AddEditSelectedSupply));

                updateSelectedWorkers();
                OnPropertyChanged(nameof(AddEditSelectedSupply));
            }
        }

        //Worker properties
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
        public ObservableCollection<WorkerModel>? AddEditWorker
        {
            get => new ObservableCollection<WorkerModel>(_addEditWorker);
            set
            {
                _addEditWorker = new List<WorkerModel>(value);
                OnPropertyChanged(nameof(AddEditWorker));
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
        public WorkerModel? SelectedDeleteWoreker
        {
            get => _selectedDeleteWoreker;
            set
            {
                _selectedDeleteWoreker = value;
                OnPropertyChanged(nameof(SelectedDeleteWoreker));
            }
        }

        //Product and place properties
        public ObservableCollection<ProductModel> AllProduct
        {
            get => new ObservableCollection<ProductModel>(_allProduct);
            set
            {
                _allProduct = new List<ProductModel>(value);
                OnPropertyChanged(nameof(AllProduct));
            }
        }
        public int? QuantityProduct
        {
            get => Convert.ToInt32(_quantityProduct);
            set
            {
                _quantityProduct = value.ToString();
                OnPropertyChanged(nameof(QuantityProduct));
            }
        }
        public ObservableCollection<PlaceModel> AllPlace
        {
            get => new ObservableCollection<PlaceModel>(_allPlace);
            set
            {
                _allPlace = new List<PlaceModel>(value);
                OnPropertyChanged(nameof(AllPlace));
            }
        }
        public int? QuantityOnPlace
        {
            get => Convert.ToInt32(_quantityOnPlace);
            set
            {
                _quantityOnPlace = value.ToString();
                OnPropertyChanged(nameof(QuantityOnPlace));
            }
        }
        #endregion Properties

        //Commands
        public ICommand DeleteWorkerCommand { get; }
        public ICommand ShowAddSupplyCommand { get; }
        public ICommand GoBackCommand { get; }

        //Commands execution
        private bool CanExecuteDeleteWorkerCommand(object obj)
        {
            bool CanExecute;
            if (SelectedDeleteWoreker == null)
                CanExecute = false;
            else
                CanExecute = true;

            return CanExecute;
        }

        private void ExecuteDeleteWorkerCommand(object? obj)
        {
            WorkerModel? workerModel = context.Workers.Where(w => w.WorkerId == SelectedDeleteWoreker.WorkerId).FirstOrDefault();

            SupplyModel? supplyModel = context.Supplies.Where(s => s.SupplyId == SelectedSupply.SupplyId).
                Include(s => s.Workers).FirstOrDefault();

            supplyModel.Workers.Remove(workerModel);
            context.SaveChanges();

            _supplys[Supplys.IndexOf(SelectedSupply)].Workers.Remove(SelectedDeleteWoreker);
            updateSelectedWorkers();
            OnPropertyChanged(nameof(SelectedSupplyWorkers));
        }
        private void ExecuteShowAddSupplyCommand(object? obj)
        {
            CurrentChildView = new AddSupplyViewModel();
            IsEnabled = false;
            BackVisibility = Visibility.Visible;
            ReverseAnimationAction = false;
            AnimationAction = true;
            AddEditSelectedSupply = SelectedSupply;
        }
        private void ExecuteGoBackCommand(object? obj)
        {
            AnimationAction = false;
            ReverseAnimationAction = true;
            CurrentChildView = null;
            IsEnabled = true;
            BackVisibility = Visibility.Collapsed;
        }

        //Methods
        public void editSupply(WorkerModel workerModel)
        {
            int i = Supplys.IndexOf(Supplys.Where(s => s.SupplyId == SelectedSupply.SupplyId).FirstOrDefault());
            _supplys[i].Workers.Add(workerModel);
            updateSelectedWorkers();
            OnPropertyChanged(nameof(SelectedSupplyWorkers));

            SupplyModel? supplyModel = context.Supplies.Where(s => s.SupplyId == SelectedSupply.SupplyId).FirstOrDefault();

            supplyModel?.Workers.Add(workerModel);
            context.SaveChanges();
        }
        public void updateSelectedWorkers()
        {
            _addEditWorker = context.Workers.Where(w => !_selectedSupply.Workers.Select(w => w.WorkerId).Contains(w.WorkerId)).Include(w => w.Post).ToList();
            OnPropertyChanged(nameof(AddEditWorker));
        }

        //Constructor
        public SupplyViewModel()
        {
            DeleteWorkerCommand = new ViewModelCommand(ExecuteDeleteWorkerCommand, CanExecuteDeleteWorkerCommand);
            ShowAddSupplyCommand = new ViewModelCommand(ExecuteShowAddSupplyCommand);
            GoBackCommand = new ViewModelCommand(ExecuteGoBackCommand);

            _supplyRepository = new SupplyRepository();
            Supplys = _supplyRepository.GetByAll();

            SelectedSupply = _supplyRepository.GetById(Supplys[0].SupplyId);

            _allProduct = context.Products.ToList();
            _allPlace = context.Places.ToList();

            IsEnabled = true;
            BackVisibility = Visibility.Collapsed;
        }
    }
}