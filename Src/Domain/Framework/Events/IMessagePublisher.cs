namespace ONLINE_SHOP.Domain.Framework.Events;

public interface IMessagePublisher
{
    void Publish(BusEvent domainEvent);
}