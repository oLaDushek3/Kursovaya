using Kursovaya.Model.Place;

namespace Kursovaya.Model.Shipping;

public partial class ShippingProductPlaceModel
{
    public int ShippingProductId { get; set; }

    public int PlaceId { get; set; }

    public int Quantity { get; set; }

    public virtual PlaceModel Place { get; set; } = null!;

    public virtual ShippingProductModel ShippingProduct { get; set; } = null!;
}
