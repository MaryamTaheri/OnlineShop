using ONLINE_SHOP.Domain.Framework.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ONLINE_SHOP.Infrastructure.Contexts.Providers.Sqlite;

public sealed class SqliteQueryDbContext : QueryDbContext
{
    public SqliteQueryDbContext(DbContextOptions<SqliteQueryDbContext> options) 
        : base(options)
    {
    }

    #region << Model Creating >>

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite(GlobalConfig.Config.GetConnectionString("SqliteDb"));
        
        base.OnConfiguring(options);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ModelCreatingCommonConfig(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    #endregion
}