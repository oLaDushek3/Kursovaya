using Kursovaya.Model;
using Kursovaya.Model.Supply;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kursovaya.Repositories
{
    public class SupplyRepository : ApplicationContext, ISupplyRepository
    {
        public void Add(SupplyModel supplyModel)
        {
            throw new NotImplementedException();
        }

        public void Edit(SupplyModel supplyModel)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public List<SupplyModel> GetByAll()
        {
            ApplicationContext context = new ApplicationContext();
            List<FactoryModel> factorys = context.Factory.ToList();
            List<SupplyModel> supplys = context.Supply.ToList();

            return supplys;
        }

    }
}
