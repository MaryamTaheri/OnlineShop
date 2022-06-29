using ONLINE_SHOP.Domain.Application.Commands.Order;
using ONLINE_SHOP.Domain.Contracts.Commands.Order;
using ONLINE_SHOP.Domain.Framework.Contracts.Response;
using ONLINE_SHOP.Domain.Models.Order;

namespace ONLINE_SHOP.Application.Commands.Order;

public class OrderCommands : CommandsBase, IOrderCommands
{
    private readonly IOrderCommandModel _orderCommandModel;

    public OrderCommands(IOrderCommandModel orderCommandModel)
    {
        _orderCommandModel = orderCommandModel;
    }

    public async Task<EmptyResponse> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        => await _orderCommandModel.CreateOredrAsync(command, cancellationToken);
}