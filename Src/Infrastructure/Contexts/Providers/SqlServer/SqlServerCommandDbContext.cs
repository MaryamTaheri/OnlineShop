using ONLINE_SHOP.Domain.Framework.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ONLINE_SHOP.Infrastructure.Contexts.Providers.SqlServer;

public sealed class SqlServerCommandDbContext : CommandDbContext
{
    public SqlServerCommandDbContext(DbContextOptions options)
        : base(options)
    {
    }

    #region << Model Creating >>

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(GlobalConfig.Config.GetConnectionString(ConnectionStringName), builder => builder.CommandTimeout(30));

        base.OnConfiguring(options);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ModelCreatingCommonConfig(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    #endregion
}