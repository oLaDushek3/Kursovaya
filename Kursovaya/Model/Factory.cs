using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model
{
    public partial class Factory
    {
        public Factory()
        {
            this.Supply = new HashSet<Supply>();
        }

        [Key]
        public int Factory_id { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Supply> Supply { get; set; }
    }
}
