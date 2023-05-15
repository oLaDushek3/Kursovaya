using System.Collections.Generic;

namespace Kursovaya.Model.Buyer;

public partial class LegalEntityModel
{
    public int LegalEntityId { get; set; }

    public string Organization { get; set; } = null!;

    public string СheckingAccount { get; set; } = null!;

    public string Bank { get; set; } = null!;

    public string CorrespondentAccount { get; set; } = null!;

    public string Bic { get; set; } = null!;

    public string Rrc { get; set; } = null!;

    public string Tin { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public virtual ICollection<BuyerAddressModel> BuyerAddresses { get; set; } = new List<BuyerAddressModel>();

    public virtual ICollection<BuyerModel> Buyers { get; } = new List<BuyerModel>();
}
