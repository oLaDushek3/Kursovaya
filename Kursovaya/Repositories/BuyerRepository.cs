﻿using Kursovaya.Model.Buyer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Repositories
{
    public class BuyerRepository : ApplicationContext, IBuyerRepository
    {
        public void Add(BuyerModel buyerModel)
        {
            throw new NotImplementedException();
        }

        public void Edit(BuyerModel buyerModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BuyerModel> GetByAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
