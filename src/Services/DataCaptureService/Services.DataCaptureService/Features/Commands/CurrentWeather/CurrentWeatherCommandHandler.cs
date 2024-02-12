using BuildingBlock.Base.Abstractions;
using MediatR;
using Newtonsoft.Json.Linq;
using Services.DataCaptureService.Events;

namespace Services.DataCaptureService.Features.Commands.CurrentWeather
{
    public class CurrentWeatherCommandHandler : IRequestHandler<CurrentWeatherCommandRequest, CurrentWeatherCommandResponse>
    {
        private readonly IEventBus _eventBus;
        public CurrentWeatherCommandHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task<CurrentWeatherCommandResponse> Handle(CurrentWeatherCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Models.WeatherData weatherData = System.Text.Json.JsonSerializer.Deserialize<Models.WeatherData>(request.responseContent);

                JObject jsonObject = JObject.Parse(request.responseContent);

                var item = jsonObject["rain"] ?? null;
                if (item is null)
                    weatherData.rain = new();
                else
                    weatherData.rain._1h = item["1h"]?.Value<double>() ?? 0;


                _eventBus.Publish(new CurrentWeathIntegrationEvent(weatherData));
            }
            catch (Exception)
            {

                throw;
            }


            return default;
        }
    }
}
