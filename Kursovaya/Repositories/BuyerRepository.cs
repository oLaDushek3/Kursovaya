using Kursovaya.Model.Buyer;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Kursovaya.Model.Supply;

namespace Kursovaya.Repositories
{
    public class BuyerRepository : ApplicationContext, IBuyerRepository
    {
        public void Add(BuyerModel buyerModel)
        {
            throw new NotImplementedException();
        }

        public void Edit(BuyerModel buyerModel)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id, ApplicationContext context)
        {
            BuyerModel deletedBuyer = context.Buyers.Where(s => s.Buyer1 == id).First();
            context.Buyers.Remove(deletedBuyer);
            context.SaveChanges();
        }

        public List<BuyerModel> GetByAll(ApplicationContext context)
        {
            List<BuyerModel> buyers = context.Buyers.
                Include(b => b.Individual).
                    ThenInclude(b => b.BuyerAddresses).
                Include(b => b.LegalEntity).
                    ThenInclude(b => b.BuyerAddresses).ToList();

            return buyers;
        }

        public BuyerModel? GetById(int id, ApplicationContext context)
        {
            BuyerModel? buyer = context.Buyers.Where(b => b.Buyer1 == id).
                Include(b => b.Individual).
                    ThenInclude(b => b.BuyerAddresses).
                Include(b => b.LegalEntity).
                    ThenInclude(b => b.BuyerAddresses).FirstOrDefault();

            return buyer;
        }
    }
}
