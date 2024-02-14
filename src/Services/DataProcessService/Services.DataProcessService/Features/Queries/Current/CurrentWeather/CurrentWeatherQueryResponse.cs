using Services.DataProcessService.Models;

namespace Services.DataProcessService.Features.Queries.Current.CurrentWeather
{
    public record CurrentWeatherQueryResponse (
        CurrentWeatherModel CurrentWeatherModel
    );
}
