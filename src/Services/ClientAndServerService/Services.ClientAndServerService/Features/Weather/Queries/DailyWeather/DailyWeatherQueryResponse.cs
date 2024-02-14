using Services.ClientAndServerService.Models;

namespace Services.ClientAndServerService.Features.Weather.Queries.DailyWeather
{
    public record DailyWeatherQueryResponse (
        DailyWeatherDataModel DailyWeatherDataModel
    );
}
