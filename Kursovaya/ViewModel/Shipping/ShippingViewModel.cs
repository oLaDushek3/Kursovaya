using Kursovaya.DialogView;
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
    public class ShippingViewModel : ViewModelBase
    {
        public MainViewModel MainViewModel;
        #region Fields
        public ApplicationContext context = new ApplicationContext();

        private ViewModelBase? _currentChildView;
        private bool _isEnabled = true;
        private Visibility _backVisibility = Visibility.Collapsed;
        private bool _animationAction;
        private bool _reverseAnimationAction;

        //Order fields
        private IShippingRepository _shippingRepository = new ShippingRepository();
        private List<ShippingModel>? _allShipping;
        private List<ShippingModel>? _displayedShippings;
        private ShippingModel? _selectedShipping;

        private string? _searchString;
        private List<ShippingModel>? _searchShippings;

        private string? _selectedBuyerType;
        private List<ShippingModel>? _buyerSortShippings;

        private DateTime? _selectedFirstDate = null;
        private DateTime? _selectedSecondDate = null;
        private List<ShippingModel>? _dateSortShippings;

        //Selected supply fields
        private List<WorkerModel>? _selectedShippingWorkers = new List<WorkerModel>();
        private int _shippingWorkersPostId;
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
        public ObservableCollection<ShippingModel> DisplayedShippings
        {
            get => new ObservableCollection<ShippingModel>(_displayedShippings);
            set
            {
                _displayedShippings = new List<ShippingModel>(value);
                OnPropertyChanged(nameof(DisplayedShippings));
            }
        }
        public ShippingModel? SelectedShipping
        {
            get => _selectedShipping;
            set
            {
                _selectedShipping = value;
                OnPropertyChanged(nameof(SelectedShipping));
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
        public ObservableCollection<WorkerModel> SelectedShippingWorkers
        {
            get => new ObservableCollection<WorkerModel>(_selectedShippingWorkers);
            set
            {
                _selectedShippingWorkers = new List<WorkerModel>(value);
                OnPropertyChanged(nameof(SelectedShippingWorkers));
            }
        }
        public int ShippingWorkersPostId
        {
            get => _shippingWorkersPostId;
            set
            {
                _shippingWorkersPostId = value;
                OnPropertyChanged(nameof(ShippingWorkersPostId));
                SortSupplyWorkersByPost();
            }
        }
        #endregion Properties

        //Commands
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand GoBackCommand { get; }
        public ICommand DeleteShippingCommand { get; }
        public ICommand ClearSearchCommand { get; }
        public ICommand ClearSortCommand { get; }
        public ICommand SortByDateCommande { get; }
        public ICommand ClearSortByDateCommande { get; }

        //Commands execution
        private void ExecuteAddCommand(object? obj)
        {
            CurrentChildView = new AddShippingViewModel(this);
            IsEnabled = false;
            BackVisibility = Visibility.Visible;
            ReverseAnimationAction = false;
            AnimationAction = true;
        }
        private void ExecuteEditCommand(object? obj)
        {
            CurrentChildView = new EditShippingViewModel(SelectedShipping, this);
            IsEnabled = false;
            BackVisibility = Visibility.Visible;
            ReverseAnimationAction = false;
            AnimationAction = true;
        }
        private bool CanExecuteEditCommand(object? obj)
        {
            if (SelectedShipping == null) return false;
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

                Thread myThread = new(ClearCurrentChildView);
                myThread.Start();
            }
        }
        private async void ExecuteDeleteShippingCommand(object obj)
        {
            ConfirmationDialogViewModel confirmationDialogViewModel = new ConfirmationDialogViewModel(MainViewModel);
            bool result = await MainViewModel.ShowDialog(confirmationDialogViewModel);

            if (result)
            {
                _shippingRepository.Remove((obj as ShippingModel).ShippingId, context);
                RefreshSupplyList(null);
            }
        }
        private void ExecuteClearSearchCommand(object? obj)
        {
            SearchString = "";
            _searchShippings = null;
            Merger();
        }
        private void ExecuteClearSortCommand(object? obj)
        {
            SelectedBuyerType = null;
            _buyerSortShippings = null;

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
        private void ExecuteClearSortByDateCommande(object? obj)
        {
            SelectedFirstDate = null;
            SelectedSecondDate = null;
            _dateSortShippings = null;
            Merger();
        }

        //Constructor
        public ShippingViewModel(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;

            AddCommand = new ViewModelCommand(ExecuteAddCommand);
            EditCommand = new ViewModelCommand(ExecuteEditCommand, CanExecuteEditCommand);
            GoBackCommand = new ViewModelCommand(ExecuteGoBackCommand);
            DeleteShippingCommand = new ViewModelCommand(ExecuteDeleteShippingCommand);
            ClearSearchCommand = new ViewModelCommand(ExecuteClearSearchCommand);
            ClearSortCommand = new ViewModelCommand(ExecuteClearSortCommand);
            SortByDateCommande = new ViewModelCommand(ExecuteSortByDateCommande);
            ClearSortByDateCommande = new ViewModelCommand(ExecuteClearSortByDateCommande, CanExecuteClearSortByDateCommande);

            RefreshSupplyList(null);
        }

        //Methods
        private void RefreshSupplyList(int? selectedShippingId)
        {
            context = new ApplicationContext();

            _allShipping = _shippingRepository.GetByAll(context);
            _displayedShippings = _allShipping.ToList();
            OnPropertyChanged(nameof(DisplayedShippings));

            SelectedShipping = _displayedShippings.Where(s => s.ShippingId == selectedShippingId).FirstOrDefault();
            OnPropertyChanged(nameof(SelectedShipping));

            Search();
            FilterByFactory();
            SortByDate();
        }

        private void Search()
        {
            if (SearchString != "" && SearchString != null)
                _searchShippings = _allShipping.Where(s => s.BuyerNavigation.LegalEntity != null ? s.BuyerNavigation.LegalEntity.Organization.ToLower().Contains(SearchString.ToLower()) : s.BuyerNavigation.Individual.Name.ToLower().Contains(SearchString.ToLower()) | s.ShippingId.ToString().Contains(SearchString.ToLower()) | s.Date.ToString().Contains(SearchString.ToLower())).ToList();
            else
                _searchShippings = null;
            Merger();
        }
        private void FilterByFactory()
        {
            if (_selectedBuyerType != null)
            {
                if (_selectedBuyerType == "Физ. лицо")
                    _buyerSortShippings = _allShipping.Where(s => s.BuyerNavigation.Individual != null).ToList();
                else
                    _buyerSortShippings = _allShipping.Where(s => s.BuyerNavigation.LegalEntity != null).ToList();
            }
            else
                _buyerSortShippings = null;
            Merger();
        }
        private void SortByDate()
        {
            if (SelectedFirstDate != null && SelectedSecondDate != null)
                _dateSortShippings = _allShipping.Where(s => s.Date >= SelectedFirstDate & s.Date <= SelectedSecondDate).ToList();
            Merger();
        }
        private void Merger()
        {
            _displayedShippings = _allShipping;

            if (_buyerSortShippings != null && _searchShippings != null && _dateSortShippings != null)
            {
                _displayedShippings = _buyerSortShippings.Intersect(_searchShippings.Intersect(_dateSortShippings)).ToList();
                OnPropertyChanged(nameof(DisplayedShippings));
            }

            if (_buyerSortShippings != null)
            {
                _displayedShippings = _allShipping.Intersect(_buyerSortShippings).ToList();

                if (_searchShippings != null)
                {
                    _displayedShippings = _displayedShippings.Intersect(_searchShippings).ToList();
                }

                if (_dateSortShippings != null)
                {
                    _displayedShippings = _displayedShippings.Intersect(_dateSortShippings).ToList();
                }

                OnPropertyChanged(nameof(DisplayedShippings));
            }
            else
            {
                if (_searchShippings != null)
                {
                    _displayedShippings = _displayedShippings.Intersect(_searchShippings).ToList();
                }

                if (_dateSortShippings != null)
                {
                    _displayedShippings = _displayedShippings.Intersect(_dateSortShippings).ToList();
                }

                OnPropertyChanged(nameof(DisplayedShippings));
            }

            if (_buyerSortShippings == null && _searchShippings == null && _dateSortShippings == null)
            {
                _displayedShippings = _allShipping;
                OnPropertyChanged(nameof(DisplayedShippings));
            }
        }

        private void SortSupplyWorkersByPost()
        {
            if (_selectedShipping != null)
            {
                if (_shippingWorkersPostId != 0)
                    _selectedShippingWorkers = _selectedShipping.Workers.Where(w => w.PostId == _shippingWorkersPostId).ToList();
                else
                    _selectedShippingWorkers = _selectedShipping.Workers.ToList();

                OnPropertyChanged(nameof(SelectedShippingWorkers));
            }
            else
            {
                _selectedShippingWorkers.Clear();
                OnPropertyChanged(nameof(SelectedShippingWorkers));
            }
        }

        private void ClearCurrentChildView()
        {
            Thread.Sleep(400);
            CurrentChildView = null;
        }
        public void SaveModifiedShipping(ShippingModel modifiedShipping)
        {
            MainViewModel.CloseDialog();

            AnimationAction = false;
            ReverseAnimationAction = true;
            BackVisibility = Visibility.Collapsed;
            IsEnabled = true;

            Thread myThread = new Thread(ClearCurrentChildView);
            myThread.Start();

            RefreshSupplyList(modifiedShipping.ShippingId);
        }
        public void AddNewShipping(ShippingModel newShipping)
        {
            MainViewModel.CloseDialog();

            AnimationAction = false;
            ReverseAnimationAction = true;
            BackVisibility = Visibility.Collapsed;
            IsEnabled = true;

            Thread myThread = new Thread(ClearCurrentChildView);
            myThread.Start();

            RefreshSupplyList(newShipping.ShippingId);
        }
    }
}