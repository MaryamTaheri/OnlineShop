using ONLINE_SHOP.Domain.ExternalServices.AccuWeather.Dtos;

namespace ONLINE_SHOP.Domain.ExternalServices.AccuWeather;

public interface IWeatherInformationServiceClient
{
    Task<WeatherInformationResponseDto> GetLastInformationAsync(CancellationToken cancellationToken);
}