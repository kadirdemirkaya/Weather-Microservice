using BuildingBlock.Base.Models;
using Services.DataProcessService.Models;

namespace Services.DataProcessService.Aggregate.Current.Events
{
    public class CurrentWeathIntegrationEvent : IntegrationEvent
    {
        public WeatherData WeatherData { get; set; }

        public CurrentWeathIntegrationEvent(WeatherData weatherData)
        {
            WeatherData = weatherData;
        }
    }
}
