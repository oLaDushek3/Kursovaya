using Kursovaya.Repositories;
using System.Collections.Generic;

namespace Kursovaya.Model.Factory
{
    public interface IFactoryRepository
    {
        void Add(FactoryModel factoryModel);
        void Edit(FactoryModel factoryModel);
        void Remove(int id);
        List<FactoryModel> GetByAll(ApplicationContext context);
        FactoryModel GetById(int id, ApplicationContext context);
    }
}
