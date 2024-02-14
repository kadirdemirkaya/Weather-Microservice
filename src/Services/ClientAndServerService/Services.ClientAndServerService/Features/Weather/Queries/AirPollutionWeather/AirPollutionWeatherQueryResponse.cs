using Services.ClientAndServerService.Models;

namespace Services.ClientAndServerService.Features.Weather.Queries.AirPollutionWeather
{
    public record AirPollutionWeatherQueryResponse (
        AirPollutionModel AirPollutionModel
    );
}
