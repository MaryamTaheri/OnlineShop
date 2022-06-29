using ONLINE_SHOP.Domain.Events;
using ONLINE_SHOP.Domain.Framework.Events;

namespace ONLINE_SHOP.Infrastructure.Consumers;

public abstract class ConsumersBase : EventConsumer
{
    public string Route { get; } = Globals.Events.Routes.OrderRoute;
}