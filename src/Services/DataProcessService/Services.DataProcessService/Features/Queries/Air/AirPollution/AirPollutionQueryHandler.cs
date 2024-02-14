using BuildingBlock.Base.Abstractions;
using MediatR;
using Services.DataProcessService.Abstractions;

namespace Services.DataProcessService.Features.Queries.Air.AirPollution
{
    public class AirPollutionQueryHandler : IRequestHandler<AirPollutionQueryRequest, AirPollutionQueryResponse>
    {
        private readonly IAirPollutionService _airPollutionService;

        public AirPollutionQueryHandler(IAirPollutionService airPollutionService)
        {
            _airPollutionService = airPollutionService;
        }

        public async Task<AirPollutionQueryResponse> Handle(AirPollutionQueryRequest request, CancellationToken cancellationToken)
            => new(await _airPollutionService.GetAirPollutionAsync(new() { lat = request.lat, lon = request.lon }));
    }
}
