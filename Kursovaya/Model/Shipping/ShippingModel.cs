using System;
using System.Collections.Generic;
using Kursovaya.Model.Buyer;
using Kursovaya.Model.Worker;

namespace Kursovaya.Model.Shipping;

public partial class ShippingModel
{
    public int ShippingId { get; set; }

    public int Buyer { get; set; }

    public DateTime Date { get; set; }

    public decimal Amount { get; set; }

    public virtual BuyerModel BuyerNavigation { get; set; } = null!;

    public virtual ICollection<ShippingProductModel> ShippingProducts { get; } = new List<ShippingProductModel>();

    public virtual ICollection<WorkerModel> Workers { get; } = new List<WorkerModel>();
}
