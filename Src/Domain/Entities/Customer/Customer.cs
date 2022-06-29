using ONLINE_SHOP.Domain.Framework.Entities.Contracts.AggregateRoots;

namespace ONLINE_SHOP.Domain.Entities.Customer;

public partial class Customer : GuidAuditableAggregateRoot
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Mobile { get; set; }
}