using ONLINE_SHOP.Domain.Events.DataTransferObjects.Product;
using ONLINE_SHOP.Domain.Framework.Contracts.Response;
using ONLINE_SHOP.Domain.Framework.Services;

namespace ONLINE_SHOP.Domain.Contracts.Queries.Product;

public class ProductListByIdsQuery : IApplicationQuery<DataResponse<List<ProductDTO>>>
{
    public List<Guid> Ids { get; set; }
}