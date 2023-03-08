using Kursovaya.Model.Worker;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model
{
    public partial class Shipping
    {
        public Shipping()
        {
            this.Shipping_Product = new HashSet<Shipping_Product>();
            this.Worker = new HashSet<WorkerModel>();
        }

        [Key]
        public int Shipping_id { get; set; }
        public int Buyer_id { get; set; }
        public System.DateTime Date { get; set; }
        public decimal Amount { get; set; }

        public virtual Buyer Buyer { get; set; }
        public virtual ICollection<Shipping_Product> Shipping_Product { get; set; }
        public virtual ICollection<WorkerModel> Worker { get; set; }
    }
}
