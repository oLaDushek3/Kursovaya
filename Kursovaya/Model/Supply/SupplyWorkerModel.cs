using Kursovaya.Model.Shipping;
using Kursovaya.Model.Worker;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model.Supply
{
    public partial class SupplyWorkerModel
    {
        [Key]
        public int Worker_id { get; set; }
        public int Supply_id { get; set; }

        [ForeignKey("Supply_id")]
        public virtual SupplyModel Supply { get; set; }
        [ForeignKey("Worker_id")]
        public virtual WorkerModel Worker { get; set; }
    }
}
