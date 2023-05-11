using Kursovaya.Model.Place;
using Kursovaya.Model.Product;
using Kursovaya.Model.Supply;
using Kursovaya.Model.Worker;
using Kursovaya.Repositories;
using Kursovaya.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kursovaya.DialogView.AddSupplyProduct
{
    public class AddSupplyProductViewModel : ViewModelBase
    {
        #region Fields
        private ApplicationContext _context;
        private SupplyViewModel _supplyViewModel;

        //Product
        private List<ProductModel> _allProducts;
        private ProductModel _selectedProduct;
        private int _specifiedQuantity;
        private int _actualQuantity = 0;
        private int _residuary;

        //Place
        private List<PlaceModel> _availablePlaces;
        private PlaceModel _selectedPlace;
        private int _quantityOnPlace;

        //SupplyProductPlace
        private SupplyProductPlaceModel _addSupplyProductPlaceModel = new SupplyProductPlaceModel();
        #endregion Fields

        #region Properties
        public List<ProductModel> AllProducts
        {
            get => _allProducts;
            set
            {
                _allProducts = value;
                OnPropertyChanged(nameof(AllProducts));
            }
        }
        public ProductModel SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
                AddSupplyProductPlaceModel.SupplyProduct = _context.SupplyProducts.Where(s => s.ProductId == _selectedProduct.ProductId).First();
            }
        }
        public int SpecifiedQuantity
        {
            get => _specifiedQuantity;
            set
            {
                _specifiedQuantity = value;
                OnPropertyChanged(nameof(SpecifiedQuantity));
                AddSupplyProductPlaceModel.Quantity = _specifiedQuantity;
                UpdateResiduary();
            }
        }
        public int ActualQuantity
        {
            get => _actualQuantity;
            set
            {
                _actualQuantity = value;
                OnPropertyChanged(nameof(ActualQuantity));
            }
        }
        public int Residuary
        {
            get => _residuary;
            set
            {
                _residuary = value;
                OnPropertyChanged(nameof(Residuary));
            }
        }

        //Place
        public List<PlaceModel> AvailablePlaces
        {
            get => _availablePlaces;
            set
            {
                _availablePlaces = value;
                OnPropertyChanged(nameof(AvailablePlaces));
            }
        }
        public PlaceModel SelectedPlace
        {
            get => _selectedPlace;
            set
            {
                _selectedPlace = value;
                OnPropertyChanged(nameof(SelectedPlace));
            }
        }
        public int QuantityOnPlace
        {
            get => _quantityOnPlace;
            set
            {
                _quantityOnPlace = value;
                OnPropertyChanged(nameof(QuantityOnPlace));
            }
        }

        //SupplyProductPlace
        public SupplyProductPlaceModel AddSupplyProductPlaceModel
        {
            get => _addSupplyProductPlaceModel;
            set
            {
                _addSupplyProductPlaceModel = value;
                OnPropertyChanged(nameof(AddSupplyProductPlaceModel));
            }
        }
        #endregion Properties

        //Command
        public ICommand AddPlaceCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand GoBackCommand { get; }

        public void ExecuteAddPlaceCommand(object? obj)
        {

        }
        public bool CanExecuteAddPlaceCommand(object? obj)
        {
            if (_selectedPlace != null && _quantityOnPlace != 0)
                return true;
            return false;
        }
        public void ExecuteAddCommand(object? obj)
        {

        }
        public bool CanExecuteAddCommand(object? obj)
        {
            if (_specifiedQuantity != 0 & _residuary == 0 && _addSupplyProductPlaceModel.SupplyProduct != null)
                return true;
            return false;
        }
        public void ExecuteGoBackCommand(object? obj)
        {
            _supplyViewModel.MainViewModel.CloseDialog();
        }

        public AddSupplyProductViewModel(ApplicationContext context, SupplyViewModel supplyViewModel)
        {
            _context = context;
            _supplyViewModel = supplyViewModel;
            AllProducts = _context.Products.ToList();

            AddPlaceCommand = new ViewModelCommand(ExecuteAddPlaceCommand, CanExecuteAddPlaceCommand);
            AddCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
            GoBackCommand = new ViewModelCommand(ExecuteGoBackCommand);
        }

        //Methods
        private void UpdateResiduary()
        {
            Residuary = _specifiedQuantity - _actualQuantity;
        }
    }
}
