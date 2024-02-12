using BuildingBlock.Base.Models;
using Services.DataProcessService.Models.Daily;

namespace Services.DataProcessService.Aggregate.Daily.Events
{
    public class DailyWeathIntegrationEvent : IntegrationEvent
    {
        public WeatherData WeatherData { get; set; }

        public DailyWeathIntegrationEvent(WeatherData weatherData)
        {
            WeatherData = weatherData;
        }
    }
}
