using Kursovaya.Model.Place;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Repositories
{
    public class PlaceRepository : ApplicationContext, IPlaceRepository
    {
        public void aAdd(PlaceModel placeModel)
        {
            throw new NotImplementedException();
        }

        public void Edit(PlaceModel placeModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PlaceModel> GetByAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
