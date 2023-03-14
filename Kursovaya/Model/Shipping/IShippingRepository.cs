using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
