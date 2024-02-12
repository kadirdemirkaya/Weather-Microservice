using MediatR;

namespace Services.DataCaptureService.Features.Commands.AirWeather
{
    public record AirWeatherCommandRequest(
        string responseContent
    ) : IRequest<AirWeatherCommandResponse>;
}
