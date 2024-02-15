using Services.DataProcessService.Models.Daily;

namespace Services.DataProcessService.Features.Queries.Daily.GetDailyWeatherInMemmory
{
    public record GetDailyWeatherInMemmoryQueryResponse (
        DailyWeatherModel? DailyWeatherModel
    );
}
