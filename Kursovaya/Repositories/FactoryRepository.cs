using Kursovaya.Model.Factory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }


        public List<FactoryModel> GetByAll(ApplicationContext context)
        {
            List<FactoryModel> factorys = context.Factories.
                Include(f => f.Supplies).ToList();

            return factorys;
        }

        public FactoryModel GetById(int id, ApplicationContext context)
        {
            FactoryModel factory = context.Factories.Where(f => f.FactoryId == id).FirstOrDefault();
            return factory;
        }
    }
}
