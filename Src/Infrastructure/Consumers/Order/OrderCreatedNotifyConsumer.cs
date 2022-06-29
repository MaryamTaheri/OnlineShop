using ONLINE_SHOP.Domain.Consumers.Order;
using ONLINE_SHOP.Domain.Contracts.Commands.Deliver;
using ONLINE_SHOP.Domain.Events.DataTransferObjects.Package;
using ONLINE_SHOP.Domain.Events.DomainEvents.Order;
using ONLINE_SHOP.Domain.Events.Enumerations;
using ONLINE_SHOP.Domain.Framework.Extensions;
using MediatR;

namespace ONLINE_SHOP.Infrastructure.Consumers.Order;

public class OrderCreatedNotifyConsumer : ConsumersBase, IOrderCreatedNotifyConsumer
{
    public bool Enabled { get; } = true;
    public string BusName { get; } = "STATE_CHANGES";
    public string EventType { get; } = typeof(NewOrderCreated).ToString();
    public int MaxRetry { get; set; } = 3;
    public TimeSpan RetryWait { get; set; } = TimeSpan.FromMinutes(5);

    private readonly IMediator _mediator;

    public OrderCreatedNotifyConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<bool> Handle(int @try, string payload)
    {
        var instance = payload.Deserialize<NewOrderCreated>();

        var packageList = new List<PackageItemDetailDTO>();
        foreach (var item in instance.Items)
        {
            packageList.Add(new PackageItemDetailDTO
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                ProductType = item.ProductType == PackageType.NORMAL ? PostType.NORMAL : PostType.EXPRESS,
                ProductPrice = item.ProductPrice,
                ProductProfitPrice = item.ProductProfitPrice,
                ProductCount = item.ProductCount
            });
        }

        //can use autp mapper
        await _mediator.Send(new CreatePackageCommand
        {
            OrderId = instance.OrderId,
            OrderDate = instance.OrderDate,
            CustomerId = instance.CustomerId,
            CustomerFirstName = instance.CustomerFirstName,
            CustomerLastName = instance.CustomerLastName,
            CustomerMobile = instance.CustomerMobile,
            TotalAmount = instance.TotalAmount,
            TrackingCode = instance.TrackingCode,
            PackageItems = packageList

        }, CancellationToken.None);

        return true;
    }
}