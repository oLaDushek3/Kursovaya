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
            this.Supply_Product = new HashSet<Supply_ProductModel>();
            this.Worker = new HashSet<WorkerModel>();
        }

        [Key]
        public int Supply_id { get; set; }
        public int Factory_id { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("Factory_id")]
        public virtual FactoryModel Factory { get; set; }
        public virtual ICollection<Supply_ProductModel> Supply_Product { get; set; }
        public virtual ICollection<WorkerModel> Worker { get; set; }
    }
}
