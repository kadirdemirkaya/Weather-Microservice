using MediatR;
using Services.DataProcessService.Models.Air;

namespace Services.DataProcessService.Features.Commands.Air.GetAirPollutionInMemory
{
    public record CreateAirPollutionInMemoryCommandRequest(
         Models.Air.Coord coord,
         AirPollutionModel? AirPollutionModel
    ) : IRequest<CreateAirPollutionInMemoryCommandResponse>;
}
