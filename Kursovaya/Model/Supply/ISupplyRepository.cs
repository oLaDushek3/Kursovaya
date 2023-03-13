using Kursovaya.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model.Supply
{
    public interface ISupplyRepository
    {
        void Add(SupplyModel supplyModel);
        void Edit(SupplyModel supplyModel);
        void Remove(int id);
        List<SupplyModel> GetByAll();
    }
}
