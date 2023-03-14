using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kursovaya.Model.Supply;

namespace Kursovaya.Model.Shipping
{
    public interface IFactoryRepository
    {
        void Add(FactoryModel factoryModel);
        void Edit(FactoryModel factoryModel);
        void Remove(int id);
        IEnumerable<FactoryModel> GetByAll();
    }
}
