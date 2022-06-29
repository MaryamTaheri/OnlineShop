namespace ONLINE_SHOP.Domain.Events.DataTransferObjects.Order;

public class CustomerDTO
{
    public Guid CustomerId { get; set; }
    public string CustomerFirstName { get; set; }
    public string CustomerLastName { get; set; }
    public string CustomerMobile { get; set; }
}