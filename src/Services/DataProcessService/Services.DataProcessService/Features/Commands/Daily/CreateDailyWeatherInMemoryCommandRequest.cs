using MediatR;
using Services.DataProcessService.Models.Daily;

namespace Services.DataProcessService.Features.Commands.Daily
{
    public record CreateDailyWeatherInMemoryCommandRequest (
         Models.Coord coord,
         DailyWeatherModel DailyWeatherModel
    ) : IRequest<CreateDailyWeatherInMemoryCommandResponse>;
}
