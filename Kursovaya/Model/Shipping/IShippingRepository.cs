using System.Collections.Generic;

namespace Kursovaya.Model.Shipping
{
    public interface IShippingRepository
    {
        void Add(ShippingModel shippingModel);
        void Edit(ShippingModel shippingModel);
        void Remove(int id);
        IEnumerable<ShippingModel> GetByAll();
    }
}
