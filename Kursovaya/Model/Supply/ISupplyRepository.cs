using Kursovaya.Repositories;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kursovaya.Model.Supply
{
    public interface ISupplyRepository
    {
        void Add(SupplyModel supplyModel);
        void Edit(SupplyModel supplyModel);
        void Remove(int id, ApplicationContext context);
        List<SupplyModel> GetByAll(ApplicationContext context);
        SupplyModel GetById(int id, ApplicationContext context);
    }
}
