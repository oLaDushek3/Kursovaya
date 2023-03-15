using Kursovaya.Model.Product;
using System;
using System.Collections.Generic;

namespace Kursovaya.Repositories
{
    public class ProductRepository : ApplicationContext, IProductRepository
    {
        public void Add(ProductModel productModel)
        {
            throw new NotImplementedException();
        }

        public void Edit(ProductModel productModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductModel> GetByAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
