using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model.Product
{
    public partial class Products_groupModel
    {
        public Products_groupModel()
        {
            Product_type = new HashSet<Product_typeModel>();
        }

        [Key]
        public int Products_group_Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Product_typeModel> Product_type { get; set; }
    }
}
