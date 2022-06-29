using ONLINE_SHOP.Domain.Contracts.API.Order;
using ONLINE_SHOP.Domain.Events.DataTransferObjects.Order;
using ONLINE_SHOP.Domain.Events.DataTransferObjects.Product;
using ONLINE_SHOP.Domain.Framework.Contracts.Response;
using ONLINE_SHOP.Domain.Framework.Services;
using ONLINE_SHOP.Domain.Framework.Services.Requests;

namespace ONLINE_SHOP.Domain.Contracts.Commands.Order;

public class CreateOrderCommand : CommandBase, IApplicationCommand<EmptyResponse>
{
    public string BasketId { get; set; }  //Basket Id 
    public DateTime OrderDate { get; set; }
    public Guid CustomerId { get; set; }
    public string CustomerFirstName { get; set; }
    public string CustomerLastName { get; set; }
    public string CustomerMobile { get; set; }
    public decimal DiscountPercent { get; set; }
    public decimal DiscountAmount { get; set; }
    public List<OrderItemDetailDTO> Products { get; set; }

    public CreateOrderCommand(List<ProductDTO> orderItems, CreateOrderContract contract)
    {
        //can use auto mapper

        var contractProducts = contract.Products;
        Products = new List<OrderItemDetailDTO>();
        foreach (var item in orderItems)
        {
            Products.Add(new OrderItemDetailDTO
            {
                ProductId = item.Id,
                ProductName = item.Name,
                ProductCategory = item.Category,
                ProductType = item.Type,
                ProductPrice = item.Price,
                ProductCount = contractProducts.First(p => p.ProductId == item.Id).ProductCount,
                ProductProfitPrice = contractProducts.First(p => p.ProductId == item.Id).ProductProfitPrice
            });
        }
    }
}