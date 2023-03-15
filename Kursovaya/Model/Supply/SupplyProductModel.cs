using System.Collections.Generic;
using Kursovaya.Model.Product;

namespace Kursovaya.Model.Supply;

public partial class SupplyProductModel
{
    public int SupplyProductId { get; set; }

    public int SupplyId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public virtual ProductModel Product { get; set; } = null!;

    public virtual SupplyModel Supply { get; set; } = null!;

    public virtual ICollection<SupplyProductPlaceModel> SupplyProductPlaces { get; } = new List<SupplyProductPlaceModel>();
}
