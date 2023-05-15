using Kursovaya.Repositories;
using System.Collections.Generic;
using System.Windows;

namespace Kursovaya.Model.Product
{
    internal interface IProduct_groupRepository
    {
        void Add(ProductsGroupModel productsGroupModel);
        void Edit(ProductsGroupModel productsGroupModel);
        void Remove(int id, ApplicationContext context);
        ProductsGroupModel? GetById(int id, ApplicationContext context);
        List<ProductsGroupModel> GetByAll(ApplicationContext context);
    }
}
