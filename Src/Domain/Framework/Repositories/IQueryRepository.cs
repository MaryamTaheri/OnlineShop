using ONLINE_SHOP.Domain.Framework.Entities;

namespace ONLINE_SHOP.Domain.Framework.Repositories;

public interface IQueryRepository<TEntity, TKey> : IRepository
    where TEntity : class, IEntity
{
    Task<TEntity> FindAsync(TKey id, CancellationToken cancellationToken);
}