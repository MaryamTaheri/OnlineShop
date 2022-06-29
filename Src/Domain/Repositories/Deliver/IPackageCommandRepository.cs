using ONLINE_SHOP.Domain.Framework.Repositories;

namespace ONLINE_SHOP.Domain.Repositories.Deliver;

public interface IPackageCommandRepository : ICommandRepository<Entities.Package.Package, Guid>
{
    Task<bool> CheckHasOrderByIdAsync(Guid orderId, CancellationToken cancellationToken);
}