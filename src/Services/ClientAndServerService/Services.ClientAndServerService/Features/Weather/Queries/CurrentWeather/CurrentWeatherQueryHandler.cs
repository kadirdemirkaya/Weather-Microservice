using AutoMapper;
using Google.Protobuf.Collections;
using MediatR;
using Services.ClientAndServerService.Abstractions;
using Services.ClientAndServerService.Models;
using Services.ClientAndServerService.Models.Current;

namespace Services.ClientAndServerService.Features.Weather.Queries.CurrentWeather
{
    public class CurrentWeatherQueryHandler : IRequestHandler<CurrentWeatherQueryRequest, CurrentWeatherQueryResponse>
    {
        private readonly ILocationService _locationService;
        private readonly IMapper _mapper;
        public CurrentWeatherQueryHandler(ILocationService locationService, IMapper mapper)
        {
            _locationService = locationService;
            _mapper = mapper;
        }

        public async Task<CurrentWeatherQueryResponse> Handle(CurrentWeatherQueryRequest request, CancellationToken cancellationToken)
        {
            var currentWeatherModelResponse = await _locationService.GetCurrentWeatherModelResponse(request.City);

            var currentWeatherModel = _mapper.Map<CurrentWeatherModel>(currentWeatherModelResponse.CurrentWeatherModel);
            var currentCloud = _mapper.Map<Cloud>(currentWeatherModelResponse.CurrentWeatherModel.CurrentCloud);
            var currentRain = _mapper.Map<Rain>(currentWeatherModelResponse.CurrentWeatherModel.CurrentRain);
            var currentSys = _mapper.Map<Sys>(currentWeatherModelResponse.CurrentWeatherModel.CurrentSys);

            currentWeatherModel.Cloud = currentCloud;
            currentWeatherModel.Rain = currentRain;
            currentWeatherModel.Sys = currentSys;

            List<Models.Current.Weather> weathers = new();
            foreach (var weather in currentWeatherModelResponse.CurrentWeatherModel.CurrentWeather)
            {
                var weatherModel = _mapper.Map<Models.Current.Weather>(weather);
                weathers.Add(weatherModel);
            }
            currentWeatherModel.Weathers = weathers;

            return new(currentWeatherModel);

        }
    }
}
