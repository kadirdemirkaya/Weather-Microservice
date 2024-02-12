using BuildingBlock.Base.Models;
using Services.DataProcessService.Models.Air;

namespace Services.DataProcessService.Aggregate.Air.Events
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
