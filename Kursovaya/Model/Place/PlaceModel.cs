using System.Collections.Generic;
using Kursovaya.Model.Shipping;
using Kursovaya.Model.Supply;

namespace Kursovaya.Model.Place;

public partial class PlaceModel
{
    public int PlaceId { get; set; }

    public string Place1 { get; set; } = null!;

    public virtual ICollection<ShippingProductPlaceModel> ShippingProductPlaces { get; } = new List<ShippingProductPlaceModel>();

    public virtual ICollection<SupplyProductPlaceModel> SupplyProductPlaces { get; } = new List<SupplyProductPlaceModel>();
}
