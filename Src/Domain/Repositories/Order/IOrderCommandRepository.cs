using ONLINE_SHOP.Domain.Framework.Repositories;

namespace ONLINE_SHOP.Domain.Repositories.Order;

public interface IOrderCommandRepository : ICommandRepository<Entities.Order.Order, Guid>
{
    Task<bool> CheckOrderWithBasketIdAsync(string basketId, CancellationToken cancellationToken);
}