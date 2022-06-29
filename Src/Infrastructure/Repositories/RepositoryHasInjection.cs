using ONLINE_SHOP.Domain.Framework.Services;
using ONLINE_SHOP.Domain.Repositories.Order;
using ONLINE_SHOP.Domain.Repositories.Customer;
using ONLINE_SHOP.Infrastructure.Repositories.Customer;
using ONLINE_SHOP.Infrastructure.Repositories.Order;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ONLINE_SHOP.Infrastructure.Repositories.Product;
using ONLINE_SHOP.Domain.Repositories.Product;
using ONLINE_SHOP.Infrastructure.Repositories.Deliver;
using ONLINE_SHOP.Domain.Repositories.Deliver;

namespace ONLINE_SHOP.Infrastructure.Repositories;

public class RepositoryHasInjection : IHaveInjection
{
    public void Inject(IServiceCollection collection, IConfiguration configuration)
    {
        //Queries:
        collection.AddScoped<ICustomerQueryRepository, CustomerQueryRepository>();
        collection.AddScoped<IProductQueryRepository, ProductQueryRepository>();

        //Commands:
        collection.AddScoped<IOrderCommandRepository, OrderCommandRepository>();
        collection.AddScoped<IPackageCommandRepository, PackageCommandRepository>();
    }
}