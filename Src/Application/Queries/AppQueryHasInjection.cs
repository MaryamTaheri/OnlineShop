using ONLINE_SHOP.Application.Queries.Product;
using ONLINE_SHOP.Domain.Application.Queries.Customer;
using ONLINE_SHOP.Domain.Application.Queries.Product;
using ONLINE_SHOP.Domain.Framework.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ONLINE_SHOP.Application.Queries;

public class AppQueryHasInjection : IHaveInjection
{
    public void Inject(IServiceCollection collection, IConfiguration configuration)
    {
        collection.AddScoped<ICustomerQueries, CustomerQueries>();
        collection.AddScoped<IProductQueries, ProductQueries>();
    }
}