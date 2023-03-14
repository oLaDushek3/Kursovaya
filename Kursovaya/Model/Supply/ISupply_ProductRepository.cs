using Kursovaya.Model.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model.Supply
{
    public interface ISupply_ProductRepository
    {
        void Add(Supply_ProductModel supply_ProductModel);
        void Edit(Supply_ProductModel supply_ProductModel);
        void Remove(int id);
        IEnumerable<Supply_ProductModel> GetByAll();
    }
}
