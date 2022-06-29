using ONLINE_SHOP.Domain.ExternalServices.AccuWeather;
using ONLINE_SHOP.Domain.Framework.Services;
using ONLINE_SHOP.Infrastructure.ExternalServices.AccuWeather;
using ONLINE_SHOP.Infrastructure.ExternalServices.Helpers.Polly.CircuitBreaker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ONLINE_SHOP.Infrastructure.ExternalServices;

public class ExternalServiceHasInjection : IHaveInjection
{
    public void Inject(IServiceCollection collection, IConfiguration configuration)
    {
        collection.AddHttpClient<WeatherInformationServiceClient>("WeatherService")
            .AddPolicyHandler(PollyHelper.GetCircuitBreakerPolicy(0.9, 10, 2, 30));

        collection.AddScoped<IWeatherInformationServiceClient, WeatherInformationServiceClient>();
    }
}