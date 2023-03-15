using Kursovaya.Model.Factory;
using System;
using System.Collections.Generic;

namespace Kursovaya.Repositories
{
    public class FactoryRepository : ApplicationContext, IFactoryRepository
    {
        public void Add(FactoryModel factoryModel)
        {
            throw new NotImplementedException();
        }

        public void Edit(FactoryModel factoryModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FactoryModel> GetByAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
