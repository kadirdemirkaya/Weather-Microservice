namespace Services.LocationService.Abstractions
{
    public interface IWeatherService
    {
        Task<DataProcessService.AirPollutionModel> GetAirPollutionAsync(string city);

        Task<DataProcessService.CurrentWeatherModel> GetCurrentWeatherAsync(string city);

        Task<DataProcessService.DailyWeatherDataModel> GetDailyWeatherAsync(string city);
    }
}
