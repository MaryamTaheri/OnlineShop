using ONLINE_SHOP.Domain.Repositories.Product;
using ONLINE_SHOP.Infrastructure.Contexts.Providers;
using Microsoft.EntityFrameworkCore;

namespace ONLINE_SHOP.Infrastructure.Repositories.Product;

public class ProductQueryRepository : QueryRepository<Domain.Entities.Product.Product, Guid>, IProductQueryRepository
{
    public ProductQueryRepository(QueryDbContext context)
        : base(context)
    {
    }

    public async Task<List<Domain.Entities.Product.Product>> FindByIdsAsync(List<Guid> Ids, CancellationToken cancellationToken)
        => await Entities
                .Where(x => Ids.Contains(x.Id.Value))
                .ToListAsync(cancellationToken);
}