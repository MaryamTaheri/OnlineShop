using ONLINE_SHOP.Domain.Events.Enumerations;
using ONLINE_SHOP.Domain.Framework.Entities.Contracts;
using ONLINE_SHOP.Domain.Framework.ValueObjects;

namespace ONLINE_SHOP.Domain.Entities.Order;

public partial class OrderItem : GuidAuditableEntity
{
    public EntityUuid OrderId { get; set; }

    public EntityUuid ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductCategory { get; set; }
    public PackageType Type { get; set; }  
    public decimal ProductPrice { get; set; }
    public decimal ProductProfitPrice { get; set; }
    public int ProductCount { get; set; }

    public virtual Order Order { get; set; }  
}