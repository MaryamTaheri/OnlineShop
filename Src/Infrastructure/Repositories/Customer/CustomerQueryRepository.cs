using ONLINE_SHOP.Domain.Repositories.Customer;
using ONLINE_SHOP.Infrastructure.Contexts.Providers;
using Microsoft.EntityFrameworkCore;

namespace ONLINE_SHOP.Infrastructure.Repositories.Customer;

public class CustomerQueryRepository : QueryRepository<Domain.Entities.Customer.Customer, Guid>, ICustomerQueryRepository
{
    public CustomerQueryRepository(QueryDbContext context)
        : base(context)
    {
    }

    public async Task<Domain.Entities.Customer.Customer> FindByMobileAsync(string mobile, CancellationToken cancellationToken)
        => await Entities.FirstOrDefaultAsync(x => x.Mobile == mobile, cancellationToken);
}