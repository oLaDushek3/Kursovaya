using Kursovaya.Repositories;
using System.Collections.Generic;

namespace Kursovaya.Model.Place
{
    public interface IPlaceRepository
    {
        void aAdd(PlaceModel placeModel);
        void Edit(PlaceModel placeModel);
        void Remove(int id);
        List<PlaceModel> GetByAll(ApplicationContext context);
    }
}
