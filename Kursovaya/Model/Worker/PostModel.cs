using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kursovaya.Model.Worker;

public partial class PostModel
{
    public int PostId { get; set; }

    public string Title { get; set; } = null!;

    public virtual ObservableCollection<WorkerModel> Workers { get; } = new ObservableCollection<WorkerModel>();
}
