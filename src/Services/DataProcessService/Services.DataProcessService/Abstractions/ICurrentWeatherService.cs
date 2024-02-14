using Services.DataProcessService.Models;

namespace Services.DataProcessService.Abstractions
{
    public interface ICurrentWeatherService
    {
        Task<CurrentWeatherModel> GetCurrentWeatherModelAsync(Models.Coord coord);
    }
}
