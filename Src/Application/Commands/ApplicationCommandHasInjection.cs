using ONLINE_SHOP.Application.Commands.Deliver;
using ONLINE_SHOP.Application.Commands.Order;
using ONLINE_SHOP.Domain.Application.Commands.Order;
using ONLINE_SHOP.Domain.Application.Commands.Package;
using ONLINE_SHOP.Domain.Framework.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ONLINE_SHOP.Application.Commands;

public class ApplicationCommandHasInjection : IHaveInjection
{
    public void Inject(IServiceCollection collection, IConfiguration configuration)
    {
        collection.AddScoped<IOrderCommands, OrderCommands>();
        collection.AddScoped<IPackageCommands, PackageCommands>();
    }
}