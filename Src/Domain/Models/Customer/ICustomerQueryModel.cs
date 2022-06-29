using ONLINE_SHOP.Domain.Contracts.Queries.Customer;
using ONLINE_SHOP.Domain.Events.DataTransferObjects.Order;
using ONLINE_SHOP.Domain.Framework.Contracts.Response;
using ONLINE_SHOP.Domain.Framework.Models;

namespace ONLINE_SHOP.Domain.Models.Customer;

public interface ICustomerQueryModel : IQueryModel
{
    Task<DataResponse<CustomerDTO>> GetCustomerInfoByMobileAsync(CustomerInfoByMobileQuery query,
        CancellationToken cancellationToken);
}