namespace Kursovaya.Model.Buyer;

public partial class BuyerAddressModel
{
    public int BuyerAddressId { get; set; }

    public string Adress { get; set; } = null!;

    public string? Note { get; set; }

    public int? IndividualId { get; set; }

    public int? LegalEntityId { get; set; }

    public virtual IndividualModel? Individual { get; set; }

    public virtual LegalEntityModel? LegalEntity { get; set; }
}
