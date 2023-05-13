using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Kursovaya.Model.Factory;
using Kursovaya.Model.Worker;
using Microsoft.EntityFrameworkCore;
namespace Kursovaya.Model.Supply;

public partial class SupplyModel : ICloneable
{
    public int SupplyId { get; set; }

    public int FactoryId { get; set; }

    public DateTime Date { get; set; }

    public virtual FactoryModel Factory { get; set; } = null!;

    public virtual List<SupplyProductModel> SupplyProducts { get; set; } = new List<SupplyProductModel>();

    public List<WorkerModel> Workers { get; set; } = new List<WorkerModel>();

    public object Clone() => MemberwiseClone();
}
