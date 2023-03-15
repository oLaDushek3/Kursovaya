using System.Collections.Generic;

namespace Kursovaya.Model.Shipping
{
    public interface IShipping_Product_PlaceRepository
    {
        void Add(ShippingProductPlaceModel shipping_Product_PlaceModel);
        void Edit(ShippingProductPlaceModel shipping_Product_PlaceModel);
        void Remove(int id);
        IEnumerable<ShippingProductPlaceModel> GetByAll();
    }
}
