using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Model.Worker
{
    public partial class PostModel
    {
        public PostModel()
        {
            Worker = new HashSet<WorkerModel>();
        }

        [Key]
        public int Post_id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<WorkerModel> Worker { get; set; }
    }
}
