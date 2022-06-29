using ONLINE_SHOP.Domain.Events.Enumerations;
using ONLINE_SHOP.Domain.Framework.Entities.Contracts.AggregateRoots;

namespace ONLINE_SHOP.Domain.Entities.Product;

public partial class Product : GuidAuditableAggregateRoot
{
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public PackageType Type { get; set; }    
}