using ONLINE_SHOP.Domain.Framework.ValueObjects;

namespace ONLINE_SHOP.Domain.Entities.Package;

public partial class Package
{
    public Package()
    {

    }

    public Package(Guid orderId, DateTime orderDate, Guid customerId, string customerFirstName, string customerLastName, string customerMobile, decimal totalAmount,
    string trackingCode)
    {
        Id = EntityUuid.Generate();
        OrderId = EntityUuid.FromGuid(orderId);
        OrderDate = orderDate;
        CustomerId = EntityUuid.FromGuid(customerId);
        CustomerFirstName = customerFirstName;
        CustomerLastName = customerLastName;
        CustomerMobile = customerMobile;
        TotalAmount = totalAmount;
        TrackingCode = trackingCode;
    }
}