using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kursovaya.Model.Supply
{
    public interface ISupplyRepository
    {
        void Add(SupplyModel supplyModel);
        void Edit(SupplyModel supplyModel);
        void Remove(int id);
        List<SupplyModel> GetByAll();
        SupplyModel GetById(int id);
    }
}
