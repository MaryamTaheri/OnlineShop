using ONLINE_SHOP.Domain.Contracts.Queries.Customer;
using ONLINE_SHOP.Domain.Events.DataTransferObjects.Order;
using ONLINE_SHOP.Domain.Framework.Contracts.Response;
using ONLINE_SHOP.Domain.Framework.Exceptions;
using ONLINE_SHOP.Domain.Framework.Models;
using ONLINE_SHOP.Domain.Models.Customer;
using ONLINE_SHOP.Domain.Repositories.Customer;

namespace ONLINE_SHOP.Infrastructure.Models.Customer;

public class CustomerQueryModel : QueryModelBase, ICustomerQueryModel
{
    private readonly ICustomerQueryRepository _customerQueryRepository;

    public CustomerQueryModel(ICustomerQueryRepository customerQueryRepository)
    {
        _customerQueryRepository = customerQueryRepository;
    }

    public async Task<DataResponse<CustomerDTO>> GetCustomerInfoByMobileAsync(CustomerInfoByMobileQuery query,
        CancellationToken cancellationToken)
    {
        var customer = await _customerQueryRepository.FindByMobileAsync(query.Mobile, cancellationToken);
        if (customer is null)
            throw new Dexception(Situation.Make(SitKeys.Unprocessable),
                                    new List<KeyValuePair<string, string>> { new(":پیام:", "شماره موبایل مشتری صحیح نمیباشد.") });


        return DataResponse<CustomerDTO>.Instance(new CustomerDTO
        {
            CustomerId = customer.Id,
            CustomerFirstName = customer.FirstName,
            CustomerLastName = customer.LastName,
            CustomerMobile = customer.Mobile
        });
    }

}