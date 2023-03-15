using Kursovaya.Model.Supply;
using System;
using System.Collections.Generic;

namespace Kursovaya.Repositories
{
    public class Supply_ProductRepository : ApplicationContext, ISupply_ProductRepository
    {
        public void Add(SupplyProductModel supplyProductModel)
        {
            throw new NotImplementedException();
        }

        public void Edit(SupplyProductModel supplyProductModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SupplyProductModel> GetByAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
