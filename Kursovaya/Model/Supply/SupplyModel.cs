using Kursovaya.Model.Worker;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model
{
    public partial class SupplyModel
    {
        public SupplyModel()
        {
            this.Supply_Product = new HashSet<Supply_Product>();
            this.Worker = new HashSet<WorkerModel>();
        }

        [Key]
        public int Supply_id { get; set; }
        public int Factory_id { get; set; }
        public System.DateTime Date { get; set; }

        [ForeignKey("Factory_id")]
        public virtual Factory Factory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Supply_Product> Supply_Product { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkerModel> Worker { get; set; }
    }
}
