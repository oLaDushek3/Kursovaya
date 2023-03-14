using Kursovaya.Model.Supply;
using Kursovaya.Model.Worker;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kursovaya.Model
{
    public partial class SupplyModel
    {
        public SupplyModel()
        {
            Supply_Product = new List<Supply_ProductModel>();
            Worker = new List<WorkerModel>();
        }

        [Key]
        public int Supply_id { get; set; }
        public int Factory_id { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("Factory_id")]
        public virtual FactoryModel Factory { get; set; }
        public virtual List<Supply_ProductModel> Supply_Product { get; set; }
        public List<WorkerModel> Worker { get; set; }
    }
}
