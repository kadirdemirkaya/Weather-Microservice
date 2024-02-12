using BuildingBlock.Base.Models;
using Services.DataCaptureService.Models.Daily;

namespace Services.DataCaptureService.Events
{
    public class DailyWeathIntegrationEvent : IntegrationEvent
    {
        public Models.Daily.WeatherData WeatherData { get; set; }

        public DailyWeathIntegrationEvent(WeatherData weatherData)
        {
            WeatherData = weatherData;
        }
    }
}
