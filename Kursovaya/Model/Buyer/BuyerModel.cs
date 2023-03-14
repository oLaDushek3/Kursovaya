using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model.Buyer
{
    public partial class BuyerModel
    {
        public BuyerModel()
        {
            Shipping = new HashSet<Shipping>();
        }

        [Key]
        public int Buyer_id { get; set; }
        public int? Individual_id { get; set; }
        public int? Legal_entity_id { get; set; }

        public virtual IndividualModel Individual { get; set; }
        public virtual Legal_entityModel Legal_entity { get; set; }
        public virtual ICollection<Shipping> Shipping { get; set; }
    }
}
