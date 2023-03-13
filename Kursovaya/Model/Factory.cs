﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model
{
    public partial class Factory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Factory()
        {
            this.Supply = new HashSet<SupplyModel>();
        }

        [Key]
        public int Factory_id { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<SupplyModel> Supply { get; set; }
    }
}
