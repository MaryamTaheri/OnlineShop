using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using ONLINE_SHOP.Domain.Events.Enumerations;
using ONLINE_SHOP.Domain.Framework.Entities.Contracts.AggregateRoots;
using ONLINE_SHOP.Domain.Framework.ValueObjects;

namespace ONLINE_SHOP.Domain.Entities.Order;

public partial class Order : GuidAuditableAggregateRoot
{
    public string BasketId { get; set; }
    public DateTime OrderDate { get; set; }

    
    public EntityUuid CustomerId { get; set; }
    [NotMapped]
    public string CustomerName => $"{CustomerFirstName} {CustomerLastName}";
    public string CustomerFirstName { get; set; }
    public string CustomerLastName { get; set; }
    public string CustomerMobile { get; set; }
    

    public decimal TotalAmount { get; set; }
    public decimal DiscountPercent { get; set; }
    public decimal DiscountAmount { get; set; }

    public DateTime? CancelDeadline { get; set; }
    public string TrackingCode { get; set; }

    public OrderStates State { get; set; } = OrderStates.Pending;

        
    public Collection<OrderItem> OrderItems { get; set; }
}