using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model.Shipping
{
    public interface IShipping_ProductRepository
    {
        void Add(Shipping_ProductModel shipping_ProductModel);
        void Edit(Shipping_ProductModel shipping_ProductModel);
        void Remove(int id);
        IEnumerable<Shipping_ProductModel> GetByAll();
    }
}
