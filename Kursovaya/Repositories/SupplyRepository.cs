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
            ApplicationContext context = new ApplicationContext();
            SupplyModel? supply = context.Supplies.
                Include(s => s.Factory).

                Include(s => s.SupplyProducts).
                    ThenInclude(s => s.SupplyProductPlaces).
                        ThenInclude(s => s.Place).

                Include(s => s.SupplyProducts).
                    ThenInclude(s => s.Product).

                Include(s => s.Workers).
                    ThenInclude(s => s.Post).
                FirstOrDefault(s => s.SupplyId == 3);
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

                Include(s => s.Workers).
                    ThenInclude(s => s.Post).ToList();
                

            return supplies;
        }

        public SupplyModel GetById(int id)
        {
            ApplicationContext context = new ApplicationContext();
            SupplyModel? supply = context.Supplies.
                Include(s => s.Factory).

                Include(s => s.SupplyProducts).
                    ThenInclude(s => s.SupplyProductPlaces).
                        ThenInclude(s => s.Place).

                Include(s => s.SupplyProducts).
                    ThenInclude(s => s.Product).

                Include(s => s.Workers).
                    ThenInclude(s => s.Post).
                FirstOrDefault(s => s.SupplyId == id);

            return supply;
        }
    }
}
