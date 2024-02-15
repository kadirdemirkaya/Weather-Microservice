using BuildingBlock.Base.Abstractions;
using MediatR;
using Services.DataCaptureService.Events;

namespace Services.DataCaptureService.Features.Commands.Air.AirWeather
{
    public class AirWeatherCommandHandler : IRequestHandler<AirWeatherCommandRequest, AirWeatherCommandResponse>
    {
        private readonly IEventBus _eventBus;

        public AirWeatherCommandHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task<AirWeatherCommandResponse> Handle(AirWeatherCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Models.Air.WeatherData weatherData = System.Text.Json.JsonSerializer.Deserialize<Models.Air.WeatherData>(request.responseContent);

                _eventBus.Publish(new AirWeathIntegrationEvent(weatherData));
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw;
            }

            return new();
        }
    }
}
