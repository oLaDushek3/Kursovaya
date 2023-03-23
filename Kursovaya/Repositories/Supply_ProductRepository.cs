using Kursovaya.Model.Supply;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kursovaya.Repositories
{
    public class Supply_ProductRepository : ApplicationContext, ISupply_ProductRepository
    {
        public void Add(SupplyProductModel supplyProductModel)
        {
            throw new NotImplementedException();
        }

        public void Edit(SupplyProductModel supplyProductModel)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
        public List<SupplyProductModel> GetByAll(ApplicationContext context)
        {
            throw new NotImplementedException();
        }

        public SupplyProductModel GetById(int id, ApplicationContext context)
        {
            SupplyProductModel? supplyProduct = context.SupplyProducts.
                Include(s => s.SupplyProductPlaces).
                    ThenInclude(s => s.Place).

                FirstOrDefault(s => s.SupplyProductId == id);

            return supplyProduct;
        }
    }
}
