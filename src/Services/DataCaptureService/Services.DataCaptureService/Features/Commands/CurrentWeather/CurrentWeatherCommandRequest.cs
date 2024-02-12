using MediatR;

namespace Services.DataCaptureService.Features.Commands.CurrentWeather
{
    public record CurrentWeatherCommandRequest(
        string responseContent
    ) : IRequest<CurrentWeatherCommandResponse>;
}
