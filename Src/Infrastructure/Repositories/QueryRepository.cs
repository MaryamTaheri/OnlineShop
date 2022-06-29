using ONLINE_SHOP.Domain.Framework.Entities;
using ONLINE_SHOP.Domain.Framework.Logging;
using ONLINE_SHOP.Domain.Framework.Repositories;
using ONLINE_SHOP.Infrastructure.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ONLINE_SHOP.Infrastructure.Repositories;

public class QueryRepository<TEntity, TKey> : IQueryRepository<TEntity, TKey>  
    where TEntity : class, IEntity
{

    private readonly DbContext _context;
    protected readonly DbSet<TEntity> Entities;

    protected QueryRepository(DbContextBase context)
    {
        _context = context;
        Entities = context.Set<TEntity>();
    }

    public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();
        
    public async Task<TEntity> FindAsync(TKey id, CancellationToken cancellationToken)
        => await Entities.FindAsync(id, cancellationToken);
}