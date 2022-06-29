

using ONLINE_SHOP.Domain.Contracts.Queries.Customer;
using ONLINE_SHOP.Domain.Events.DataTransferObjects.Order;
using ONLINE_SHOP.Domain.Framework.Contracts.Response;
using ONLINE_SHOP.Domain.Framework.Services.Handlers;

namespace ONLINE_SHOP.Domain.Application.Queries.Customer;

public interface ICustomerQueries :  
    IApplicationQueryHandler<CustomerInfoByMobileQuery, DataResponse<CustomerDTO>>
{
}