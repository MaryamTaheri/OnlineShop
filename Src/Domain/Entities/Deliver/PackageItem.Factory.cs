using ONLINE_SHOP.Domain.Events.Enumerations;
using ONLINE_SHOP.Domain.Framework.ValueObjects;

namespace ONLINE_SHOP.Domain.Entities.Package;

public partial class PackageItem
{
    public PackageItem()
    {
    }

    public PackageItem(Guid orderId, Guid productId, string productName, string productCategory, decimal productPrice, decimal productProfitPrice, int productCount, PostType type)
    {
        Id = EntityUuid.Generate();
        ProductId = EntityUuid.FromGuid(productId);
        ProductName = productName;
        ProductPrice = productPrice;
        ProductProfitPrice = ProductProfitPrice;
        ProductCount = productCount;
        Type = type;
    }
}