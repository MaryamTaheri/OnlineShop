using Microsoft.EntityFrameworkCore;

namespace ONLINE_SHOP.Infrastructure.Contexts.Providers;

public class QueryDbContext : DbContextBase
{
    public QueryDbContext(DbContextOptions options)
        : base(options)
    {
        ConnectionStringName = "SqlReadNode";
    }
}