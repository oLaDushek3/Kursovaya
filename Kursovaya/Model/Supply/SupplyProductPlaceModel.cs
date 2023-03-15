using Kursovaya.Model.Place;

namespace Kursovaya.Model.Supply;

public partial class SupplyProductPlaceModel
{
    public int SupplyProductId { get; set; }

    public int PlaceId { get; set; }

    public int Quantity { get; set; }

    public virtual PlaceModel Place { get; set; } = null!;

    public virtual SupplyProductModel SupplyProduct { get; set; } = null!;
}
