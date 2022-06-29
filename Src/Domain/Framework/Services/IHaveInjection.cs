using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ONLINE_SHOP.Domain.Framework.Services;

public interface IHaveInjection
{
    void Inject(IServiceCollection collection, IConfiguration configuration);
}