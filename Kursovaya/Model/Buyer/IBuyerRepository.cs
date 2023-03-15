using System.Collections.Generic;

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
