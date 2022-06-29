using ONLINE_SHOP.Domain.Events.Enumerations;
using ONLINE_SHOP.Domain.Framework.ValueObjects;

namespace ONLINE_SHOP.Domain.Entities.Order;

public partial class Order
{
    public Order()
    {

    }

    public Order(string basketId, DateTime orderDate, Guid customerId, string customerFirstName, string customerLastName, string customerMobile, decimal totalAmount,
    decimal discountPercent, decimal discountAmount)
    {
        Id = EntityUuid.Generate();
        BasketId = basketId;
        OrderDate = orderDate;
        CustomerId = EntityUuid.FromGuid(customerId);
        CustomerFirstName = customerFirstName;
        CustomerLastName = customerLastName;
        CustomerMobile = customerMobile;
        TotalAmount = totalAmount;
        DiscountAmount = discountAmount;
        DiscountPercent = discountPercent;
        TrackingCode = EntityUuid.Generate().ToString();
        State = OrderStates.Pending;
    }
}