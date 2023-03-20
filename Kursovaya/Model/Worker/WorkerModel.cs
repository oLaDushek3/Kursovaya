using Kursovaya.Model.Shipping;
using Kursovaya.Model.Supply;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kursovaya.Model.Worker;

public partial class WorkerModel
{
    public int WorkerId { get; set; }

    public string FullName { get; set; } = null!;

    public string TypeOfContract { get; set; } = null!;

    public string TypeOfSalary { get; set; } = null!;

    public int PostId { get; set; }

    public virtual PostModel Post { get; set; } = null!;

    public virtual ObservableCollection<ShippingModel> Shippings { get; } = new ObservableCollection<ShippingModel>();

    public virtual ObservableCollection<SupplyModel> Supplies { get; } = new ObservableCollection<SupplyModel>();
}
