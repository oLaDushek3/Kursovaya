using Kursovaya.Model;
using Kursovaya.Model.Product;
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
            List<FactoryModel> factorys = context.Factory.ToList();
            List<ProductModel> products = context.Product.ToList();
            List<SupplyModel> supplys = context.Supply.ToList();
            MessageBox.Show(supplys[0].Factory.Address);

            return supplys;
        }

    }
}
