using ONLINE_SHOP.Domain.Contracts.Commands.Deliver;
using ONLINE_SHOP.Domain.Contracts.Commands.Order;
using ONLINE_SHOP.Domain.Framework.Contracts.Response;
using ONLINE_SHOP.Domain.Framework.Services.Handlers;

namespace ONLINE_SHOP.Domain.Application.Commands.Package;

public interface IPackageCommands :
    IApplicationCommandHandler<CreatePackageCommand, EmptyResponse>
{
}