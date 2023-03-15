using System;
using System.Collections.Generic;
using Kursovaya.Model.Factory;
using Kursovaya.Model.Worker;

namespace Kursovaya.Model.Supply;

public partial class SupplyModel
{
    public int SupplyId { get; set; }

    public int FactoryId { get; set; }

    public DateTime Date { get; set; }

    public virtual FactoryModel Factory { get; set; } = null!;

    public virtual ICollection<SupplyProductModel> SupplyProducts { get; } = new List<SupplyProductModel>();

    public virtual ICollection<WorkerModel> Workers { get; } = new List<WorkerModel>();
}
