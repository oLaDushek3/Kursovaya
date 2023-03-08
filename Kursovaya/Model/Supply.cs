using Kursovaya.Model.Worker;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model
{
    public partial class Supply
    {
        public Supply()
        {
            this.Supply_Product = new HashSet<Supply_Product>();
            this.Worker = new HashSet<WorkerModel>();
        }

        [Key]
        public int Supply_id { get; set; }
        public int Factory_id { get; set; }
        public System.DateTime Date { get; set; }

        public virtual Factory Factory { get; set; }
        public virtual ICollection<Supply_Product> Supply_Product { get; set; }
        public virtual ICollection<WorkerModel> Worker { get; set; }
    }
}
