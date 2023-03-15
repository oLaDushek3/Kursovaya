using System.Collections.Generic;

namespace Kursovaya.Model.Shipping
{
    public interface IShipping_ProductRepository
    {
        void Add(ShippingProductModel shippingProductModel);
        void Edit(ShippingProductModel shippingProductModel);
        void Remove(int id);
        IEnumerable<ShippingProductModel> GetByAll();
    }
}
