
namespace ONLINE_SHOP.Domain.ExternalServices.AccuWeather.Dtos;

public class WeatherInformationResponseDto
{
    public DateTime? Date { get; set; }
    public double? Lat { get; set; }
    public double? Lon { get; set; }
    public string Unit { get; set; }
    public double? Temperature { get; set; }
}