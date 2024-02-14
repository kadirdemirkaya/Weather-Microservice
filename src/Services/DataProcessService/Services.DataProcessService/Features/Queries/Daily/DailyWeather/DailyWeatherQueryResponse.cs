using Services.DataProcessService.Models.Daily;

namespace Services.DataProcessService.Features.Queries.Daily.DailyWeather
{
    public record DailyWeatherQueryResponse(
        DailyWeatherModel DailyWeatherModel
    );
}
