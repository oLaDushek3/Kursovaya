using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model
{
    public partial class Buyer
    {
        public Buyer()
        {
            this.Shipping = new HashSet<Shipping>();
        }

        [Key]
        public int Buyer_id { get; set; }
        public Nullable<int> Individual_id { get; set; }
        public Nullable<int> Legal_entity_id { get; set; }

        public virtual Individual Individual { get; set; }
        public virtual Legal_entity Legal_entity { get; set; }
        public virtual ICollection<Shipping> Shipping { get; set; }
    }
}
