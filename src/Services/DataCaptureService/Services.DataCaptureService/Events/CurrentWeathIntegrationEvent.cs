using BuildingBlock.Base.Models;
using Services.DataCaptureService.Models;

namespace Services.DataCaptureService.Events
{
    public class CurrentWeathIntegrationEvent : IntegrationEvent
    {
        public Models.WeatherData WeatherData { get; set; }

        public CurrentWeathIntegrationEvent(WeatherData weatherData)
        {
            WeatherData = weatherData;
        }
    }
}
