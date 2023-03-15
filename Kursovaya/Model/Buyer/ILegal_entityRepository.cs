using System.Collections.Generic;

namespace Kursovaya.Model.Buyer
{
    internal interface ILegal_entityRepository
    {
        void Add(LegalEntityModel legalEntityModel);
        void Edit(LegalEntityModel legalEntityModel);
        void Remove(int id);
        IEnumerable<LegalEntityModel> GetByAll();
    }
}
