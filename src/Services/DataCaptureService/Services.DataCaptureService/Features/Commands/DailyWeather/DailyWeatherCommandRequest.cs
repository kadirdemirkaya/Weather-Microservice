using MediatR;

namespace Services.DataCaptureService.Features.Commands.DailyWeather
{
    public record DailyWeatherCommandRequest(
       string responseContent
    ) : IRequest<DailyWeatherCommandResponse>;
}
