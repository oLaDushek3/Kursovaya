using Kursovaya.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model.Place
{
    public interface IPlaceRepository
    {
        void aAdd(PlaceModel placeModel);
        void Edit(PlaceModel placeModel);
        void Remove(int id);
        IEnumerable<PlaceModel> GetByAll();
    }
}
