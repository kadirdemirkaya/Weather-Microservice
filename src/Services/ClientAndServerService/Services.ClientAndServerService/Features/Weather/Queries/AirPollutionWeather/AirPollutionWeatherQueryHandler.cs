using AutoMapper;
using MediatR;
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

            List<Models.AirListModel> airListModels = new List<Models.AirListModel>();
            foreach (var airListModel in airPollutionModelResponse.AirPollutionModel.AirListModel)
            {
                var airList = _mapper.Map<Models.AirListModel>(airListModel);
                var airComponent = _mapper.Map<Models.Air.Component>(airListModel.AirComponent);
                var airmain = _mapper.Map<Models.Air.Main>(airListModel.Main);

                airList.Main = airmain;
                airList.Component = airComponent;

                airListModels.Add(airList);
            }

            mapData.AirListModels.AddRange(airListModels);

            return new(mapData);
        }
    }
}
