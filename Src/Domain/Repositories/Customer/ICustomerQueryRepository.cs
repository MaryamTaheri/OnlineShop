
using ONLINE_SHOP.Domain.Framework.Repositories;

namespace ONLINE_SHOP.Domain.Repositories.Customer;

public interface ICustomerQueryRepository : IQueryRepository<Entities.Customer.Customer, Guid>
{
     Task<Domain.Entities.Customer.Customer> FindByMobileAsync(string mobile, CancellationToken cancellationToken);
}