using ONLINE_SHOP.Domain.Framework.Entities.Auditable;

namespace ONLINE_SHOP.Domain.Framework.Repositories;

public interface ICommandRepository<TEntity, TKey> : ICreatableCommandRepository<TEntity, TKey> 
    where TEntity : class, IAuditableEntity
{
    void Modify(TEntity entity);
    void Remove(TKey id);
    void Remove(TEntity entity);
    void Restore(TEntity entity);
}