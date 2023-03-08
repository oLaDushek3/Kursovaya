using Kursovaya.Model.Worker;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model
{
    public partial class Section
    {
        public Section()
        {
            this.Place = new HashSet<Place>();
            this.Worker = new HashSet<WorkerModel>();
        }

        [Key]
        public int Section_id { get; set; }
        public string Target { get; set; }

        public virtual ICollection<Place> Place { get; set; }
        public virtual ICollection<WorkerModel> Worker { get; set; }
    }
}
