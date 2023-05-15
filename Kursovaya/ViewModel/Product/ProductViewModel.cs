using Kursovaya.DialogView;
using Kursovaya.Model.Buyer;
using Kursovaya.Model.Product;
using Kursovaya.Model.Product;
using Kursovaya.Repositories;
using Kursovaya.ViewModel.Buyer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Kursovaya.ViewModel.Product
{
    public class ProductViewModel : ViewModelBase
    {
        public MainViewModel MainViewModel;
        #region Fields
        public ApplicationContext context = new ApplicationContext();

        private ViewModelBase? _currentChildView;
        private bool _isEnabled = true;
        private Visibility _backVisibility = Visibility.Collapsed;
        private bool _animationAction;
        private bool _reverseAnimationAction;

        //Product fields
        private IProductRepository _productRepository = new ProductRepository();
        private List<ProductModel>? _allProduct;
        private List<ProductModel>? _displayedProduct;
        private ProductModel? _selectedProduct;

        private string? _searchString;
        private List<ProductModel>? _searchProduct;

        private string? _selectedProductType;
        private List<ProductModel>? _typeSortProduct;
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

        //Product properties
        public ObservableCollection<ProductModel> DisplayedProduct
        {
            get => new ObservableCollection<ProductModel>(_displayedProduct);
            set
            {
                _displayedProduct = new List<ProductModel>(value);
                OnPropertyChanged(nameof(DisplayedProduct));
            }
        }
        public ProductModel? SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
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

        public List<string> ProductTypeList
        {
            get
            {
                return new List<string>() { "Физ. лицо", "Юр. Лицо" };
            }
        }
        public string SelectedProductType
        {
            get => _selectedProductType;
            set
            {
                _selectedProductType = value;
                OnPropertyChanged(nameof(SelectedProductType));
                FilterByFactory();
            }
        }
        #endregion

        //Commands
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand GoBackCommand { get; }
        public ICommand DeleteProductCommand { get; }
        public ICommand ClearSearchCommand { get; }
        public ICommand ClearSortCommand { get; }
        public ICommand ClearSortByDateCommande { get; }

        //Commands execution
        private void ExecuteAddCommand(object? obj)
        {
            //CurrentChildView = new AddBuyerViewModel(this);
            IsEnabled = false;
            BackVisibility = Visibility.Visible;
            ReverseAnimationAction = false;
            AnimationAction = true;
        }
        private void ExecuteEditCommand(object? obj)
        {
            //CurrentChildView = new EditBuyerViewModel(SelectedBuyer, this);
            IsEnabled = false;
            BackVisibility = Visibility.Visible;
            ReverseAnimationAction = false;
            AnimationAction = true;
        }
        private bool CanExecuteEditCommand(object? obj)
        {
            if (SelectedProduct == null) return false;
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
        private async void ExecuteDeleteProductCommand(object obj)
        {
            ConfirmationDialogViewModel confirmationDialogViewModel = new(MainViewModel);
            bool result = await MainViewModel.ShowDialog(confirmationDialogViewModel);

            if (result)
            {
                _productRepository.Remove((obj as ProductModel).ProductId, context);
                RefreshProductsList(null);
            }
        }
        private void ExecuteClearSearchCommand(object? obj)
        {
            SearchString = "";
            _searchProduct = null;
            Merger();
        }
        private void ExecuteClearSortCommand(object? obj)
        {
            SelectedProductType = null;
            _typeSortProduct = null;

            Merger();
        }

        //Constructor
        public ProductViewModel(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;

            AddCommand = new ViewModelCommand(ExecuteAddCommand);
            EditCommand = new ViewModelCommand(ExecuteEditCommand, CanExecuteEditCommand);
            GoBackCommand = new ViewModelCommand(ExecuteGoBackCommand);
            ClearSearchCommand = new ViewModelCommand(ExecuteClearSearchCommand);
            ClearSortCommand = new ViewModelCommand(ExecuteClearSortCommand);

            RefreshProductsList(null);
        }

        //Methods
        private void RefreshProductsList(int? selectedProductId)
        {
            context = new ApplicationContext();

            _allProduct = _productRepository.GetByAll(context);
            _displayedProduct = _allProduct.ToList();
            OnPropertyChanged(nameof(DisplayedProduct));

            SelectedProduct = _displayedProduct.Where(p => p.ProductId == selectedProductId).FirstOrDefault();
            OnPropertyChanged(nameof(SelectedProduct));

            Search();
            FilterByFactory();
        }

        private void Search()
        {

            if (SearchString != "" && SearchString != null) 
                _searchProduct = _allProduct.Where(p => p.ProductId.ToString().Contains(SearchString.ToLower()) | p.Title.ToLower().Contains(SearchString.ToLower()) | p.Characteristic.ToLower().Contains(SearchString.ToLower()) | p.ProductType.ProductsGroup.Name.ToLower().Contains(SearchString.ToLower()) | p.ProductType.Title.ToLower().Contains(SearchString.ToLower())).ToList();      
            else
                _searchProduct = null;
            Merger();
        }
        private void FilterByFactory()
        {
            //if (_selectedBuyerType != null)
            //{
            //    if (_selectedBuyerType == "Физ. лицо")
            //        _typeSortProduct = _allProduct.Where(s => s.Individual != null).ToList();
            //    else
            //        _typeSortProduct = _allProduct.Where(s => s.LegalEntity != null).ToList();
            //}
            //else
            //    _typeSortProduct = null;
            //Merger();
        }
        private void Merger()
        {
            _displayedProduct = _allProduct;

            if (_typeSortProduct != null && _searchProduct != null)
            {
                _displayedProduct = _typeSortProduct.Intersect(_searchProduct).ToList();
                OnPropertyChanged(nameof(DisplayedProduct));
            }

            if (_typeSortProduct != null)
            {
                _displayedProduct = _allProduct.Intersect(_typeSortProduct).ToList();

                if (_searchProduct != null)
                {
                    _displayedProduct = _displayedProduct.Intersect(_searchProduct).ToList();
                }

                OnPropertyChanged(nameof(DisplayedProduct));
            }
            else
            {
                if (_searchProduct != null)
                {
                    _displayedProduct = _displayedProduct.Intersect(_searchProduct).ToList();
                }

                OnPropertyChanged(nameof(DisplayedProduct));
            }

            if (_typeSortProduct == null && _searchProduct == null)
            {
                _displayedProduct = _allProduct;
                OnPropertyChanged(nameof(DisplayedProduct));
            }
        }

        private void ClearCurrentChildView()
        {
            Thread.Sleep(400);
            CurrentChildView = null;
        }
        public void SaveAndCloseCUView(ProductModel productModel)
        {
            MainViewModel.CloseDialog();

            AnimationAction = false;
            ReverseAnimationAction = true;
            BackVisibility = Visibility.Collapsed;
            IsEnabled = true;

            Thread myThread = new Thread(ClearCurrentChildView);
            myThread.Start();

            RefreshProductsList(productModel.ProductId);
        }
    }
}