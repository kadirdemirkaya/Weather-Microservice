using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Extensions;
using MediatR;
using Services.DataProcessService.Models.Air;
using Services.DataProcessService.Aggregate.Air;
using Services.DataProcessService.Aggregate.Air.ValueObjects;

namespace Services.DataProcessService.Features.Queries.Air.AirPollutionInMemory
{
    public class GetAirPollutionInMemoryQueryHandler : IRequestHandler<GetAirPollutionInMemoryQueryRequest, GetAirPollutionInMemoryQueryResponse>
    {
        private readonly IRedisService<AirPollutionWeather, AirPollutionWeatherId> _redisService;

        public GetAirPollutionInMemoryQueryHandler(IRedisService<AirPollutionWeather, AirPollutionWeatherId> redisService)
        {
            _redisService = redisService;
        }

        public async Task<GetAirPollutionInMemoryQueryResponse> Handle(GetAirPollutionInMemoryQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                string key = KeyFormatterExtension.Format(nameof(AirPollutionModel), request.coord.lat, request.coord.lon);
                AirPollutionModel? data = _redisService.Get<AirPollutionModel>(key);
                return new(data);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
