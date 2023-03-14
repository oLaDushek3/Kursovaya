using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model.Buyer
{
    internal interface ILegal_entityRepository
    {
        void Add(Legal_entityModel legal_entityModel);
        void Edit(Legal_entityModel legal_entityModel);
        void Remove(int id);
        IEnumerable<Legal_entityModel> GetByAll();
    }
}
