using ONLINE_SHOP.Domain.Events.DataTransferObjects.Order;
using ONLINE_SHOP.Domain.Framework.Events;
using ONLINE_SHOP.Domain.Framework.ValueObjects;

namespace ONLINE_SHOP.Domain.Events.DomainEvents.Order;

public class NewOrderCreated : DomainEvent
{
    public DateTime OrderDate { get; set; }
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
    public string CustomerFirstName { get; set; }
    public string CustomerLastName { get; set; }
    public string CustomerMobile { get; set; }
    public decimal TotalAmount { get; set; }
    public string TrackingCode { get; set; }
    public List<OrderItemDetailDTO> Items { get; set; } = new List<OrderItemDetailDTO>();
    
    public NewOrderCreated()
    {
        ExchangeName = Globals.Events.StateChangesBus;
        Routes = new[] {Globals.Events.Routes.OrderRoute};
    }
}