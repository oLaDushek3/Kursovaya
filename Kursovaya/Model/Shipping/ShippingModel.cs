using Kursovaya.Model.Worker;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model.Shipping
{
    public partial class ShippingModel
    {
        public ShippingModel()
        {
            Shipping_Product = new HashSet<Shipping_ProductModel>();
            Worker = new HashSet<WorkerModel>();
        }

        [Key]
        public int Shipping_id { get; set; }
        public int Buyer_id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }

        public virtual Buyer Buyer { get; set; }
        public virtual ICollection<Shipping_ProductModel> Shipping_Product { get; set; }
        public virtual ICollection<WorkerModel> Worker { get; set; }
    }
}
