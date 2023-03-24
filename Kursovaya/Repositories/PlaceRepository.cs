using Kursovaya.Model.Place;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }


        public List<PlaceModel> GetByAll(ApplicationContext context)
        {
            List<PlaceModel> places = context.Places.ToList();
            return places;
        }
    }
}
