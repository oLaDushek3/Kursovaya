using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model.Buyer
{
    public partial class Legal_entityModel
    {
        public Legal_entityModel()
        {
            Buyer = new HashSet<BuyerModel>();
            Buyer_address = new HashSet<Buyer_addressModel>();
        }

        [Key]
        public int Legal_entity_id { get; set; }
        public string organization { get; set; }
        public string Сhecking_account { get; set; }
        public string Bank { get; set; }
        public string Correspondent_account { get; set; }
        public string BIC { get; set; }
        public string RRC { get; set; }
        public string TIN { get; set; }
        public string Phone_number { get; set; }

        public virtual ICollection<BuyerModel> Buyer { get; set; }
        public virtual ICollection<Buyer_addressModel> Buyer_address { get; set; }
    }
}
