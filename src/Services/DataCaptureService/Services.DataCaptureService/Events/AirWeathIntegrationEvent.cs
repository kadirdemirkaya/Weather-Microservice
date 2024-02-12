using BuildingBlock.Base.Models;
using Services.DataCaptureService.Models.Air;

namespace Services.DataCaptureService.Events
{
    public class AirWeathIntegrationEvent : IntegrationEvent
    {
        public WeatherData WeatherData { get; set; }

        public AirWeathIntegrationEvent(WeatherData weatherData)
        {
            WeatherData = weatherData;
        }
    }
}
