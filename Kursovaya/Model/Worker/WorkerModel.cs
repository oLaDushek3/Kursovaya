using Kursovaya.Model.Shipping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model.Worker
{
    public partial class WorkerModel
    {
        public WorkerModel()
        {
            Shipping = new HashSet<ShippingModel>();
            Supply = new HashSet<SupplyModel>();
        }

        [Key]
        public int Worker_id { get; set; }
        public string Full_name { get; set; }
        public int Section_id { get; set; }
        public string Type_of_contract { get; set; }
        public string Type_of_salary { get; set; }
        public int Post_id { get; set; }

        [ForeignKey("Post_id")]
        public virtual PostModel Post { get; set; }
        public virtual ICollection<ShippingModel> Shipping { get; set; }
        public virtual ICollection<SupplyModel> Supply { get; set; }
    }
}
