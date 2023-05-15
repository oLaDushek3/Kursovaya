using Kursovaya.DialogView;
using Kursovaya.Model.Product;
using Kursovaya.Repositories;
using Kursovaya.ViewModel.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Windows.Input;

namespace Kursovaya.ViewModel.Product
{
    public class EditProductViewModel : ViewModelBase
    {
        #region Fields
        private ProductViewModel _currentProductViewModel;

        ApplicationContext context = new ApplicationContext();

        private IProductRepository _productRepository = new ProductRepository();
        private ProductModel _selectedProduct;
        private ProductModel _editableProduct = new();

        private List<ProductTypeModel> _productTypeList;
        private IProduct_typeRepository _productTypeRepository = new Product_typeRepository();
        #endregion Fields

        #region Properties
        public ProductModel EditableProduct
        {
            get => _editableProduct;
            set
            {
                _editableProduct = value;
                OnPropertyChanged(nameof(EditableProduct));
            }
        }
        public List<ProductTypeModel> ProductTypeList
        {
            get => _productTypeList;
            set
            {
                _productTypeList = value;
                OnPropertyChanged(nameof(ProductTypeList));
            }
        }
        #endregion Properties

        //Commands
        public ICommand SaveCommand { get; }

        //Commands execution
        public async void ExecuteSaveCommand(object? obj)
        {
            ConfirmationDialogViewModel confirmationDialogViewModel = new ConfirmationDialogViewModel(_currentProductViewModel.MainViewModel);
            bool result = await _currentProductViewModel.MainViewModel.ShowDialog(confirmationDialogViewModel);

            if (result)
            {
                context.SaveChanges();
                _currentProductViewModel.SaveAndCloseCUView(_editableProduct);
            }
        }

        //Constructor
        public EditProductViewModel(ProductModel selectedProduct, ProductViewModel currentProductViewModel)
        {
            _currentProductViewModel = currentProductViewModel;
            _selectedProduct = selectedProduct;
            _editableProduct = _productRepository.GetById(selectedProduct.ProductId, context);

            ProductTypeList = _productTypeRepository.GetByAll(context);
            SaveCommand = new ViewModelCommand(ExecuteSaveCommand);
        }
    }
}
