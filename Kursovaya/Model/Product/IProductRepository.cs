using Kursovaya.Repositories;
using System.Collections.Generic;
using System.Windows;

namespace Kursovaya.Model.Product
{
    public interface IProductRepository
    {
        void Add(ProductModel productModel);
        void Edit(ProductModel productModel);
        void Remove(int id);
        List<ProductModel> GetByAll(ApplicationContext context);
    }
}