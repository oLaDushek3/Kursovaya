using System.Collections.Generic;

namespace Kursovaya.Model.Product
{
    internal interface IProduct_typeRepository
    {
        void Add(ProductTypeModel productTypeModel);
        void Edit(ProductTypeModel productTypeModel);
        void Remove(int id);
        IEnumerable<ProductTypeModel> GetByAll();
    }
}
