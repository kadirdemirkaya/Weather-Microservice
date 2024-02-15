using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Extensions;
using MediatR;
using Services.DataProcessService.Aggregate.Air;
using Services.DataProcessService.Aggregate.Air.ValueObjects;
using Services.DataProcessService.Models.Air;

namespace Services.DataProcessService.Features.Commands.Air.GetAirPollutionInMemory
{
    public class CreateAirPollutionInMemoryCommandHandler : IRequestHandler<CreateAirPollutionInMemoryCommandRequest, CreateAirPollutionInMemoryCommandResponse>
    {
        private readonly IRedisService<AirPollutionWeather, AirPollutionWeatherId> _redisService;

        public CreateAirPollutionInMemoryCommandHandler(IRedisService<AirPollutionWeather, AirPollutionWeatherId> redisService)
        {
            _redisService = redisService;
        }

        public async Task<CreateAirPollutionInMemoryCommandResponse> Handle(CreateAirPollutionInMemoryCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                int count = _redisService.GetStringKeyCount();
                if (count == 10)
                    return new(true);
                string key = KeyFormatterExtension.Format(nameof(AirPollutionModel), request.coord.lat, request.coord.lon);
                return new(_redisService.Add(key, request.AirPollutionModel, TimeSpanExtension.AddMinute(200)));
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
