using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model
{
    public partial class Supply_Product_Place
    {
        [Key]
        public int Supply_Product_id { get; set; }
        public int Place_id { get; set; }
        public int Quantity { get; set; }

        public virtual Place Place { get; set; }
        public virtual Supply_Product Supply_Product { get; set; }
    }
}
