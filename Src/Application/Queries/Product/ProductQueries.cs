using ONLINE_SHOP.Domain.Application.Queries.Product;
using ONLINE_SHOP.Domain.Contracts.Queries.Product;
using ONLINE_SHOP.Domain.Events.DataTransferObjects.Product;
using ONLINE_SHOP.Domain.Framework.Contracts.Response;
using ONLINE_SHOP.Domain.Models.Product;

namespace ONLINE_SHOP.Application.Queries.Product;

public class ProductQueries : QueriesBase, IProductQueries
{
    private readonly IProductQueryModel _productQueryModel;

    public ProductQueries(IProductQueryModel productQueryModel) => _productQueryModel = productQueryModel;

    public async Task<DataResponse<List<ProductDTO>>> Handle(ProductListByIdsQuery query, CancellationToken cancellationToken)
    => await _productQueryModel.GetProductListByIdsAsync(query, cancellationToken);
}