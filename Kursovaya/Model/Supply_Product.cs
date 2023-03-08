using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model
{
    public partial class Supply_Product
    {
        public Supply_Product()
        {
            this.Supply_Product_Place = new HashSet<Supply_Product_Place>();
        }

        [Key]
        public int Supply_Product_id { get; set; }
        public int Supply_id { get; set; }
        public int Product_id { get; set; }
        public int Quantity { get; set; }

        public virtual Product Product { get; set; }
        public virtual Supply Supply { get; set; }
        public virtual ICollection<Supply_Product_Place> Supply_Product_Place { get; set; }
    }
}
