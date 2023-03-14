using Kursovaya.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model.Buyer
{
    public interface IBuyerRepository
    {
        void Add(BuyerModel buyerModel);
        void Edit(BuyerModel buyerModel);
        void Remove(int id);
        IEnumerable<BuyerModel> GetByAll();
    }
}
