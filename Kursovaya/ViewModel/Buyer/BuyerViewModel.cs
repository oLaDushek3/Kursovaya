using Kursovaya.DialogView;
using Kursovaya.Model.Buyer;
using Kursovaya.Model.Shipping;
using Kursovaya.Repositories;
using Kursovaya.ViewModel.Shipping;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Kursovaya.ViewModel.Buyer
{
    public class BuyerViewModel : ViewModelBase
    {
        public MainViewModel MainViewModel;
        #region Fields
        public ApplicationContext context = new ApplicationContext();

        private ViewModelBase? _currentChildView;
        private bool _isEnabled = true;
        private Visibility _backVisibility = Visibility.Collapsed;
        private bool _animationAction;
        private bool _reverseAnimationAction;

        //Buyer fields
        private IBuyerRepository _buyerRepository = new BuyerRepository();
        private List<BuyerModel>? _allBuyer;
        private List<BuyerModel>? _displayedBuyer;
        private BuyerModel? _selectedBuyer;

        private string? _searchString;
        private List<BuyerModel>? _searchBuyer;

        private string? _selectedBuyerType;
        private List<BuyerModel>? _typeSortBuyer;
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

        //Buyer properties
        public ObservableCollection<BuyerModel> DisplayedBuyer
        {
            get => new ObservableCollection<BuyerModel>(_displayedBuyer);
            set
            {
                _displayedBuyer = new List<BuyerModel>(value);
                OnPropertyChanged(nameof(DisplayedBuyer));
            }
        }
        public BuyerModel? SelectedBuyer
        {
            get => _selectedBuyer;
            set
            {
                _selectedBuyer = value;
                OnPropertyChanged(nameof(SelectedBuyer));
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
        #endregion

        //Commands
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand GoBackCommand { get; }
        public ICommand DeleteBuyerCommand { get; }
        public ICommand ClearSearchCommand { get; }
        public ICommand ClearSortCommand { get; }
        public ICommand ClearSortByDateCommande { get; }

        //Commands execution
        private void ExecuteAddCommand(object? obj)
        {
            CurrentChildView = new AddBuyerViewModel(this);
            IsEnabled = false;
            BackVisibility = Visibility.Visible;
            ReverseAnimationAction = false;
            AnimationAction = true;
        }
        private void ExecuteEditCommand(object? obj)
        {
            CurrentChildView = new EditBuyerViewModel(SelectedBuyer, this);
            IsEnabled = false;
            BackVisibility = Visibility.Visible;
            ReverseAnimationAction = false;
            AnimationAction = true;
        }
        private bool CanExecuteEditCommand(object? obj)
        {
            if (SelectedBuyer == null) return false;
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
        private async void ExecuteDeleteBuyerCommand(object obj)
        {
            ConfirmationDialogViewModel confirmationDialogViewModel = new(MainViewModel);
            bool result = await MainViewModel.ShowDialog(confirmationDialogViewModel);

            if (result)
            {
                _buyerRepository.Remove((obj as BuyerModel).Buyer1, context);
                RefreshBuyersList(null);
            }
        }
        private void ExecuteClearSearchCommand(object? obj)
        {
            SearchString = "";
            _searchBuyer = null;
            Merger();
        }
        private void ExecuteClearSortCommand(object? obj)
        {
            SelectedBuyerType = null;
            _typeSortBuyer = null;

            Merger();
        }

        //Constructor
        public BuyerViewModel(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;

            AddCommand = new ViewModelCommand(ExecuteAddCommand);
            EditCommand = new ViewModelCommand(ExecuteEditCommand, CanExecuteEditCommand);
            GoBackCommand = new ViewModelCommand(ExecuteGoBackCommand);
            DeleteBuyerCommand = new ViewModelCommand(ExecuteDeleteBuyerCommand);
            ClearSearchCommand = new ViewModelCommand(ExecuteClearSearchCommand);
            ClearSortCommand = new ViewModelCommand(ExecuteClearSortCommand);

            RefreshBuyersList(null);
        }

        //Methods
        private void RefreshBuyersList(int? selectedBuyerId)
        {
            context = new ApplicationContext();

            _allBuyer = _buyerRepository.GetByAll(context);
            _displayedBuyer = _allBuyer.ToList();
            OnPropertyChanged(nameof(DisplayedBuyer));

            SelectedBuyer = _displayedBuyer.Where(s => s.Buyer1 == selectedBuyerId).FirstOrDefault();
            OnPropertyChanged(nameof(SelectedBuyer));

            Search();
            FilterByFactory();
        }

        private void Search()
        {
            if (SearchString != "" && SearchString != null)
                _searchBuyer = _allBuyer.Where(s => s.Buyer1.ToString().Contains(SearchString.ToLower()) |
                s.LegalEntity != null ? s.LegalEntity.Organization.ToLower().Contains(SearchString.ToLower()) : s.Individual.Name.ToLower().Contains(SearchString.ToLower()) |
                s.LegalEntity != null ? s.LegalEntity.BuyerAddresses.Last().Adress.ToLower().Contains(SearchString.ToLower()) : s.Individual.BuyerAddresses.Last().Adress.ToLower().Contains(SearchString.ToLower())).ToList();
            else
                _searchBuyer = null;
            Merger();
        }
        private void FilterByFactory()
        {
            if (_selectedBuyerType != null)
            {
                if (_selectedBuyerType == "Физ. лицо")
                    _typeSortBuyer = _allBuyer.Where(s => s.Individual != null).ToList();
                else
                    _typeSortBuyer = _allBuyer.Where(s => s.LegalEntity != null).ToList();
            }
            else
                _typeSortBuyer = null;
            Merger();
        }
        private void Merger()
        {
            _displayedBuyer = _allBuyer;

            if (_typeSortBuyer != null && _searchBuyer != null)
            {
                _displayedBuyer = _typeSortBuyer.Intersect(_searchBuyer).ToList();
                OnPropertyChanged(nameof(DisplayedBuyer));
            }

            if (_typeSortBuyer != null)
            {
                _displayedBuyer = _allBuyer.Intersect(_typeSortBuyer).ToList();

                if (_searchBuyer != null)
                {
                    _displayedBuyer = _displayedBuyer.Intersect(_searchBuyer).ToList();
                }

                OnPropertyChanged(nameof(DisplayedBuyer));
            }
            else
            {
                if (_searchBuyer != null)
                {
                    _displayedBuyer = _displayedBuyer.Intersect(_searchBuyer).ToList();
                }

                OnPropertyChanged(nameof(DisplayedBuyer));
            }

            if (_typeSortBuyer == null && _searchBuyer == null)
            {
                _displayedBuyer = _allBuyer;
                OnPropertyChanged(nameof(DisplayedBuyer));
            }
        }

        private void ClearCurrentChildView()
        {
            Thread.Sleep(400);
            CurrentChildView = null;
        }
        public void SaveAndCloseCUView(BuyerModel buyerModel)
        {
            MainViewModel.CloseDialog();

            AnimationAction = false;
            ReverseAnimationAction = true;
            BackVisibility = Visibility.Collapsed;
            IsEnabled = true;

            Thread myThread = new Thread(ClearCurrentChildView);
            myThread.Start();

            RefreshBuyersList(buyerModel.Buyer1);
        }
    }
}
