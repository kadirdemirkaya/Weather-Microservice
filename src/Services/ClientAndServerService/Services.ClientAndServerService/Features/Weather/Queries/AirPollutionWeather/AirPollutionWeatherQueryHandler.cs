using AutoMapper;
using MediatR;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Services.ClientAndServerService.Abstractions;

namespace Services.ClientAndServerService.Features.Weather.Queries.AirPollutionWeather
{
    public class AirPollutionWeatherQueryHandler : IRequestHandler<AirPollutionWeatherQueryRequest, AirPollutionWeatherQueryResponse>
    {
        private readonly ILocationService _locationService;
        private readonly IMapper _mapper;
        public AirPollutionWeatherQueryHandler(ILocationService locationService, IMapper mapper)
        {
            _locationService = locationService;
            _mapper = mapper;
        }

        public async Task<AirPollutionWeatherQueryResponse> Handle(AirPollutionWeatherQueryRequest request, CancellationToken cancellationToken)
        {
            var airPollutionModelResponse = await _locationService.GetAirPollutionModelAsync(request.City);
            var mapData = _mapper.Map<Models.AirPollutionModel>(airPollutionModelResponse.AirPollutionModel);
            return new(mapData);
        }
    }
}
