using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model.Product
{
    internal interface IProduct_typeRepository
    {
        void Add(Product_typeModel product_typeModel);
        void Edit(Product_typeModel product_typeModel);
        void Remove(int id);
        IEnumerable<Product_typeModel> GetByAll();
    }
}
