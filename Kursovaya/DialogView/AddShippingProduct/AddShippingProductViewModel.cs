using Kursovaya.Model.Place;
using Kursovaya.Model.Product;
using Kursovaya.Model.Shipping;
using Kursovaya.Model.Shipping;
using Kursovaya.Model.Worker;
using Kursovaya.Repositories;
using Kursovaya.ViewModel;
using Kursovaya.ViewModel.Shipping;
using Kursovaya.ViewModel.Shipping;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kursovaya.DialogView.AddShippingProduct
{
    public class AddShippingProductViewModel : ViewModelBase
    {
        #region Fields
        private ApplicationContext _context;
        private EditShippingViewModel _currentEditShippingViewModel;
        private AddShippingViewModel _currentAddShippingViewModel;

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
        private List<ShippingProductPlaceModel> _addShippingProductPlaces = new List<ShippingProductPlaceModel>();

        //ShippingProductPlace
        private ShippingProductModel _addShippingProduct = new ShippingProductModel();
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

                AddShippingProduct.Product = value;
            }
        }
        public int SpecifiedQuantity
        {
            get => _specifiedQuantity;
            set
            {
                _specifiedQuantity = value;
                OnPropertyChanged(nameof(SpecifiedQuantity));
                AddShippingProduct.Quantity = _specifiedQuantity;
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
        public ObservableCollection<ShippingProductPlaceModel> AddShippingProductPlaces
        {
            get => new ObservableCollection<ShippingProductPlaceModel>(_addShippingProductPlaces);
            set
            {
                _addShippingProductPlaces = new List<ShippingProductPlaceModel>(value);
                OnPropertyChanged(nameof(AddShippingProductPlaces));
            }
        }

        //ShippingProductPlace
        public ShippingProductModel AddShippingProduct
        {
            get => _addShippingProduct;
            set
            {
                _addShippingProduct = value;
                OnPropertyChanged(nameof(AddShippingProduct));
            }
        }
        #endregion Properties

        //Command
        public ICommand AddPlaceCommand { get; }
        public ICommand DeletePlaceCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand GoBackCommand { get; }

        private void ExecuteAddPlaceCommand(object? obj)
        {
            ShippingProductPlaceModel shippingProductPlaceModel = new ShippingProductPlaceModel()
            {
                Place = SelectedPlace,
                Quantity = QuantityOnPlace,
                ShippingProduct = AddShippingProduct
            };

            _addShippingProductPlaces.Add(shippingProductPlaceModel);
            OnPropertyChanged(nameof(AddShippingProductPlaces));

            ActualQuantity += QuantityOnPlace;
            UpdateResiduary();

            SelectedPlace = null;
            QuantityOnPlace = 0;
            AvailablePlaces = _context.Places.Where(p => !AddShippingProductPlaces.Select(pl => pl.Place.PlaceId).Contains(p.PlaceId)).ToList();
            OnPropertyChanged(nameof(AvailablePlaces));
        }
        private void ExecuteDeletePlaceCommand(object obj)
        {
            _addShippingProductPlaces.Remove(obj as ShippingProductPlaceModel);
            OnPropertyChanged(nameof(AddShippingProductPlaces));

            ActualQuantity -= (obj as ShippingProductPlaceModel).Quantity;
            UpdateResiduary();

            AvailablePlaces = _context.Places.Where(p => !AddShippingProductPlaces.Select(pl => pl.Place.PlaceId).Contains(p.PlaceId)).ToList();
        }
        private bool CanExecuteAddPlaceCommand(object? obj)
        {
            if (_selectedPlace != null && _quantityOnPlace != 0 && _selectedProduct != null)
                return true;
            return false;
        }
        private void ExecuteAddCommand(object? obj)
        {
            AddShippingProduct.ShippingProductPlaces = AddShippingProductPlaces.ToList();
            _currentEditShippingViewModel?.AddShippingProduct(AddShippingProduct);
            _currentAddShippingViewModel?.AddShippingProduct(AddShippingProduct);

            GoBackCommand.Execute(null);
        }
        private bool CanExecuteAddCommand(object? obj)
        {
            if (_specifiedQuantity != 0 & _residuary == 0 && _selectedProduct != null)
                return true;
            return false;
        }
        private void ExecuteGoBackCommand(object? obj)
        {
            _currentEditShippingViewModel?.CurrentShippingViewModel.MainViewModel.CloseDialog();
            _currentAddShippingViewModel?.CurrentShippingViewModel.MainViewModel.CloseDialog();
        }
        public AddShippingProductViewModel(ApplicationContext context, EditShippingViewModel editShippingViewModel)
        {
            _context = context;
            _currentEditShippingViewModel = editShippingViewModel;
            AllProducts = _context.Products.ToList();
            AvailablePlaces = _context.Places.ToList();

            AddPlaceCommand = new ViewModelCommand(ExecuteAddPlaceCommand, CanExecuteAddPlaceCommand);
            DeletePlaceCommand = new ViewModelCommand(ExecuteDeletePlaceCommand);
            AddCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
            GoBackCommand = new ViewModelCommand(ExecuteGoBackCommand);
        }

        public AddShippingProductViewModel(ApplicationContext context, AddShippingViewModel addShippingViewModel)
        {
            _context = context;
            _currentAddShippingViewModel = addShippingViewModel;
            AllProducts = _context.Products.ToList();
            AvailablePlaces = _context.Places.ToList();

            AddPlaceCommand = new ViewModelCommand(ExecuteAddPlaceCommand, CanExecuteAddPlaceCommand);
            DeletePlaceCommand = new ViewModelCommand(ExecuteDeletePlaceCommand);
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
