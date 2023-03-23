using System.Collections.Generic;
using Kursovaya.Model.Shipping;
using Kursovaya.Model.Supply;

namespace Kursovaya.Model.Product;

public partial class ProductModel
{
    public int ProductId { get; set; }

    public string Title { get; set; } = null!;

    public string Characteristic { get; set; } = null!;

    public int ProductTypeId { get; set; }

    public int Quantity { get; set; }

    public decimal PricePerUnit { get; set; }

    public virtual ProductTypeModel ProductType { get; set; }

    public virtual List<ShippingProductModel> ShippingProducts { get; } = new List<ShippingProductModel>();

    public virtual List<SupplyProductModel> SupplyProducts { get; } = new List<SupplyProductModel>();
}
