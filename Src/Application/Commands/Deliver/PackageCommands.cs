using ONLINE_SHOP.Domain.Application.Commands.Package;
using ONLINE_SHOP.Domain.Contracts.Commands.Deliver;
using ONLINE_SHOP.Domain.Framework.Contracts.Response;
using ONLINE_SHOP.Domain.Models.Deliver;

namespace ONLINE_SHOP.Application.Commands.Deliver;

public class PackageCommands : CommandsBase, IPackageCommands
{
    private readonly IPackageCommandModel _packageCommandModel;

    public PackageCommands(IPackageCommandModel packageCommandModel) 
    => _packageCommandModel = packageCommandModel;

    public async Task<EmptyResponse> Handle(CreatePackageCommand command, CancellationToken cancellationToken)
        => await _packageCommandModel.CreatePackageAsync(command, cancellationToken);
}