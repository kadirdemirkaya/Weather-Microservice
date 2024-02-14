using Services.ClientAndServerService.Models;

namespace Services.ClientAndServerService.Features.Weather.Queries.CurrentWeather
{
    public record CurrentWeatherQueryResponse(
        CurrentWeatherModel CurrentWeatherModel
    );
}
