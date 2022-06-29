using ONLINE_SHOP.Domain.Contracts.Commands.Order;
using ONLINE_SHOP.Domain.Framework.Contracts.Response;
using ONLINE_SHOP.Domain.Framework.Models;

namespace ONLINE_SHOP.Domain.Models.Order;

public interface IOrderCommandModel : ICommandModel
{
    Task<EmptyResponse> CreateOredrAsync(CreateOrderCommand command, CancellationToken cancellationToken);
}