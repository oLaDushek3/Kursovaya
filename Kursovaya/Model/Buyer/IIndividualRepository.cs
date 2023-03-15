using System.Collections.Generic;

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
