using Kursovaya.Model.Buyer;
using System;
using System.Collections.Generic;

namespace Kursovaya.Repositories
{
    public class Legal_entityRepository : ApplicationContext, ILegal_entityRepository
    {
        public void Add(LegalEntityModel legal_entityModel)
        {
            throw new NotImplementedException();
        }

        public void Edit(LegalEntityModel legal_entityModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LegalEntityModel> GetByAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
