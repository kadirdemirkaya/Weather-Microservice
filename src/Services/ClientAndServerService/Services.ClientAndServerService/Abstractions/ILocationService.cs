using LocationService;

namespace Services.ClientAndServerService.Abstractions
{
    public interface ILocationService
    {
        Task<AirPollutionModelResponse> GetAirPollutionModelAsync(string city);

        Task<DailyWeatherModelResponse> GetDailyWeatherModelResponse(string city);

        Task<CurrentWeatherModelResponse> GetCurrentWeatherModelResponse(string city);
    }
}
