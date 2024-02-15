using MediatR;
using Services.DataProcessService.Models;

namespace Services.DataProcessService.Features.Commands.Currnet.CreateCurrentWeatherInMemory
{
    public record CreateCurrentWeatherInMemoryCommandRequest (
         Models.Coord coord,
         CurrentWeatherModel? CurrentWeatherModel
    ) : IRequest<CreateCurrentWeatherInMemoryCommandResponse>;
}
