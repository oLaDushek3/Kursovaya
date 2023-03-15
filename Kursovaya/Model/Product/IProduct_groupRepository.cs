using System.Collections.Generic;

namespace Kursovaya.Model.Product
{
    internal interface IProduct_groupRepository
    {
        void Add(ProductsGroupModel productsGroupModel);
        void Edit(ProductsGroupModel productsGroupModel);
        void Remove(int id);
        IEnumerable<ProductsGroupModel> GetByAll();
    }
}
