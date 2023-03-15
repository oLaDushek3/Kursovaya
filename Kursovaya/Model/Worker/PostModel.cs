using System.Collections.Generic;

namespace Kursovaya.Model.Worker;

public partial class PostModel
{
    public int PostId { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<WorkerModel> Workers { get; } = new List<WorkerModel>();
}
