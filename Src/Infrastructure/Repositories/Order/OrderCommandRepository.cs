using ONLINE_SHOP.Domain.Repositories.Order;
using ONLINE_SHOP.Infrastructure.Contexts.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ONLINE_SHOP.Infrastructure.Repositories.Order;

public class OrderCommandRepository : CommandRepository<Domain.Entities.Order.Order, Guid>, IOrderCommandRepository
{
    public OrderCommandRepository(IHttpContextAccessor accessor, CommandDbContext context) : base(accessor, context)
    {
    }

    public async Task<bool> CheckOrderWithBasketIdAsync(string basketId, CancellationToken cancellationToken)
        => await Entities
            .AnyAsync(p => p.BasketId == basketId, cancellationToken);
}