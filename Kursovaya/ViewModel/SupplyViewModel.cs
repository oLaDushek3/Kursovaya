using Kursovaya.DialogView;
using Kursovaya.Model.Factory;
using Kursovaya.Model.Supply;
using Kursovaya.Model.Worker;
using Kursovaya.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kursovaya.ViewModel
{
    public class SupplyViewModel : ViewModelBase, IDialog
    {
        ApplicationContext context = new ApplicationContext();

        #region Fields
        private ViewModelBase _currentChildView;
        private bool _isEnabled;
        private Visibility _backVisibility;
        private bool _animationAction;
        private bool _reverseAnimationAction;
        private bool _clickBool;
        private string _searchString = "Поиск...";

        //Supply fields
        private ISupplyRepository _supplyRepository = new SupplyRepository();
        private List<SupplyModel> _displayedsupplys;
        private SupplyModel? _selectedSupply;

        private IFactoryRepository _factoryRepository = new FactoryRepository();
        private FactoryModel _selectedFactory;

        private DateTime _selectedFirstDate = DateTime.Today;
        private DateTime _selectedSecondDate = DateTime.Today;
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
        public string SearchString
        {
            get => _searchString;
            set
            {
                _searchString = value;
                OnPropertyChanged(nameof(SearchString));
                if (value != null) 
                    Search(value);
            }
        }
        public MainViewModel MainViewModel { get; set; }

        //Supply properties
        public ObservableCollection<SupplyModel> DisplayedSupplys
        {
            get => new ObservableCollection<SupplyModel>(_displayedsupplys);
            set
            {
                _displayedsupplys = new List<SupplyModel>(value);
                OnPropertyChanged(nameof(DisplayedSupplys));
            }
        }
        private List<SupplyModel> AllSuply { get; set; }
        public SupplyModel? SelectedSupply
        {
            get => _selectedSupply;
            set
            {
                _selectedSupply = value;
                OnPropertyChanged(nameof(SelectedSupply));
            }
        }

        public List<FactoryModel> AllFactory { get; set; }
        public FactoryModel SelectedFactory
        {
            get => _selectedFactory;
            set
            {
                _selectedFactory = value;
                OnPropertyChanged(nameof(SelectedFactory));
                if (value != null)
                    FilterByFactory(value);
            }
        }

        public  DateTime SelectedFirstDate
        {
            get => _selectedFirstDate;
            set
            {
                _selectedFirstDate = value;
                OnPropertyChanged(nameof(SelectedFirstDate)); 
                if (value != null)
                    FilterByFactory(value, SelectedSecondDate);
            }
        }
        public DateTime SelectedSecondDate
        {
            get => _selectedSecondDate;
            set
            {
                _selectedSecondDate = value;
                OnPropertyChanged(nameof(SelectedSecondDate));
                if (value != null)
                    FilterByFactory(SelectedFirstDate, value);
            }
        }
        #endregion Properties

        //Commands
        public ICommand ShowAddSupplyCommand { get; }
        public ICommand GoBackCommand { get; }
        public ICommand DeleteSupplyCommand { get; }

        //Commands execution
        private void ExecuteShowAddSupplyCommand(object? obj)
        {
            CurrentChildView = new EditSupplyViewModel(SelectedSupply, this);
            IsEnabled = false;
            BackVisibility = Visibility.Visible;
            ReverseAnimationAction = false;
            AnimationAction = true;
        }
        private void ExecuteGoBackCommand(object? obj)
        {
            MainViewModel.ShowDialog(this);
        }
        private void ExecuteDeleteSupplyCommand(object? obj)
        {
            SupplyModel? supplyModel = _supplyRepository.GetById(SelectedSupply.SupplyId, context);
            foreach (SupplyProductModel supplyProduct in supplyModel.SupplyProducts)
            {
                foreach (SupplyProductPlaceModel supplyProductPlace in supplyProduct.SupplyProductPlaces)
                {
                    context.SupplyProductPlaces.Remove(supplyProductPlace);
                }
                context.SupplyProducts.Remove(supplyProduct);
            }

            foreach (WorkerModel workerModel in supplyModel.Workers)
            {
                workerModel.Supplies.Remove(supplyModel);
            }

            context.Supplies.Remove(supplyModel);
            context.SaveChanges();
        }

        //Constructor
        public SupplyViewModel(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;

            ShowAddSupplyCommand = new ViewModelCommand(ExecuteShowAddSupplyCommand);
            GoBackCommand = new ViewModelCommand(ExecuteGoBackCommand);
            DeleteSupplyCommand = new ViewModelCommand(ExecuteDeleteSupplyCommand);

            AllSuply = _supplyRepository.GetByAll();
            _displayedsupplys = AllSuply.ToList();
            SelectedSupply = _supplyRepository.GetById(DisplayedSupplys[0].SupplyId, context);

            AllFactory = _factoryRepository.GetByAll(context);

            IsEnabled = true;
            BackVisibility = Visibility.Collapsed;
        }
        
        //Methods
        private void ClearCurrentChildView()
        {
            Thread.Sleep(400);
            CurrentChildView = null;
        }
        public void Search(string Search)
        {
            if (Search != "")
                _displayedsupplys = AllSuply.Where(s => s.Factory.Address.ToLower().Contains(Search.ToLower()) | s.SupplyId.ToString().Contains(Search.ToLower()) | s.Date.ToString().Contains(Search.ToLower())).ToList();
            else 
                _displayedsupplys = AllSuply.ToList();
            OnPropertyChanged(nameof(DisplayedSupplys));
        }
        public void FilterByFactory(FactoryModel selectedFactory)
        {
            if (selectedFactory != null)
                _displayedsupplys = AllSuply.Where(s => s.Factory.FactoryId == selectedFactory.FactoryId).ToList();
            else
                _displayedsupplys = AllSuply.ToList();
            OnPropertyChanged(nameof(DisplayedSupplys));
        }
        public void FilterByFactory(DateTime firstDate, DateTime secondDate)
        {
            if (firstDate != null & secondDate != null)
                _displayedsupplys = AllSuply.Where(s => s.Date > firstDate & s.Date < secondDate).ToList();
            else
                _displayedsupplys = AllSuply.ToList();
            OnPropertyChanged(nameof(DisplayedSupplys));
        }

        //DialogClick
        public void ClickYes()
        {
            MainViewModel.CloseDialog();

            AnimationAction = false;
            ReverseAnimationAction = true;
            BackVisibility = Visibility.Collapsed;
            IsEnabled = true;

            _displayedsupplys = _supplyRepository.GetByAll();
            SelectedSupply = DisplayedSupplys[0];

            Thread myThread = new Thread(ClearCurrentChildView);
            myThread.Start();
        }

        public void ClickNo()
        {
            MainViewModel.CloseDialog();
        }
    }
}