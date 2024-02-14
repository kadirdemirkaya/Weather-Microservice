using Services.DataProcessService.Aggregate.Air.ValueObjects;
using Services.DataProcessService.Models.Daily;

namespace Services.DataProcessService.Abstractions
{
    public interface IDailyWeatherService
    {
        Task<DailyWeatherModel> GetDWeatherModelAsync(Models.Coord coord);
    }
}
