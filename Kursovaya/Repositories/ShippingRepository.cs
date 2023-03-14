using Kursovaya.Model.Shipping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Repositories
{
    public class ShippingRepository : ApplicationContext, IShippingRepository
    {
        public void Add(ShippingModel shippingModel)
        {
            throw new NotImplementedException();
        }

        public void Edit(ShippingModel shippingModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ShippingModel> GetByAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
