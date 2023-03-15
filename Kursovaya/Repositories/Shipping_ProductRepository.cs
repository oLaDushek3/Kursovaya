using Kursovaya.Model.Shipping;
using System;
using System.Collections.Generic;

namespace Kursovaya.Repositories
{
    public class Shipping_ProductRepository : ApplicationContext, IShipping_ProductRepository
    {
        public void Add(ShippingProductModel shippingProductModel)
        {
            throw new NotImplementedException();
        }

        public void Edit(ShippingProductModel shippingProductModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ShippingProductModel> GetByAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
