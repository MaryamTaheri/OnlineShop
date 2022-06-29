using ONLINE_SHOP.Domain.Contracts.Commands.Order;
using ONLINE_SHOP.Domain.Events.DomainEvents.Order;
using ONLINE_SHOP.Domain.Framework.Contracts.Response;
using ONLINE_SHOP.Domain.Framework.Events;
using ONLINE_SHOP.Domain.Framework.Exceptions;
using ONLINE_SHOP.Domain.Framework.Models;
using ONLINE_SHOP.Domain.Models.Order;
using ONLINE_SHOP.Domain.Repositories.Order;

namespace ONLINE_SHOP.Infrastructure.Models.Order;

public class OrderCommandModel : CommandModelBase, IOrderCommandModel
{
    private readonly IOrderCommandRepository _orderCommandRepository;

    public OrderCommandModel(
        IOrderCommandRepository orderCommandRepository,
        IMessagePublisher publisher)
        : base(orderCommandRepository, publisher)
    {
        _orderCommandRepository = orderCommandRepository;
    }

    public async Task<EmptyResponse> CreateOredrAsync(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var totalAmount = command.Products.Select(p => (p.ProductPrice * p.ProductCount) + p.ProductProfitPrice).Sum();
        var discountAmount = command.DiscountPercent > 0 ? totalAmount * command.DiscountPercent : command.DiscountAmount;

        if (discountAmount > 0)
            totalAmount -= discountAmount;

        checkValidate();

        var checkOrderWithBasketId = await _orderCommandRepository.CheckOrderWithBasketIdAsync(command.BasketId, cancellationToken);
        if (checkOrderWithBasketId)
            return null;

        var order = new Domain.Entities.Order.Order(command.BasketId, command.OrderDate, command.CustomerId, command.CustomerFirstName, command.CustomerLastName,
         command.CustomerMobile, totalAmount, command.DiscountPercent, command.DiscountAmount);

        order.Apply(new NewOrderCreated
        {
            OrderId = order.Id,
            OrderDate = order.OrderDate,
            CustomerId = order.CustomerId,
            CustomerFirstName = order.CustomerFirstName,
            CustomerLastName = order.CustomerLastName,
            CustomerMobile = order.CustomerMobile,
            TotalAmount = order.TotalAmount,
            TrackingCode = order.TrackingCode,
            Items = command.Products
        });

        await _orderCommandRepository.AddAsync(order, cancellationToken);
        await HandleTransactionAsync(command, order, cancellationToken);

        return EmptyResponse.Instance();


        void checkValidate()
        {
            //مبلغ پایه جهت بررسی را میتوان بصورت 
            //const,
            //appsetting,
            //یا در یک جدول اطلاعات پایه قرار داد تا درصورت تغییر، نیاز به تغییر در کد نباشد
            if (totalAmount < 5000)
                throw new Dexception(Situation.Make(SitKeys.Unprocessable),
                            new List<KeyValuePair<string, string>> { new(":پیام:", "جمع مبلغ نمیتواند کمتر 5000 تومان باشد.") });
        }
    }
}