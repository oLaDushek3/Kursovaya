using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kursovaya.Model.Shipping;

namespace Kursovaya.Model.Product
{
    public partial class ProductModel
    {
        public ProductModel()
        {
            Shipping_Product = new HashSet<Shipping_ProductModel>();
            Supply_Product = new HashSet<Supply_ProductModel>();
        }

        [Key]
        public int Product_id { get; set; }
        public string Title { get; set; }
        public string Characteristic { get; set; }
        public int Product_type_id { get; set; }
        public int Quantity { get; set; }
        public decimal Price_per_unit { get; set; }

        [ForeignKey("Product_type_id")]
        public virtual Product_typeModel Product_type { get; set; }
        public virtual ICollection<Shipping_ProductModel> Shipping_Product { get; set; }
        public virtual ICollection<Supply_ProductModel> Supply_Product { get; set; }
    }
}
