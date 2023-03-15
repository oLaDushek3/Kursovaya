using System.Collections.Generic;

namespace Kursovaya.Model.Buyer
{
    public interface IBuyer_addressRepository
    {
        void Add(BuyerAddressModel buyerAddressModel);
        void Edit(BuyerAddressModel buyerAddressModel);
        void Remove(int id);
        IEnumerable<BuyerAddressModel> GetByAll();
    }
}
