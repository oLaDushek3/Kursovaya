using Kursovaya.Repositories;
using System.Collections.Generic;

namespace Kursovaya.Model.Product
{
    internal interface IProduct_typeRepository
    {
        void Add(ProductTypeModel productTypeModel);
        void Edit(ProductTypeModel productTypeModel);
        void Remove(int id, ApplicationContext context);
        ProductTypeModel GetById(int id, ApplicationContext context);
        List<ProductTypeModel> GetByAll(ApplicationContext context);
    }
}
