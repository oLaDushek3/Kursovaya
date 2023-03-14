using Kursovaya.Model.Buyer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Repositories
{
    public class Legal_entityRepository : ApplicationContext, ILegal_entityRepository
    {
        public void Add(Legal_entityModel legal_entityModel)
        {
            throw new NotImplementedException();
        }

        public void Edit(Legal_entityModel legal_entityModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Legal_entityModel> GetByAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
