namespace Services.LocationService.Abstractions
{
    public interface IWeatherService
    {
        Task<DataCaptureService.AirPollutionModel> GetAirPollutionAsync(string city);

        Task<DataCaptureService.CurrentWeatherModel> GetCurrentWeatherAsync(string city);

        Task<DataCaptureService.DailyWeatherDataModel> GetDailyWeatherAsync(string city);
    }
}
