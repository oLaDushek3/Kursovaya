using Kursovaya.Model.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model.Supply
{
    public interface ISupply_Product_PlaceRepository
    {
        void Add(Supply_Product_PlaceModel supply_Product_PlaceModel);
        void Edit(Supply_Product_PlaceModel supply_Product_PlaceModel);
        void Remove(int id);
        IEnumerable<Supply_Product_PlaceModel> GetByAll();
    }
}
