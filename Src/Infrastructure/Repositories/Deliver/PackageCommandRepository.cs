using ONLINE_SHOP.Domain.Repositories.Deliver;
using ONLINE_SHOP.Infrastructure.Contexts.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ONLINE_SHOP.Infrastructure.Repositories.Deliver;

public class PackageCommandRepository : CommandRepository<Domain.Entities.Package.Package, Guid>, IPackageCommandRepository
{
    public PackageCommandRepository(IHttpContextAccessor accessor, CommandDbContext context) : base(accessor, context)
    {
    }

    public async Task<bool> CheckHasOrderByIdAsync(Guid orderId, CancellationToken cancellationToken)
        => await Entities
            .AnyAsync(p => p.OrderId == orderId, cancellationToken);
}