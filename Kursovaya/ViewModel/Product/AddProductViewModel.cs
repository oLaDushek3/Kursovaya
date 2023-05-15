using Kursovaya.DialogView;
using Kursovaya.Model.Product;
using Kursovaya.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kursovaya.ViewModel.Product
{
    public class AddProductViewModel : ViewModelBase
    {
        #region Fields
        private ProductViewModel _currentProductViewModel;

        ApplicationContext context = new ApplicationContext();

        private ProductModel _createdProduct = new();

        private List<ProductTypeModel> _productTypeList;
        private IProduct_typeRepository _productTypeRepository = new Product_typeRepository();
        #endregion Fields

        #region Properties
        public ProductModel CreatedProduct
        {
            get => _createdProduct;
            set
            {
                _createdProduct = value;
                OnPropertyChanged(nameof(CreatedProduct));
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
            context.Products.Add(CreatedProduct);
            context.SaveChanges();
            _currentProductViewModel.SaveAndCloseCUView(_createdProduct);
        }

        //Constructor
        public AddProductViewModel(ProductViewModel currentProductViewModel)
        {
            _currentProductViewModel = currentProductViewModel;

            ProductTypeList = _productTypeRepository.GetByAll(context);
            SaveCommand = new ViewModelCommand(ExecuteSaveCommand);
        }
    }
}
