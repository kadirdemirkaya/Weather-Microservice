using MediatR;

namespace Services.DataCaptureService.Features.Commands.Air.AirWeather
{
    public record AirWeatherCommandRequest(
        string responseContent
    ) : IRequest<AirWeatherCommandResponse>;
}
