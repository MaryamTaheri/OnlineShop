using ONLINE_SHOP.Domain.Contracts.Queries.Product;
using ONLINE_SHOP.Domain.Events.DataTransferObjects.Product;
using ONLINE_SHOP.Domain.Framework.Contracts.Response;
using ONLINE_SHOP.Domain.Framework.Exceptions;
using ONLINE_SHOP.Domain.Framework.Models;
using ONLINE_SHOP.Domain.Models.Product;
using ONLINE_SHOP.Domain.Repositories.Product;

namespace ONLINE_SHOP.Infrastructure.Models.Product;

public class ProductQueryModel : QueryModelBase, IProductQueryModel
{
    private readonly IProductQueryRepository _productQueryRepository;

    public ProductQueryModel(IProductQueryRepository productQueryRepository)
    {
        _productQueryRepository = productQueryRepository;
    }

    public async Task<DataResponse<List<ProductDTO>>> GetProductListByIdsAsync(ProductListByIdsQuery query,
        CancellationToken cancellationToken)
    {
        var products = await _productQueryRepository.FindByIdsAsync(query.Ids, cancellationToken);
         if (!products.Any())
            throw new Dexception(Situation.Make(SitKeys.Unprocessable),
                                    new List<KeyValuePair<string, string>> { new(":پیام:", "کالاهای انتخاب شده در سامانه وجود ندارد.") });

        //TODO
        //Can use Auto mapper
        var result = new List<ProductDTO>();
        foreach (var item in products)
        {
            result.Add(new ProductDTO
            {
                Id = item.Id,
                Name = item.Name,
                Category = item.Category,
                Price = item.Price,
                Type = item.Type
            });
        }

        return DataResponse<List<ProductDTO>>.Instance(result);
    }
}