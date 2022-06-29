using ONLINE_SHOP.Domain.Contracts.Queries.Product;
using ONLINE_SHOP.Domain.Events.DataTransferObjects.Product;
using ONLINE_SHOP.Domain.Framework.Contracts.Response;
using ONLINE_SHOP.Domain.Framework.Services.Handlers;

namespace ONLINE_SHOP.Domain.Application.Queries.Product;

public interface IProductQueries :  
    IApplicationQueryHandler<ProductListByIdsQuery, DataResponse<List<ProductDTO>>>
{
}