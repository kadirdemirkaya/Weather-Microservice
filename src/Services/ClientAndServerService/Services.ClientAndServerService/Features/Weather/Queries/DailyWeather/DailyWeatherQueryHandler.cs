using AutoMapper;
using MediatR;
using Services.ClientAndServerService.Abstractions;
using Services.ClientAndServerService.Models;

namespace Services.ClientAndServerService.Features.Weather.Queries.DailyWeather
{
    public class DailyWeatherQueryHandler : IRequestHandler<DailyWeatherQueryRequest, DailyWeatherQueryResponse>
    {
        private readonly ILocationService _locationService;
        private readonly IMapper _mapper;

        public DailyWeatherQueryHandler(ILocationService locationService, IMapper mapper)
        {
            _locationService = locationService;
            _mapper = mapper;
        }

        public async Task<DailyWeatherQueryResponse> Handle(DailyWeatherQueryRequest request, CancellationToken cancellationToken)
        {
            var dailyWeatherModelResponse = await _locationService.GetDailyWeatherModelResponse(request.City);
            
            // !
            var mapData = _mapper.Map<DailyWeatherDataModel>(dailyWeatherModelResponse.DailyWeatherDataModel);
            // !

            return new(mapData);
        }
    }
}
