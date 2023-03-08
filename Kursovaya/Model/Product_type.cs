using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model
{
    public partial class Product_type
    {
        public Product_type()
        {
            this.Product = new HashSet<Product>();
        }

        [Key]
        public int Product_type_id { get; set; }
        public string Title { get; set; }
        public int Products_group_Id { get; set; }

        public virtual ICollection<Product> Product { get; set; }
        public virtual Products_group Products_group { get; set; }
    }
}
