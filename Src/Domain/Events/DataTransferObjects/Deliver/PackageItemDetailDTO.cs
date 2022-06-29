
using ONLINE_SHOP.Domain.Events.Enumerations;

namespace ONLINE_SHOP.Domain.Events.DataTransferObjects.Package;

public class PackageItemDetailDTO
{
    public Guid ProductId { get; set; }
    public decimal ProductProfitPrice { get; set; }
    public int ProductCount { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public PostType ProductType { get; set; } 
}