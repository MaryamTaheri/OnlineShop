using ONLINE_SHOP.Domain.Framework.Services;
using ONLINE_SHOP.Domain.Models.Customer;
using ONLINE_SHOP.Domain.Models.Deliver;
using ONLINE_SHOP.Domain.Models.Order;
using ONLINE_SHOP.Domain.Models.Product;
using ONLINE_SHOP.Infrastructure.Models.Customer;
using ONLINE_SHOP.Infrastructure.Models.Deliver;
using ONLINE_SHOP.Infrastructure.Models.Order;
using ONLINE_SHOP.Infrastructure.Models.Product;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ONLINE_SHOP.Infrastructure.Models;

public class ModelHasInjects : IHaveInjection
{
    public void Inject(IServiceCollection collection, IConfiguration configuration)
    {
        //Queries:
        collection.AddScoped<ICustomerQueryModel, CustomerQueryModel>();
        collection.AddScoped<IProductQueryModel, ProductQueryModel>();

        //Commands:
        collection.AddScoped<IOrderCommandModel, OrderCommandModel>();
        collection.AddScoped<IPackageCommandModel, PackageCommandModel>();
    }
}