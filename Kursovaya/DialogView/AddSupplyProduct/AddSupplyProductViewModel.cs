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

namespace Kursovaya.DialogView.AddSupplyProduct
{
    public class AddSupplyProductViewModel : ViewModelBase
    {
        #region Fields
        private ApplicationContext _context;
        private List<ProductModel> _allProducts;
        private ProductModel _selectedProduct;
        private string _specifiedQuantity = "fff";
        private int _actualQuantity = 0;
        private int _residuary;

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
            }
        }
        public string SpecifiedQuantity
        {
            get => _specifiedQuantity;
            set
            {
                _specifiedQuantity = value;
                OnPropertyChanged(nameof(SpecifiedQuantity));
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

        #endregion Properties
        public AddSupplyProductViewModel(ApplicationContext context)
        {
            _context = context;
            AllProducts = _context.Products.ToList();
        }
    }
}
