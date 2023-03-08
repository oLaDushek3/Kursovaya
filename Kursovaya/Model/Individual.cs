using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model
{
    public partial class Individual
    {
        public Individual()
        {
            this.Buyer = new HashSet<Buyer>();
            this.Buyer_address = new HashSet<Buyer_address>();
        }

        [Key]
        public int Individual_id { get; set; }
        public string Name { get; set; }
        public string surname { get; set; }
        public string Series_passport_number { get; set; }
        public string Phone_Number { get; set; }

        public virtual ICollection<Buyer> Buyer { get; set; }
        public virtual ICollection<Buyer_address> Buyer_address { get; set; }
    }
}
