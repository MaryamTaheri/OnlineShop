
using ONLINE_SHOP.Domain.Events.DataTransferObjects.Order;
using ONLINE_SHOP.Domain.Framework.Contracts.Response;
using ONLINE_SHOP.Domain.Framework.Services;

namespace ONLINE_SHOP.Domain.Contracts.Queries.Customer;

public class CustomerInfoByMobileQuery : IApplicationQuery<DataResponse<CustomerDTO>>
{
    public string Mobile { get; set; }
}