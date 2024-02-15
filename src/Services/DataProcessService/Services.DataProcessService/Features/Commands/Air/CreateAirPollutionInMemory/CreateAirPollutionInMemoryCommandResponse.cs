using Services.DataProcessService.Aggregate.Air;

namespace Services.DataProcessService.Features.Commands.Air.GetAirPollutionInMemory
{
    public record CreateAirPollutionInMemoryCommandResponse(
        bool response
    );
}
