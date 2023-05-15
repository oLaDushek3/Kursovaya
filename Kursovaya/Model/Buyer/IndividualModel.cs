using System.Collections.Generic;

namespace Kursovaya.Model.Buyer;

public partial class IndividualModel
{
    public int IndividualId { get; set; }

    public string Name { get; set; } = null!;

    public string SeriesPassportNumber { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public virtual ICollection<BuyerAddressModel> BuyerAddresses { get; set; } = new List<BuyerAddressModel>();

    public virtual ICollection<BuyerModel> Buyers { get; } = new List<BuyerModel>();
}
