using System.Collections.Generic;
using Kursovaya.Model.Supply;

namespace Kursovaya.Model.Factory;

public partial class FactoryModel
{
    public int FactoryId { get; set; }

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public virtual ICollection<SupplyModel> Supplies { get; } = new List<SupplyModel>();
}
