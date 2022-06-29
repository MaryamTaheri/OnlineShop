
namespace ONLINE_SHOP.Domain.Events.DataTransferObjects.Order;

public class OrderItemDTO
{
    public Guid ProductId { get; set; }
    public decimal ProductProfitPrice { get; set; }
    public int ProductCount { get; set; }
}