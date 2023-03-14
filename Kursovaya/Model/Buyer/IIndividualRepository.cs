using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model.Buyer
{
    internal interface IIndividualRepository
    {
        void Add(IndividualModel individualModel);
        void Edit(IndividualModel individualModel);
        void Remove(int id);
        IEnumerable<IndividualModel> GetByAll();
    }
}
