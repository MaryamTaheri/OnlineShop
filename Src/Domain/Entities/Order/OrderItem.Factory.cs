using ONLINE_SHOP.Domain.Events.Enumerations;
using ONLINE_SHOP.Domain.Framework.ValueObjects;

namespace ONLINE_SHOP.Domain.Entities.Order;

public partial class OrderItem
{
    public OrderItem()
    {
    }

    public OrderItem(Guid orderId, Guid productId, string productName, string productCategory, decimal productPrice, decimal productProfitPrice, int productCount, PackageType type)
    {
        Id = EntityUuid.Generate();
        OrderId = EntityUuid.FromGuid(orderId);
        ProductId = EntityUuid.FromGuid(productId);
        ProductName = productName;
        ProductCategory = productCategory;
        Type = type;
        ProductPrice = productPrice;
        ProductProfitPrice = ProductProfitPrice;
        ProductCount = productCount;
    }
}