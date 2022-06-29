using ONLINE_SHOP.Domain.Contracts.Queries.Product;
using ONLINE_SHOP.Domain.Events.DataTransferObjects.Product;
using ONLINE_SHOP.Domain.Framework.Contracts.Response;
using ONLINE_SHOP.Domain.Framework.Models;

namespace ONLINE_SHOP.Domain.Models.Product;

public interface IProductQueryModel : IQueryModel
{
   Task<DataResponse<List<ProductDTO>>> GetProductListByIdsAsync(ProductListByIdsQuery query,
        CancellationToken cancellationToken);
}