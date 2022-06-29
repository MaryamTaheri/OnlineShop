using System.Collections.ObjectModel;
using ONLINE_SHOP.Domain.Events.DomainEvents.Order;
using ONLINE_SHOP.Domain.Framework.ValueObjects;

namespace ONLINE_SHOP.Domain.Entities.Order;
public partial class Order
{
    protected override void When(object @event)
    {
        switch (@event)
        {
            case NewOrderCreated o:
                OrderItems ??= new Collection<OrderItem>();
                foreach (var item in o.Items)
                    OrderItems.Add(new OrderItem(EntityUuid.FromGuid(Id), item.ProductId, item.ProductName, item.ProductCategory, item.ProductPrice, item.ProductProfitPrice, item.ProductCount, item.ProductType));
                
                break;
        }
    }

    protected override void EnsureReadyState(object @event)
    {

    }

    protected override void EnsureValidState()
    {

    }
}