using Kursovaya.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Repositories
{
    public class Product_groupRepository : ApplicationContext, IProduct_groupRepository
    {
        public void Add(Product_groupModel product_groupModel)
        {
            throw new NotImplementedException();
        }

        public void Edit(Product_groupModel product_groupModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product_groupModel> GetByAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
