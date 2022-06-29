using ONLINE_SHOP.Domain.Events.Enumerations;
using ONLINE_SHOP.Domain.Framework.Entities.Contracts;
using ONLINE_SHOP.Domain.Framework.ValueObjects;

namespace ONLINE_SHOP.Domain.Entities.Package;

public partial class PackageItem : GuidAuditableEntity
{
    public EntityUuid PackageId { get; set; }

    public EntityUuid ProductId { get; set; }
    public string ProductName { get; set; }
    public PostType Type { get; set; }  
    public decimal ProductPrice { get; set; }
    public decimal ProductProfitPrice { get; set; }
    public int ProductCount { get; set; }

    public virtual Package Package { get; set; }  
}