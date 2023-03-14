using Kursovaya.Model.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model
{
    public partial class Supply_ProductModel
    {
        public Supply_ProductModel()
        {
            this.Supply_Product_Place = new HashSet<Supply_Product_PlaceModel>();
        }

        [Key]
        public int Supply_Product_id { get; set; }
        public int Supply_id { get; set; }
        public int Product_id { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("Product_id")]
        public virtual ProductModel Product { get; set; }
        [ForeignKey("Supply_id")]
        public virtual SupplyModel Supply { get; set; }
        public virtual ICollection<Supply_Product_PlaceModel> Supply_Product_Place { get; set; }
    }
}
