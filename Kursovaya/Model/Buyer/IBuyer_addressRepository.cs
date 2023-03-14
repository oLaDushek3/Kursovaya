using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model.Buyer
{
    public interface IBuyer_addressRepository
    {
        void Add(Buyer_addressModel buyer_addressModel);
        void Edit(Buyer_addressModel buyer_addressModel);
        void Remove(int id);
        IEnumerable<Buyer_addressModel> GetByAll();
    }
}
