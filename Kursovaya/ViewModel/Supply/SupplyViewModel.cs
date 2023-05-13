using Kursovaya.DialogView;
using Kursovaya.Model.Factory;
using Kursovaya.Model.Supply;
using Kursovaya.Model.Worker;
using Kursovaya.Repositories;
using Kursovaya.View;
using Kursovaya.ViewModel.Supply;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Kursovaya.ViewModel
{
    public class SupplyViewModel : ViewModelBase
    {
        public MainViewModel MainViewModel;
        #region Fields
        public ApplicationContext context = new ApplicationContext();

        private ViewModelBase? _currentChildView;
        private bool _isEnabled = true;
        private Visibility _backVisibility = Visibility.Collapsed;
        private bool _animationAction;
        private bool _reverseAnimationAction;

        //Supply fields
        private ISupplyRepository _supplyRepository = new SupplyRepository();
        private List<SupplyModel>? _allSuply;
        private List<SupplyModel>? _displayedSupplys;
        private SupplyModel? _selectedSupply;

        private string? _searchString;
        private List<SupplyModel>? _searcSupplys;

        private IFactoryRepository _factoryRepository = new FactoryRepository();
        private FactoryModel? _selectedFactory;
        private List<SupplyModel>? _factorySortSupplys;

        private DateTime? _selectedFirstDate = null;
        private DateTime? _selectedSecondDate = null;
        private List<SupplyModel>? _dateSortSupplys;

        //Selected supply fields
        private List<WorkerModel>? _selectedSupplyWorkers = new List<WorkerModel>();
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

        public DateTime? SelectedFirstDate
        {
            get => _selectedFirstDate;
            set
            {
                _selectedFirstDate = value;
                OnPropertyChanged(nameof(SelectedFirstDate)); 
            }
        }
        public DateTime? SelectedSecondDate
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
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand GoBackCommand { get; }
        public ICommand DeleteSupplyCommand { get; }
        public ICommand ClearSearchCommand { get; }
        public ICommand ClearSortCommand { get; }
        public ICommand SortByDateCommande { get; }
        public ICommand ClearSortByDateCommande { get; }

        //Commands execution
        private void ExecuteAddCommand(object? obj)
        {
            CurrentChildView = new AddSupplyViewModel(this);
            IsEnabled = false;
            BackVisibility = Visibility.Visible;
            ReverseAnimationAction = false;
            AnimationAction = true;
        }
        private void ExecuteEditCommand(object? obj)
        {
            CurrentChildView = new EditSupplyViewModel(SelectedSupply, this);
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
        private async void ExecuteGoBackCommand(object? obj)
        {
            ConfirmationDialogViewModel confirmationDialogViewModel = new ConfirmationDialogViewModel(MainViewModel);
            bool result = await MainViewModel.ShowDialog(confirmationDialogViewModel);

            if (result)
            {
                MainViewModel.CloseDialog();

                AnimationAction = false;
                ReverseAnimationAction = true;
                BackVisibility = Visibility.Collapsed;
                IsEnabled = true;

                Thread myThread = new Thread(ClearCurrentChildView);
                myThread.Start();
            }
        }
        private async void ExecuteDeleteSupplyCommand(object obj)
        {
            ConfirmationDialogViewModel confirmationDialogViewModel = new ConfirmationDialogViewModel(MainViewModel);
            bool result = await MainViewModel.ShowDialog(confirmationDialogViewModel);

            if (result)
            {
                _supplyRepository.Remove((obj as SupplyModel).SupplyId, context);
                RefreshSupplyList(null);
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
            SortByDate();
        }
        private bool CanExecuteClearSortByDateCommande(object? obj)
        {
            if (SelectedFirstDate != null && SelectedSecondDate != null)
                return true;
            
            return false;
        }
        private void ExecuteClearSortByDateCommande (object? obj)
        {
            SelectedFirstDate = null;
            SelectedSecondDate = null;
            _dateSortSupplys = null;
            Merger();
        }

        //Constructor
        public SupplyViewModel(MainViewModel mainViewModel)
        { 
            MainViewModel = mainViewModel;

            AddCommand = new ViewModelCommand(ExecuteAddCommand);
            EditCommand = new ViewModelCommand(ExecuteEditCommand, CanExecuteEditCommand);
            GoBackCommand = new ViewModelCommand(ExecuteGoBackCommand);
            DeleteSupplyCommand = new ViewModelCommand(ExecuteDeleteSupplyCommand);
            ClearSearchCommand = new ViewModelCommand(ExecuteClearSearchCommand);
            ClearSortCommand = new ViewModelCommand(ExecuteClearSortCommand);
            SortByDateCommande = new ViewModelCommand(ExecuteSortByDateCommande);
            ClearSortByDateCommande = new ViewModelCommand(ExecuteClearSortByDateCommande, CanExecuteClearSortByDateCommande);

            AllFactory = _factoryRepository.GetByAll(context);

            RefreshSupplyList(null);
        }

        //Methods
        private void RefreshSupplyList(int? selectedSupplyId)
        {
            context = new ApplicationContext();

            _allSuply = _supplyRepository.GetByAll(context);
            _displayedSupplys = _allSuply.ToList();
            OnPropertyChanged(nameof(DisplayedSupplys));
            
            SelectedSupply = _displayedSupplys.Where(s => s.SupplyId == selectedSupplyId).FirstOrDefault();
            OnPropertyChanged(nameof(SelectedSupply));

            Search();
            FilterByFactory();
            SortByDate();
        }

        private void Search()
        {
            if (SearchString != "" && SearchString != null)
                _searcSupplys = _allSuply.Where(s => s.Factory.Address.ToLower().Contains(SearchString.ToLower()) | s.SupplyId.ToString().Contains(SearchString.ToLower()) | s.Date.ToString().Contains(SearchString.ToLower())).ToList();
            else
                _searcSupplys = null;

            Merger();
        }
        private void FilterByFactory()
        {
            if (_selectedFactory != null)
                _factorySortSupplys = _allSuply.Where(s => s.Factory.FactoryId == _selectedFactory.FactoryId).ToList();
            else
                _factorySortSupplys = null;
            Merger();
        }
        private void SortByDate()
        {
            if (SelectedFirstDate != null && SelectedSecondDate != null)
                _dateSortSupplys = _allSuply.Where(s => s.Date >= SelectedFirstDate & s.Date <= SelectedSecondDate).ToList();
            Merger();
        }
        private void Merger()
        {
            _displayedSupplys = _allSuply;

            if (_factorySortSupplys != null && _searcSupplys != null && _dateSortSupplys != null)
            {
                _displayedSupplys = _factorySortSupplys.Intersect(_searcSupplys.Intersect(_dateSortSupplys)).ToList();
                OnPropertyChanged(nameof(DisplayedSupplys));
            }

            if (_factorySortSupplys != null)
            {
                _displayedSupplys = _allSuply.Intersect(_factorySortSupplys).ToList();

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
                _displayedSupplys = _allSuply;
                OnPropertyChanged(nameof(DisplayedSupplys));
            }
        }

        private void SortSupplyWorkersByPost()
        {
            if(_selectedSupply != null)
            {
                if (_supplyWorkersPostId != 0)
                    _selectedSupplyWorkers = _selectedSupply.Workers.Where(w => w.PostId == _supplyWorkersPostId).ToList();
                else
                    _selectedSupplyWorkers = _selectedSupply.Workers.ToList();

                OnPropertyChanged(nameof(SelectedSupplyWorkers));
            }
            else
            {
                _selectedSupplyWorkers.Clear();
                OnPropertyChanged(nameof(SelectedSupplyWorkers));
            }
        }

        private void ClearCurrentChildView()
        {
            Thread.Sleep(400);
            CurrentChildView = null;
        }

        public void SaveModifiedSupply(SupplyModel modifiedSupply)
        {
            MainViewModel.CloseDialog();

            AnimationAction = false;
            ReverseAnimationAction = true;
            BackVisibility = Visibility.Collapsed;
            IsEnabled = true;

            Thread myThread = new Thread(ClearCurrentChildView);
            myThread.Start();

            RefreshSupplyList(modifiedSupply.SupplyId);
        }
        public void AddNewSupply(SupplyModel modifiedSupply)
        {
            MainViewModel.CloseDialog();

            AnimationAction = false;
            ReverseAnimationAction = true;
            BackVisibility = Visibility.Collapsed;
            IsEnabled = true;

            Thread myThread = new Thread(ClearCurrentChildView);
            myThread.Start();

            RefreshSupplyList(modifiedSupply.SupplyId);
        }
    }
}