using ONLINE_SHOP.Domain.Framework.Logging;

namespace ONLINE_SHOP.Domain.Framework.Events;

public abstract class EventConsumer
{
    public ILog Logger { get; }
    public EventConsumer()
    {
        Logger = LogManager.GetLogger<EventConsumer>();
    }
}