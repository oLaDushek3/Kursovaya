using System.Collections.Generic;

namespace Kursovaya.Model.Factory
{
    public interface IFactoryRepository
    {
        void Add(FactoryModel factoryModel);
        void Edit(FactoryModel factoryModel);
        void Remove(int id);
        IEnumerable<FactoryModel> GetByAll();
    }
}
