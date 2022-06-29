using ONLINE_SHOP.Domain.Contracts.Commands.Deliver;
using ONLINE_SHOP.Domain.Events.DomainEvents.Deliver;
using ONLINE_SHOP.Domain.Framework.Contracts.Response;
using ONLINE_SHOP.Domain.Framework.Events;
using ONLINE_SHOP.Domain.Framework.Models;
using ONLINE_SHOP.Domain.Models.Deliver;
using ONLINE_SHOP.Domain.Repositories.Deliver;

namespace ONLINE_SHOP.Infrastructure.Models.Deliver;

public class PackageCommandModel : CommandModelBase, IPackageCommandModel
{
    private readonly IPackageCommandRepository _packageCommandRepository;

    public PackageCommandModel(
        IPackageCommandRepository packageCommandRepository,
        IMessagePublisher publisher)
        : base(packageCommandRepository, publisher)
    {
        _packageCommandRepository = packageCommandRepository;
    }

    public async Task<EmptyResponse> CreatePackageAsync(CreatePackageCommand command, CancellationToken cancellationToken)
    {
        var checkHasOrderById = await _packageCommandRepository.CheckHasOrderByIdAsync(command.OrderId, cancellationToken);
        if (checkHasOrderById)
            return null;

        var package = new Domain.Entities.Package.Package(command.OrderId, command.OrderDate, command.CustomerId, command.CustomerFirstName, command.CustomerLastName,
         command.CustomerMobile, command.TotalAmount, command.TrackingCode);

        package.Apply(new NewPackageCreated
        {
        });

        await _packageCommandRepository.AddAsync(package, cancellationToken);
        await HandleTransactionAsync(command, package, cancellationToken);

        return EmptyResponse.Instance();
    }
}