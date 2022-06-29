using ONLINE_SHOP.Domain.Contracts.Commands.Deliver;
using ONLINE_SHOP.Domain.Framework.Contracts.Response;
using ONLINE_SHOP.Domain.Framework.Models;

namespace ONLINE_SHOP.Domain.Models.Deliver;

public interface IPackageCommandModel : ICommandModel
{
    Task<EmptyResponse> CreatePackageAsync(CreatePackageCommand command, CancellationToken cancellationToken);
}