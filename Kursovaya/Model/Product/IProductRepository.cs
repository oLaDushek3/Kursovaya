using Kursovaya.Repositories;
using System.Collections.Generic;
using System.Windows;

namespace Kursovaya.Model.Product
{
    public interface IProductRepository
    {
        void Add(ProductModel productModel);
        void Edit(ProductModel productModel);
        void Remove(int id, ApplicationContext context);
        ProductModel? GetById(int id, ApplicationContext context);
        List<ProductModel> GetByAll(ApplicationContext context);
    }
}