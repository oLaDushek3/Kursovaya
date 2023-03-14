using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model.Product
{
    internal interface IProduct_groupRepository
    {
        void Add(Product_groupModel product_groupModel);
        void Edit(Product_groupModel product_groupModel);
        void Remove(int id);
        IEnumerable<Product_groupModel> GetByAll();
    }
}
