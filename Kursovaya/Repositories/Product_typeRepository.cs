using Kursovaya.Model.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public void Remove(int id, ApplicationContext context)
        {
            throw new NotImplementedException();
        }

        public ProductTypeModel GetById(int id, ApplicationContext context)
        {
            throw new NotImplementedException();
        }
        public List<ProductTypeModel> GetByAll(ApplicationContext context)
        {
            List<ProductTypeModel> productTypeModel = new();
            productTypeModel = context.ProductTypes.Include(t => t.ProductsGroup).ToList();
            return productTypeModel;
        }
    }
}
