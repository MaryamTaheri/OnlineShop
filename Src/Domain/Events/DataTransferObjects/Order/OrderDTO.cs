using ONLINE_SHOP.Domain.Events.DataTransferObjects.Order;

namespace ONLINE_SHOP.Domain.Events.DataTransferObjects.Order;

public class OrderDTO
{
    public DateTime OrderDate { get; set; }
    public Guid CustomerId { get; set; }
    public string CustomerFirstName { get; set; }
    public string CustomerLastName { get; set; }
    public string CustomerMobile { get; set; }
    public decimal DiscountPercent { get; set; }
    public decimal DiscountAmount { get; set; }
    public string TrackingCode { get; set; }  //from basket service ???

    public OrderItemDTO[] Products { get; set; }
}