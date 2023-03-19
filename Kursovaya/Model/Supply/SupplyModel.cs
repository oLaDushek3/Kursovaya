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

    public virtual List<SupplyProductModel> SupplyProducts { get; } = new List<SupplyProductModel>();

    public List<WorkerModel> Workers { get; set; } = new List<WorkerModel>();
}
