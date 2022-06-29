using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ONLINE_SHOP.Infrastructure.Contexts.Providers;

public class CommandDbContext : DbContextBase
{
    public CommandDbContext(DbContextOptions options)
        : base(options)
    {
        ConnectionStringName = "SqlWriteNode";
    }
}