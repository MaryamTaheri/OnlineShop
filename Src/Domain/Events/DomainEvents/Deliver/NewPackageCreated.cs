using ONLINE_SHOP.Domain.Framework.Events;

namespace ONLINE_SHOP.Domain.Events.DomainEvents.Deliver;

public class NewPackageCreated : DomainEvent
{
    
    
    public NewPackageCreated()
    {
        ExchangeName = Globals.Events.StateChangesBus;
        Routes = new[] {Globals.Events.Routes.OrderRoute};
    }
}