using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model.Shipping
{
    public interface IShipping_Product_PlaceRepository
    {
        void Add(Shipping_Product_PlaceModel shipping_Product_PlaceModel);
        void Edit(Shipping_Product_PlaceModel shipping_Product_PlaceModel);
        void Remove(int id);
        IEnumerable<Shipping_Product_PlaceModel> GetByAll();
    }
}
