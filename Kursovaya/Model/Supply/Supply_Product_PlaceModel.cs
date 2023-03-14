using Kursovaya.Model.Place;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model
{
    public partial class Supply_Product_PlaceModel
    {
        [Key]
        public int Supply_Product_id { get; set; }
        public int Place_id { get; set; }
        public int Quantity { get; set; }

        public virtual PlaceModel Place { get; set; }
        public virtual Supply_ProductModel Supply_Product { get; set; }
    }
}
