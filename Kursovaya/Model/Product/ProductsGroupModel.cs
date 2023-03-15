using System.Collections.Generic;

namespace Kursovaya.Model.Product;

public partial class ProductsGroupModel
{
    public int ProductsGroupId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ProductTypeModel> ProductTypes { get; } = new List<ProductTypeModel>();
}
