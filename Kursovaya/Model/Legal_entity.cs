using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model
{
    public partial class Legal_entity
    {
        public Legal_entity()
        {
            this.Buyer = new HashSet<Buyer>();
            this.Buyer_address = new HashSet<Buyer_address>();
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

        public virtual ICollection<Buyer> Buyer { get; set; }
        public virtual ICollection<Buyer_address> Buyer_address { get; set; }
    }
}
