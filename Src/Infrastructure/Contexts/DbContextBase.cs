using ONLINE_SHOP.Domain.Framework.Entities;
using ONLINE_SHOP.Infrastructure.Contexts.Common.Extension;
using Microsoft.EntityFrameworkCore;

namespace ONLINE_SHOP.Infrastructure.Contexts;

public class DbContextBase : DbContext
{
    private readonly AuditingInterceptor _auditingInterceptor;
    protected string ConnectionStringName = string.Empty;

    public DbContextBase(DbContextOptions options)
        : base(options)
    {
    }

    #region << Model Creating >>

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.AddInterceptors(_auditingInterceptor);

        options.UseSnakeCaseNamingConvention();
    }

    protected void ModelCreatingCommonConfig(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dbo");

        modelBuilder.NeedToRegisterEntitiesConfig<IEntity>();
        modelBuilder.NeedToRegisterMappingConfig();

        base.OnModelCreating(modelBuilder);
    }

    #endregion
}