using Kursovaya.Model.Shipping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Repositories
{
    public class Shipping_ProductRepository : ApplicationContext, IShipping_ProductRepository
    {
        public void Add(Shipping_ProductModel shipping_ProductModel)
        {
            throw new NotImplementedException();
        }

        public void Edit(Shipping_ProductModel shipping_ProductModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Shipping_ProductModel> GetByAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
