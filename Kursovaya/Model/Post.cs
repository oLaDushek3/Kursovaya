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
    public partial class Post
    {
        public Post()
        {
            this.Worker = new HashSet<WorkerModel>();
        }

        [Key]
        public int Post_id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<WorkerModel> Worker { get; set; }
    }
}
