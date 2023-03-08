using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model
{
    public partial class Shipping_Product
    {
        public Shipping_Product()
        {
            this.Shipping_Product_Place = new HashSet<Shipping_Product_Place>();
        }

        [Key]
        public int Shipping_Product_id { get; set; }
        public int Shipping_id { get; set; }
        public int Product_id { get; set; }
        public int Quantity { get; set; }

        public virtual Product Product { get; set; }
        public virtual Shipping Shipping { get; set; }
        public virtual ICollection<Shipping_Product_Place> Shipping_Product_Place { get; set; }
    }
}
