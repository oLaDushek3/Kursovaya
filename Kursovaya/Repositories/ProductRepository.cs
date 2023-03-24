using Kursovaya.Model.Product;
using Kursovaya.Model.Supply;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProductModel> GetByAll(ApplicationContext context)
        {
            List<ProductModel>? products = context.Products.
                Include(p => p.ProductType).
                    ThenInclude(t => t.ProductsGroup).ToList();

            return products;
        }
    }
}
