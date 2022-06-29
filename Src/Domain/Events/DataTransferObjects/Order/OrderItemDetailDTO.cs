
using ONLINE_SHOP.Domain.Events.Enumerations;

namespace ONLINE_SHOP.Domain.Events.DataTransferObjects.Order;

public class OrderItemDetailDTO : OrderItemDTO
{
    public string ProductName { get; set; }
    public string ProductCategory { get; set; }
    public decimal ProductPrice { get; set; }
    public PackageType ProductType { get; set; } 
}