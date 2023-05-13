using Kursovaya.Model.Place;
using Kursovaya.Model.Product;
using Kursovaya.Model.Supply;
using Kursovaya.Model.Worker;
using Kursovaya.Repositories;
using Kursovaya.ViewModel;
using Kursovaya.ViewModel.Supply;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private EditSupplyViewModel _currentEditSupplyViewModel;
        private AddSupplyViewModel _currentAddSupplyViewModel;

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
        private List<SupplyProductPlaceModel> _addSupplyProductPlaces = new List<SupplyProductPlaceModel>();

        //SupplyProductPlace
        private SupplyProductModel _addSupplyProduct = new SupplyProductModel();
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

                AddSupplyProduct.Product = value;
            }
        }
        public int SpecifiedQuantity
        {
            get => _specifiedQuantity;
            set
            {
                _specifiedQuantity = value;
                OnPropertyChanged(nameof(SpecifiedQuantity));
                AddSupplyProduct.Quantity = _specifiedQuantity;
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
        public ObservableCollection<SupplyProductPlaceModel> AddSupplyProductPlaces
        {
            get => new ObservableCollection<SupplyProductPlaceModel>(_addSupplyProductPlaces);
            set
            {
                _addSupplyProductPlaces = new List<SupplyProductPlaceModel>(value);
                OnPropertyChanged(nameof(AddSupplyProductPlaces));
            }
        }

        //SupplyProductPlace
        public SupplyProductModel AddSupplyProduct
        {
            get => _addSupplyProduct;
            set
            {
                _addSupplyProduct = value;
                OnPropertyChanged(nameof(AddSupplyProduct));
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
            SupplyProductPlaceModel supplyProductPlaceModel = new SupplyProductPlaceModel()
            {
                Place = SelectedPlace,
                Quantity = QuantityOnPlace,
                SupplyProduct = AddSupplyProduct
            };

            _addSupplyProductPlaces.Add(supplyProductPlaceModel);
            OnPropertyChanged(nameof(AddSupplyProductPlaces));

            ActualQuantity += QuantityOnPlace;
            UpdateResiduary();

            SelectedPlace = null;
            QuantityOnPlace = 0;
            AvailablePlaces = _context.Places.Where(p => !AddSupplyProductPlaces.Select(pl => pl.Place.PlaceId).Contains(p.PlaceId)).ToList();
            OnPropertyChanged(nameof(AvailablePlaces));
        }
        private void ExecuteDeletePlaceCommand(object obj)
        {
            _addSupplyProductPlaces.Remove(obj as SupplyProductPlaceModel);
            OnPropertyChanged(nameof(AddSupplyProductPlaces));

            ActualQuantity -= (obj as SupplyProductPlaceModel).Quantity;
            UpdateResiduary();

            AvailablePlaces = _context.Places.Where(p => !AddSupplyProductPlaces.Select(pl => pl.Place.PlaceId).Contains(p.PlaceId)).ToList();
        }
        private bool CanExecuteAddPlaceCommand(object? obj)
        {
            if (_selectedPlace != null && _quantityOnPlace != 0 && _selectedProduct != null)
                return true;
            return false;
        }
        private void ExecuteAddCommand(object? obj)
        {
            AddSupplyProduct.SupplyProductPlaces = AddSupplyProductPlaces.ToList();
            _currentEditSupplyViewModel?.AddSupplyProduct(AddSupplyProduct);
            _currentAddSupplyViewModel?.AddSupplyProduct(AddSupplyProduct);

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
            _currentEditSupplyViewModel?.currentSupplyViewModel.MainViewModel.CloseDialog();
            _currentAddSupplyViewModel?.currentSupplyViewModel.MainViewModel.CloseDialog();
        }
        public AddSupplyProductViewModel(ApplicationContext context, EditSupplyViewModel editSupplyViewModel)
        {
            _context = context;
            _currentEditSupplyViewModel = editSupplyViewModel;
            AllProducts = _context.Products.ToList();
            AvailablePlaces = _context.Places.ToList();

            AddPlaceCommand = new ViewModelCommand(ExecuteAddPlaceCommand, CanExecuteAddPlaceCommand);
            DeletePlaceCommand = new ViewModelCommand(ExecuteDeletePlaceCommand);
            AddCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
            GoBackCommand = new ViewModelCommand(ExecuteGoBackCommand);
        }

        public AddSupplyProductViewModel(ApplicationContext context, AddSupplyViewModel addSupplyViewModel)
        {
            _context = context;
            _currentAddSupplyViewModel = addSupplyViewModel;
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
