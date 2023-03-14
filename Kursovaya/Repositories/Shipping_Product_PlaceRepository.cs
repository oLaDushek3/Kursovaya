using Kursovaya.Model.Shipping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Repositories
{
    public class Shipping_Product_PlaceRepository : ApplicationContext, IShipping_Product_PlaceRepository
    {
        public void Add(Shipping_Product_PlaceModel shipping_Product_PlaceModel)
        {
            throw new NotImplementedException();
        }

        public void Edit(Shipping_Product_PlaceModel shipping_Product_PlaceModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Shipping_Product_PlaceModel> GetByAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
