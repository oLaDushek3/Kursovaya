using Kursovaya.Model.Shipping;
using Kursovaya.Model.Supply;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kursovaya.Repositories
{
    public class ShippingRepository : ApplicationContext, IShippingRepository
    {
        public void Add(ShippingModel shippingModel)
        {
            throw new NotImplementedException();
        }

        public void Edit(ShippingModel shippingModel)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id, ApplicationContext context)
        {
            ShippingModel deletedShipping = context.Shippings.Where(s => s.ShippingId == id).First();
            context.Shippings.Remove(deletedShipping);
            context.SaveChanges();
        }

        public List<ShippingModel> GetByAll(ApplicationContext context)
        {
            List<ShippingModel> shippings = context.Shippings.
                Include(s => s.BuyerNavigation).
                    ThenInclude(b => b.Individual).

                Include(s => s.BuyerNavigation).
                    ThenInclude(b => b.LegalEntity).

                Include(s => s.ShippingProducts).
                    ThenInclude(s => s.ShippingProductPlaces).
                        ThenInclude(s => s.Place).

                Include(s => s.ShippingProducts).
                    ThenInclude(s => s.Product).

                Include(s => s.Workers).
                    ThenInclude(w => w.Post).ToList();

            return shippings;
        }

        public ShippingModel GetById(int id, ApplicationContext context)
        {
            ShippingModel? shippings = context.Shippings.
                Include(s => s.BuyerNavigation).
                    ThenInclude(b => b.Individual).

                Include(s => s.BuyerNavigation).
                    ThenInclude(b => b.LegalEntity).

                Include(s => s.ShippingProducts).
                    ThenInclude(s => s.ShippingProductPlaces).
                        ThenInclude(s => s.Place).

                Include(s => s.ShippingProducts).
                    ThenInclude(s => s.Product).

                Include(s => s.Workers).
                    ThenInclude(w => w.Post).Where(s => s.ShippingId == id).FirstOrDefault();

            return shippings;
        }
    }
}
