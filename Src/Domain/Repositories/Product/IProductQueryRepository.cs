
using ONLINE_SHOP.Domain.Framework.Repositories;

namespace ONLINE_SHOP.Domain.Repositories.Product;

public interface IProductQueryRepository : IQueryRepository<Entities.Product.Product, Guid>
{
     Task<List<Domain.Entities.Product.Product>> FindByIdsAsync(List<Guid> Ids, CancellationToken cancellationToken);
}