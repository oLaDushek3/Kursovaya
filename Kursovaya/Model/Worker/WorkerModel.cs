using Kursovaya.Model.Shipping;
using Kursovaya.Model.Supply;
using System.Collections.Generic;

namespace Kursovaya.Model.Worker;

public partial class WorkerModel
{
    public int WorkerId { get; set; }

    public string FullName { get; set; } = null!;

    public string TypeOfContract { get; set; } = null!;

    public string TypeOfSalary { get; set; } = null!;

    public int PostId { get; set; }

    public virtual PostModel Post { get; set; } = null!;

    public virtual ICollection<ShippingModel> Shippings { get; } = new List<ShippingModel>();

    public virtual ICollection<SupplyModel> Supplies { get; } = new List<SupplyModel>();
}
