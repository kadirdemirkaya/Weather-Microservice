using MediatR;

namespace Services.DataProcessService.Features.Queries.Air.AirPollutionInMemory
{
    public class AirPollutionInMemoryQueryHandler : IRequestHandler<AirPollutionInMemoryQueryRequest, AirPollutionInMemoryQueryResponse>
    {
        public Task<AirPollutionInMemoryQueryResponse> Handle(AirPollutionInMemoryQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
