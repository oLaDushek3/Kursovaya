using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kursovaya.Model.Shipping;

namespace Kursovaya.Model.Place
{
    public partial class PlaceModel
    {
        public PlaceModel()
        {
            Shipping_Product_Place = new HashSet<Shipping_Product_PlaceModel>();
            Supply_Product_Place = new HashSet<Supply_Product_PlaceModel>();
        }

        [Key]
        public int Place_id { get; set; }
        public int Place { get; set; }


        public virtual ICollection<Shipping_Product_PlaceModel> Shipping_Product_Place { get; set; }
        public virtual ICollection<Supply_Product_PlaceModel> Supply_Product_Place { get; set; }
    }
}
