using System.Collections.Generic;
using Kursovaya.Model.Shipping;

namespace Kursovaya.Model.Buyer;
public partial class BuyerModel
{
    public int Buyer1 { get; set; }

    public int? IndividualId { get; set; }

    public int? LegalEntityId { get; set; }

    public virtual IndividualModel? Individual { get; set; }

    public virtual LegalEntityModel? LegalEntity { get; set; }

    public virtual ICollection<ShippingModel> Shippings { get; } = new List<ShippingModel>();
}
