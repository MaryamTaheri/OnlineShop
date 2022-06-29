
using ONLINE_SHOP.Infrastructure.Contexts.Providers.Sqlite;

public static class SqliteConfigExtensions
{
    public static void EnsureSqliteDb(this IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetRequiredService<SqliteCommandDbContext>();
            context.Database.EnsureCreated();
        }
    }
}


