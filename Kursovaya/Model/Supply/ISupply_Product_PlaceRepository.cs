using System.Collections.Generic;

namespace Kursovaya.Model.Supply
{
    public interface ISupply_Product_PlaceRepository
    {
        void Add(SupplyProductPlaceModel supplyProductPlaceModel);
        void Edit(SupplyProductPlaceModel supplyProductPlaceModel);
        void Remove(int id);
        IEnumerable<SupplyProductPlaceModel> GetByAll();
    }
}
