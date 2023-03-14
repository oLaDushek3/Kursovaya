using Kursovaya.Model;
using Kursovaya.Model.Supply;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Repositories
{
    public class Supply_ProductRepository : ApplicationContext, ISupply_ProductRepository
    {
        public void Add(Supply_ProductModel supply_ProductModel)
        {
            throw new NotImplementedException();
        }

        public void Edit(Supply_ProductModel supply_ProductModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Supply_ProductModel> GetByAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
