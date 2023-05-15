

using Kursovaya.Model.Supply;
using Kursovaya.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Kursovaya.Model.Shipping
{
    public interface IShippingRepository
    {
        void Add(ShippingModel shippingModel);
        void Edit(ShippingModel shippingModel);
        void Remove(int id, ApplicationContext context);
        ShippingModel GetById(int id, ApplicationContext context);
        List<ShippingModel> GetByAll(ApplicationContext context);
    }
}
