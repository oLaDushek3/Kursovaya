using Kursovaya.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kursovaya.Repositories
{
    public class Product_groupRepository : ApplicationContext, IProduct_groupRepository
    {
        public void Add(ProductsGroupModel productsGroupModel)
        {
            throw new NotImplementedException();
        }

        public void Edit(ProductsGroupModel productsGroupModel)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id, ApplicationContext context)
        {
            ProductsGroupModel? productsGroupModel = context.ProductsGroups.Where(g => g.ProductsGroupId== id).FirstOrDefault();
            context.ProductsGroups.Remove(productsGroupModel);
        }

        public ProductsGroupModel? GetById(int id, ApplicationContext context)
        {
            ProductsGroupModel productsGroupModels = context.ProductsGroups.Where(g => g.ProductsGroupId == id).FirstOrDefault();
            return productsGroupModels;
        }

        public List<ProductsGroupModel> GetByAll(ApplicationContext context)
        {
            List<ProductsGroupModel> productsGroupModels = context.ProductsGroups.ToList();
            return productsGroupModels;
        }
    }
}
