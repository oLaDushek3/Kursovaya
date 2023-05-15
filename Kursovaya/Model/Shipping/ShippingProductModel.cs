using System.Collections.Generic;
using Kursovaya.Model.Product;

namespace Kursovaya.Model.Shipping;

public partial class ShippingProductModel
{
    public int ShippingProductId { get; set; }

    public int ShippingId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public virtual ProductModel Product { get; set; } = null!;

    public virtual ShippingModel Shipping { get; set; } = null!;

    public virtual ICollection<ShippingProductPlaceModel> ShippingProductPlaces { get; set; } = new List<ShippingProductPlaceModel>();
}
