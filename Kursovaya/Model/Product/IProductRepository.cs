using System.Collections.Generic;

namespace Kursovaya.Model.Product
{
    public interface IProductRepository
    {
        void Add(ProductModel productModel);
        void Edit(ProductModel productModel);
        void Remove(int id);
        IEnumerable<ProductModel> GetByAll();
    }
}