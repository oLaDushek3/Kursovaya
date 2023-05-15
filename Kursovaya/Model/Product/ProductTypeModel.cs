using System.Collections.Generic;

namespace Kursovaya.Model.Product;

public partial class ProductTypeModel
{
    public int ProductTypeId { get; set; }

    public string Title { get; set; } = null!;

    public int ProductsGroupId { get; set; }

    public virtual List<ProductModel> Products { get; } = new List<ProductModel>();

    public virtual ProductsGroupModel ProductsGroup { get; set; } = null!;
}
