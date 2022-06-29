using ONLINE_SHOP.Domain.ExternalServices.AccuWeather;
using ONLINE_SHOP.Domain.ExternalServices.AccuWeather.Dtos;
using ONLINE_SHOP.Domain.Framework.Exceptions;
using ONLINE_SHOP.Domain.Framework.Extensions;
using ONLINE_SHOP.Domain.Framework.Logging;
using Microsoft.Extensions.Configuration;

namespace ONLINE_SHOP.Infrastructure.ExternalServices.AccuWeather;

public class WeatherInformationServiceClient : WeatherBaseEndpoint, IWeatherInformationServiceClient
{
    public ILog Logger { get; }

    private const string RequestUri = "c8591abf-080a-46b5-bd2a-483c66398725?";

    public WeatherInformationServiceClient(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory, configuration)
    {
        Logger = LogManager.GetLogger<WeatherInformationServiceClient>();
    }

    public async Task<WeatherInformationResponseDto> GetLastInformationAsync(CancellationToken cancellationToken)
    {
        try
        {
            Logger.Info("start receiving data...");

            var response = await Connection.GetAsync(RequestUri, cancellationToken);

            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
                throw new Dexception(Situation.Make(SitKeys.Unprocessable),
                    new List<KeyValuePair<string, string>>
                        {new(":پیام:", "عدم برقراری ارتباط با سرویس آب و هوا")},
                    responseContent, payload: new Dictionary<string, string>
                    {
                        {"request", string.Empty},
                        {"response", responseContent},
                        {"statusCode", response.StatusCode.ToString()}
                    });

            var result = responseContent.Deserialize<WeatherInformationResponseDto>();

            if (result is null)
                throw new Dexception(Situation.Make(SitKeys.Unprocessable),
                    new List<KeyValuePair<string, string>> {new(":پیام:", "داده دریافتی از وب سرویس آب و هوا نامعتبر است!")});

            Logger.Info($"received data  with response: {responseContent}");

            return result;
        }
        catch (Exception exception)
        {
            Logger.Error($"error in receiving data. {exception.Message} {exception?.InnerException}");
            
            return null;
        }
    }
}