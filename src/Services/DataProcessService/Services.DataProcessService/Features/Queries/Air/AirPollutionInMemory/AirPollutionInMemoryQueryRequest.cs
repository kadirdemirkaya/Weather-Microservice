using MediatR;

namespace Services.DataProcessService.Features.Queries.Air.AirPollutionInMemory
{
    public record AirPollutionInMemoryQueryRequest (
        
    ) : IRequest<AirPollutionInMemoryQueryResponse>;
}
