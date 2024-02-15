using BuildingBlock.Base.Abstractions;
using MediatR;
using Newtonsoft.Json.Linq;
using Serilog;
using Services.DataCaptureService.Events;

namespace Services.DataCaptureService.Features.Commands.Daily.DailyWeather
{
    public class DailyWeatherCommandHandler : IRequestHandler<DailyWeatherCommandRequest, DailyWeatherCommandResponse>
    {
        private readonly IEventBus _eventBus;

        public DailyWeatherCommandHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task<DailyWeatherCommandResponse> Handle(DailyWeatherCommandRequest request, CancellationToken cancellationToken)
        {
            List<double> rains = new();
            int i = 0;
            try
            {
                Models.Daily.WeatherData weatherData = System.Text.Json.JsonSerializer.Deserialize<Models.Daily.WeatherData>(request.responseContent);

                JObject jsonObject = JObject.Parse(request.responseContent);

                foreach (var item in jsonObject["list"])
                {
                    JToken rainToken = item["rain"]?["3h"];
                    double rainValue = rainToken?.Value<double>() ?? 0;
                    rains.Add(rainValue);
                }
                foreach (var lst in weatherData.list)
                {
                    if (rains[i] != 0)
                        lst.rain._3h = rains[i];
                    else
                        lst.rain = new();
                    i++;
                }

                DailyWeathIntegrationEvent dailyWeatherCommandRequest = new(weatherData);

                _eventBus.Publish(dailyWeatherCommandRequest);
            }
            catch (Exception ex)
            {
                Log.Error("Error message : " + ex.Message);
                throw new Exception(ex.Message);
            }

            return default;
        }
    }
}
