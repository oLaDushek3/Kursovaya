using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kursovaya.Model.Shipping;

namespace Kursovaya.Model
{
    public partial class Place
    {
        public Place()
        {
            this.Shipping_Product_Place = new HashSet<Shipping_Product_PlaceModel>();
            this.Supply_Product_Place = new HashSet<Supply_Product_PlaceModel>();
        }

        [Key]
        public int Place_id { get; set; }
        public int Section_id { get; set; }


        [ForeignKey("Section_id")]
        public virtual Section Section { get; set; }
        public virtual ICollection<Shipping_Product_PlaceModel> Shipping_Product_Place { get; set; }
        public virtual ICollection<Supply_Product_PlaceModel> Supply_Product_Place { get; set; }
    }
}
