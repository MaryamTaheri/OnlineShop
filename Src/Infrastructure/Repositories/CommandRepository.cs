using System.Data;
using ONLINE_SHOP.Domain.Framework.Entities.Auditable;
using ONLINE_SHOP.Domain.Framework.Logging;
using ONLINE_SHOP.Domain.Framework.Repositories;
using ONLINE_SHOP.Domain.Framework.ValueObjects;
using ONLINE_SHOP.Infrastructure.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ONLINE_SHOP.Infrastructure.Repositories;

public class CommandRepository<TEntity, TKey> : ICommandRepository<TEntity, TKey>
    where TEntity : class, IAuditableEntity, ICreatableEntity
{
    public ILog Logger { get; }
    protected readonly DbContext Context;

    public DbSet<TEntity> Entities { get; }

    /// <summary>
    /// Don't use in join or left/right join 
    /// </summary>
    public IQueryable<TEntity> NotRemoved =>
        from e in Entities
        where e.RemovedAt == null
        select e;


    public CommandRepository(IHttpContextAccessor accessor, DbContextBase context)
    {
        Context = context;
        Entities = context.Set<TEntity>();
        Logger = LogManager.GetLogger<CommandRepository<TEntity, TKey>>();
    }

    public async Task<TEntity> FindAsync(TKey id, CancellationToken cancellationToken)
    {
        return await Entities.FindAsync(EntityUuid.FromString(id.ToString()), cancellationToken);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await Entities.AddAsync(entity, cancellationToken);
    }

    public void Modify(TEntity entity)
    {
        Entities.Update(entity);
    }

    public void Remove(TKey id)
    {
        Remove(FindAsync(id, CancellationToken.None).GetAwaiter().GetResult());
    }

    public void Remove(TEntity entity)
    {
        if (entity is IRemovableEntity dre)
            dre.RemovedAt = DateTime.UtcNow;
        
        Entities.Update(entity);
    }

    public void Restore(TEntity entity)
    {
        if (entity is IRemovableEntity dre)
        {
            dre.RemovedAt = null;
            dre.RemovedBy = null;
            Entities.Update(entity);
        }
    }
    
    public void PhysicalRemove(TKey id)
    {
        PhysicalRemove(FindAsync(id, CancellationToken.None).GetAwaiter().GetResult());
    }

    public void PhysicalRemove(TEntity entity)
    {
        Entities.Remove(entity);
    }

    public async Task<IDbContextTransaction> StartTransAsync(IsolationLevel level = IsolationLevel.ReadUncommitted, CancellationToken cancellationToken = default)
        => await Context.Database.BeginTransactionAsync(level, cancellationToken);

    public void SaveChange()
    {
        Context.SaveChanges();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await Context.SaveChangesAsync(cancellationToken);
    }
}