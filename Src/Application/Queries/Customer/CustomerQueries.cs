using ONLINE_SHOP.Application.Queries;
using ONLINE_SHOP.Domain.Application.Queries.Customer;
using ONLINE_SHOP.Domain.Contracts.Queries.Customer;
using ONLINE_SHOP.Domain.Events.DataTransferObjects.Order;
using ONLINE_SHOP.Domain.Framework.Contracts.Response;
using ONLINE_SHOP.Domain.Models.Customer;

public class CustomerQueries : QueriesBase, ICustomerQueries
{
    private readonly ICustomerQueryModel _customerQueryModel;

    public CustomerQueries(ICustomerQueryModel customerQueryModel) => _customerQueryModel = customerQueryModel;

    public async Task<DataResponse<CustomerDTO>> Handle(CustomerInfoByMobileQuery query, CancellationToken cancellationToken)
    => await _customerQueryModel.GetCustomerInfoByMobileAsync(query, cancellationToken);
}