using Kursovaya.DialogView;
using Kursovaya.Model.Factory;
using Kursovaya.Model.Supply;
using Kursovaya.Model.Worker;
using Kursovaya.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kursovaya.ViewModel
{
    public class SupplyViewModel : ViewModelBase, IConfirmationDialog
    {
        #region Fields
        ApplicationContext context = new ApplicationContext();

        private ViewModelBase _currentChildView;
        private bool _isEnabled;
        private Visibility _backVisibility;
        private bool _animationAction;
        private bool _reverseAnimationAction;

        //Supply fields
        private ISupplyRepository _supplyRepository = new SupplyRepository();
        private List<SupplyModel> AllSuply;
        private List<SupplyModel> _displayedSupplys;
        private SupplyModel? _selectedSupply;

        private string _searchString;
        private List<SupplyModel> _searcSupplys;

        private IFactoryRepository _factoryRepository = new FactoryRepository();
        private FactoryModel _selectedFactory;
        private List<SupplyModel> _factorySortSupplys;

        private DateTime _selectedFirstDate = DateTime.Today;
        private DateTime _selectedSecondDate = DateTime.Today;
        private List<SupplyModel> _dateSortSupplys;

        //Selected supply fields
        private List<WorkerModel> _selectedSupplyWorkers = new List<WorkerModel>();
        private int _supplyWorkersPostId;
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
        public MainViewModel MainViewModel { get; set; }

        //Supply properties
        public ObservableCollection<SupplyModel> DisplayedSupplys
        {
            get => new ObservableCollection<SupplyModel>(_displayedSupplys);
            set
            {
                _displayedSupplys = new List<SupplyModel>(value);
                OnPropertyChanged(nameof(DisplayedSupplys));
            }
        }
        public SupplyModel? SelectedSupply
        {
            get => _selectedSupply;
            set
            {
                _selectedSupply = value;
                OnPropertyChanged(nameof(SelectedSupply));
                SortSupplyWorkersByPost();
            }
        }

        public string SearchString
        {
            get => _searchString;
            set
            {
                _searchString = value;
                OnPropertyChanged(nameof(SearchString));
                Search();
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
                FilterByFactory();
            }
        }

        public  DateTime SelectedFirstDate
        {
            get => _selectedFirstDate;
            set
            {
                _selectedFirstDate = value;
                OnPropertyChanged(nameof(SelectedFirstDate)); 
            }
        }
        public DateTime SelectedSecondDate
        {
            get => _selectedSecondDate;
            set
            {
                _selectedSecondDate = value;
                OnPropertyChanged(nameof(SelectedSecondDate));
            }
        }

        //Selected supply fields
        public ObservableCollection<WorkerModel> SelectedSupplyWorkers
        {
            get => new ObservableCollection<WorkerModel>(_selectedSupplyWorkers);
            set
            {
                _selectedSupplyWorkers = new List<WorkerModel>(value);
                OnPropertyChanged(nameof(SelectedSupplyWorkers));
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
        #endregion Properties

        //Commands
        public ICommand EditCommand { get; }
        public ICommand GoBackCommand { get; }
        public ICommand DeleteSupplyCommand { get; }
        public ICommand ClearSearchCommand { get; }
        public ICommand ClearSortCommand { get; }
        public ICommand SortByDateCommande { get; }

        //Commands execution
        private void ExecuteEditCommand(object? obj)
        {
            CurrentChildView = new EditSupplyViewModel(SelectedSupply, this, context);
            IsEnabled = false;
            BackVisibility = Visibility.Visible;
            ReverseAnimationAction = false;
            AnimationAction = true;
        }
        private bool CanExecuteEditCommand(object? obj)
        {
            if(SelectedSupply == null) return false;
            return true;
        }
        private void ExecuteGoBackCommand(object? obj)
        {
            ConfirmationDialogViewModel confirmationDialogViewModel = new ConfirmationDialogViewModel(this);
            MainViewModel.ShowDialog(confirmationDialogViewModel);
        }
        private void ExecuteDeleteSupplyCommand(object? obj)
        {
            if(SelectedSupply != null)
            {
                _supplyRepository.Remove(SelectedSupply.SupplyId);
            }
        }
        private void ExecuteClearSearchCommand(object? obj)
        {
            SearchString = "";
            _searcSupplys = null;
            Merger();
        }
        private void ExecuteClearSortCommand(object? obj)
        {
            SelectedFactory = null;
            _factorySortSupplys = null;

            Merger();
        }
        private void ExecuteSortByDateCommande(object? obj)
        {
            if (SelectedFirstDate != null & SelectedSecondDate != null)
                _dateSortSupplys = AllSuply.Where(s => s.Date >= SelectedFirstDate & s.Date <= SelectedSecondDate).ToList();
            else
                _dateSortSupplys = null;

            Merger();
        }
        private bool CanExecuteSortByDateCommande(object? obj)
        {
            if (SelectedFirstDate != null & SelectedSecondDate != null)
                return true;
            else
                return false;
        }

        //Constructor
        public SupplyViewModel(MainViewModel mainViewModel)
        { 
            MainViewModel = mainViewModel;

            EditCommand = new ViewModelCommand(ExecuteEditCommand, CanExecuteEditCommand);
            GoBackCommand = new ViewModelCommand(ExecuteGoBackCommand);
            DeleteSupplyCommand = new ViewModelCommand(ExecuteDeleteSupplyCommand);
            ClearSearchCommand = new ViewModelCommand(ExecuteClearSearchCommand);
            ClearSortCommand = new ViewModelCommand(ExecuteClearSortCommand);
            SortByDateCommande = new ViewModelCommand(ExecuteSortByDateCommande, CanExecuteSortByDateCommande);

            AllSuply = _supplyRepository.GetByAll();
            _displayedSupplys = AllSuply.ToList();

            AllFactory = _factoryRepository.GetByAll(context);

            IsEnabled = true;
            BackVisibility = Visibility.Collapsed;
        }

        //Methods

        private void Search()
        {
            if (SearchString != "")
                _searcSupplys = AllSuply.Where(s => s.Factory.Address.ToLower().Contains(SearchString.ToLower()) | s.SupplyId.ToString().Contains(SearchString.ToLower()) | s.Date.ToString().Contains(SearchString.ToLower())).ToList();
            else
                _searcSupplys = null;

            Merger();
        }
        private void FilterByFactory()
        {
            if (_selectedFactory != null)
                _factorySortSupplys = AllSuply.Where(s => s.Factory.FactoryId == _selectedFactory.FactoryId).ToList();
            else
                _factorySortSupplys = null;
            Merger();
        }
        private void Merger()
        {
            _displayedSupplys = AllSuply;

            if (_factorySortSupplys != null && _searcSupplys != null && _dateSortSupplys != null)
            {
                _displayedSupplys = _factorySortSupplys.Intersect(_searcSupplys.Intersect(_dateSortSupplys)).ToList();
                OnPropertyChanged(nameof(DisplayedSupplys));
            }

            if (_factorySortSupplys != null)
            {
                _displayedSupplys = AllSuply.Intersect(_factorySortSupplys).ToList();

                if (_searcSupplys != null)
                {
                    _displayedSupplys = _displayedSupplys.Intersect(_searcSupplys).ToList();
                }

                if (_dateSortSupplys != null)
                {
                    _displayedSupplys = _displayedSupplys.Intersect(_dateSortSupplys).ToList();
                }

                OnPropertyChanged(nameof(DisplayedSupplys));
            }
            else
            {
                if (_searcSupplys != null)
                {
                    _displayedSupplys = _displayedSupplys.Intersect(_searcSupplys).ToList();
                }

                if (_dateSortSupplys != null)
                {
                    _displayedSupplys = _displayedSupplys.Intersect(_dateSortSupplys).ToList();
                }

                OnPropertyChanged(nameof(DisplayedSupplys));
            }

            if (_factorySortSupplys == null && _searcSupplys == null && _dateSortSupplys == null)
            {
                _displayedSupplys = AllSuply;
                OnPropertyChanged(nameof(DisplayedSupplys));
            }
        }

        private void SortSupplyWorkersByPost()
        {
            if (_supplyWorkersPostId != 0)
                _selectedSupplyWorkers = _selectedSupply.Workers.Where(w => w.PostId == _supplyWorkersPostId).ToList();
            else
                _selectedSupplyWorkers = _selectedSupply.Workers.ToList();

            OnPropertyChanged(nameof(SelectedSupplyWorkers));
        }

        //DialogClick
        public new void ClickYes()
        {
            MainViewModel.CloseDialog();

            AnimationAction = false;
            ReverseAnimationAction = true;
            BackVisibility = Visibility.Collapsed;
            IsEnabled = true;

            Thread myThread = new Thread(ClearCurrentChildView);
            myThread.Start();
        }

        public new void ClickNo()
        {
            MainViewModel.CloseDialog();
        }
        private void ClearCurrentChildView()
        {
            Thread.Sleep(400);
            CurrentChildView = null;
        }
    }
}