using Kursovaya.Repositories;
using System.Collections.Generic;

namespace Kursovaya.Model.Supply
{
    public interface ISupply_ProductRepository
    {
        void Add(SupplyProductModel supplyProductModel);
        void Edit(SupplyProductModel supplyProductModel);
        void Remove(int id);
        List<SupplyProductModel> GetByAll(ApplicationContext contex);
        SupplyProductModel GetById(int id, ApplicationContext contex);
    }
}
