using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model
{
    public partial class Products_group
    {
        public Products_group()
        {
            this.Product_type = new HashSet<Product_type>();
        }

        [Key]
        public int Products_group_Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Product_type> Product_type { get; set; }
    }
}
