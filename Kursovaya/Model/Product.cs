using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model
{
    public partial class Product
    {
        public Product()
        {
            this.Shipping_Product = new HashSet<Shipping_Product>();
            this.Supply_Product = new HashSet<Supply_Product>();
        }

        [Key]
        public int Product_id { get; set; }
        public string Title { get; set; }
        public string Characteristic { get; set; }
        public int Products_group_Id { get; set; }
        public int Product_type_id { get; set; }
        public int Quantity { get; set; }
        public decimal Price_per_unit { get; set; }

        public virtual Product_type Product_type { get; set; }
        public virtual ICollection<Shipping_Product> Shipping_Product { get; set; }
        public virtual ICollection<Supply_Product> Supply_Product { get; set; }
    }
}
