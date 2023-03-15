using Kursovaya.Model.Supply;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Kursovaya.Repositories
{
    public class SupplyRepository : ApplicationContext, ISupplyRepository
    {
        public void Add(SupplyModel supplyModel)
        {
            throw new NotImplementedException();
        }

        public void Edit(SupplyModel supplyModel)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public List<SupplyModel> GetByAll()
        {
            ApplicationContext context = new ApplicationContext();
            List<SupplyModel>? supplies = context.Supplies.
                Include(s => s.Factory).

                Include(s => s.SupplyProducts).
                    ThenInclude(s => s.SupplyProductPlaces).
                        ThenInclude(s => s.Place).

                Include(s => s.SupplyProducts).
                    ThenInclude(s => s.Product).

                Include(s => s.Workers).ToList();

            return supplies;
        }

    }
}
