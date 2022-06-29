using ONLINE_SHOP.Domain.Consumers.Order;
using ONLINE_SHOP.Domain.Framework.Services;
using ONLINE_SHOP.Infrastructure.Consumers.Order;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ONLINE_SHOP.Infrastructure.Consumers;

public class ConsumerHasInjection : IHaveInjection
{
    public void Inject(IServiceCollection collection, IConfiguration configuration)
    {
        collection.AddScoped<IOrderCreatedNotifyConsumer, OrderCreatedNotifyConsumer>();
    }
}