using Kursovaya.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Repositories
{
    public class Product_typeRepository : ApplicationContext, IProduct_typeRepository
    {
        public void Add(Product_typeModel product_typeModel)
        {
            throw new NotImplementedException();
        }

        public void Edit(Product_typeModel product_typeModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product_typeModel> GetByAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
