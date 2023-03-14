using Kursovaya.Model.Buyer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Repositories
{
    public class Buyer_addressRepository : ApplicationContext, IBuyer_addressRepository
    {
        public void Add(Buyer_addressModel buyer_addressModel)
        {
            throw new NotImplementedException();
        }

        public void Edit(Buyer_addressModel buyer_addressModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Buyer_addressModel> GetByAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
