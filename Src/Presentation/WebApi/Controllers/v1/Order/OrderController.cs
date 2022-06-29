using Microsoft.AspNetCore.Mvc;
using MediatR;
using ONLINE_SHOP.Domain.Contracts.Commands.Order;
using ONLINE_SHOP.Domain.Contracts.API.Order;
using ONLINE_SHOP.Domain.Contracts.Queries.Customer;
using ONLINE_SHOP.Domain.Contracts.Queries.Product;

namespace ONLINE_SHOP.Presentation.WebApi.Controllers.v1.Order;

[ApiVersion("1.0")]
[Area("online-shop")]
public class OrderController : BaseController
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(nameof(CreateOrder))]
    public async Task<ActionResult<EmptyResult>> CreateOrder([FromBody] CreateOrderContract contract, CancellationToken cancellationToken)
    {
        //TODO
        //Command replaced with query

        var customerInfo = (await _mediator.Send(new CustomerInfoByMobileQuery { Mobile = contract.CustomerMobile }, cancellationToken)).Data;
        var productListInfo = (await _mediator.Send(new ProductListByIdsQuery { Ids = contract.Products.Select(p => p.ProductId).ToList() }, cancellationToken)).Data;

        //TODO
        //can use auto mapper

        return Dynamic(await _mediator.Send(new CreateOrderCommand(productListInfo, contract)
        {
            BasketId = contract.BasketId,
            OrderDate = contract.OrderDate,
            CustomerId = customerInfo.CustomerId,
            CustomerFirstName = customerInfo.CustomerFirstName,
            CustomerLastName = customerInfo.CustomerLastName,
            CustomerMobile = customerInfo.CustomerMobile,
            DiscountPercent = contract.DiscountPercent,
            DiscountAmount = contract.DiscountAmount
        }, cancellationToken));
    }
}