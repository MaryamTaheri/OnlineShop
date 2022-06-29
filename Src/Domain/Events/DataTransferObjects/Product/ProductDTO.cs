
using ONLINE_SHOP.Domain.Events.Enumerations;

namespace ONLINE_SHOP.Domain.Events.DataTransferObjects.Product;

public class ProductDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public PackageType Type { get; set; } 
    public decimal Price { get; set; }
}