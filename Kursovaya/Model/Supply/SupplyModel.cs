using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Kursovaya.Model.Factory;
using Kursovaya.Model.Worker;
using Microsoft.EntityFrameworkCore;

namespace Kursovaya.Model.Supply;

public partial class SupplyModel
{
    public int SupplyId { get; set; }

    public int FactoryId { get; set; }

    public DateTime Date { get; set; }

    public virtual FactoryModel Factory { get; set; } = null!;

    public virtual ObservableCollection<SupplyProductModel> SupplyProducts { get; } = new ObservableCollection<SupplyProductModel>();

    public ObservableCollection<WorkerModel> Workers { get; set; } = new ObservableCollection<WorkerModel>();
}
