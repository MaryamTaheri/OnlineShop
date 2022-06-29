using ONLINE_SHOP.Domain.Framework.Contracts.Response;
using ONLINE_SHOP.Domain.Framework.Logging;
using ONLINE_SHOP.Infrastructure.Framework.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ONLINE_SHOP.Presentation.WebApi;

[ApiController]
[Produces("application/json")]
[Route("api/[area]")]
public class BaseController : ControllerBase
{
    public BaseController()
    {
    }

    #region .:: RESPONSE HELPERS ::.

    [NonAction]
    protected StandardForcedResponse Dynamic(EmptyResponse response) => new(response);

    [NonAction]
    protected StandardForcedResponse<TData> Dynamic<TData>(DataResponse<TData> response) => new(response);

    #endregion
}