using MediatR;

namespace Services.DataCaptureService.Features.Commands.Current.CurrentWeather
{
    public record CurrentWeatherCommandRequest(
        string responseContent
    ) : IRequest<CurrentWeatherCommandResponse>;
}
