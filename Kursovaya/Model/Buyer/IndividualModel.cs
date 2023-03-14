using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model.Buyer
{
    public partial class IndividualModel
    {
        public IndividualModel()
        {
            Buyer = new HashSet<BuyerModel>();
            Buyer_address = new HashSet<Buyer_addressModel>();
        }

        [Key]
        public int Individual_id { get; set; }
        public string Name { get; set; }
        public string surname { get; set; }
        public string Series_passport_number { get; set; }
        public string Phone_Number { get; set; }

        public virtual ICollection<BuyerModel> Buyer { get; set; }
        public virtual ICollection<Buyer_addressModel> Buyer_address { get; set; }
    }
}
