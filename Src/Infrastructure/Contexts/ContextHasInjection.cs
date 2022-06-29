using ONLINE_SHOP.Domain.Framework.Services;
using ONLINE_SHOP.Infrastructure.Contexts.Providers;
using ONLINE_SHOP.Infrastructure.Contexts.Providers.Sqlite;
using ONLINE_SHOP.Infrastructure.Contexts.Providers.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ONLINE_SHOP.Infrastructure.Contexts;

public class ContextHasInjection : IHaveInjection
{
    public void Inject(IServiceCollection collection, IConfiguration configuration)
    {
        #region << C O N N E C T I O N   S T R I N G >>

        var context = "FAKE";
        // if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("CURRENT_CONTEXT")))
        //     context = Environment.GetEnvironmentVariable("CURRENT_CONTEXT");

        switch (context)
        {
            case "ORIGINAL":
                collection.AddDbContext<SqlServerQueryDbContext>(ServiceLifetime.Transient);
                collection.AddScoped<QueryDbContext, SqlServerQueryDbContext>();

                collection.AddDbContext<SqlServerCommandDbContext>(ServiceLifetime.Transient);
                collection.AddScoped<CommandDbContext, SqlServerCommandDbContext>();
                break;

            case "FAKE":

                //Query .....
                collection.AddDbContext<SqliteQueryDbContext>();
                collection.AddScoped<QueryDbContext, SqliteQueryDbContext>();

                //Command
                collection.AddDbContext<SqliteCommandDbContext>();
                collection.AddScoped<CommandDbContext, SqliteCommandDbContext>();
                break;
        }

        #endregion
    }
}