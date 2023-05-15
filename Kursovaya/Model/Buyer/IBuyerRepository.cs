using Kursovaya.Repositories;
using System.Collections.Generic;

namespace Kursovaya.Model.Buyer
{
    public interface IBuyerRepository
    {
        void Add(BuyerModel buyerModel);
        void Edit(BuyerModel buyerModel);
        void Remove(int id, ApplicationContext context);
        BuyerModel? GetById(int id, ApplicationContext context);
        List<BuyerModel> GetByAll(ApplicationContext context);
    }
}
