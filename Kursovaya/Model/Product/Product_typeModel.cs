using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model.Product
{
    public partial class Product_typeModel
    {
        public Product_typeModel()
        {
            Product = new HashSet<ProductModel>();
        }

        [Key]
        public int Product_type_id { get; set; }
        public string Title { get; set; }
        public int Products_group_Id { get; set; }

        public virtual ICollection<ProductModel> Product { get; set; }
        public virtual Products_groupModel Products_group { get; set; }
    }
}
