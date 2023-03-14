using Kursovaya.Model.Shipping;
using Kursovaya.Model.Supply;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
