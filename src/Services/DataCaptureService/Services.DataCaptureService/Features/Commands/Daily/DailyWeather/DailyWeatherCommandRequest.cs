using MediatR;

namespace Services.DataCaptureService.Features.Commands.Daily.DailyWeather
{
    public record DailyWeatherCommandRequest(
       string responseContent
    ) : IRequest<DailyWeatherCommandResponse>;
}
