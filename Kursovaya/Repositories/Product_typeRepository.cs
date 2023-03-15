using Kursovaya.Model.Product;
using System;
using System.Collections.Generic;

namespace Kursovaya.Repositories
{
    public class Product_typeRepository : ApplicationContext, IProduct_typeRepository
    {
        public void Add(ProductTypeModel productTypeModel)
        {
            throw new NotImplementedException();
        }

        public void Edit(ProductTypeModel productTypeModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductTypeModel> GetByAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
