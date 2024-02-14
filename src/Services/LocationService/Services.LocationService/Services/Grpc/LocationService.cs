using AutoMapper;
using Google.Protobuf.Collections;
using Grpc.Core;
using LocationService;
using Services.LocationService.Abstractions;

namespace Services.LocationService.Services.Grpc
{
    public class LocationService : GrpcLocation.GrpcLocationBase
    {
        private readonly IWeatherService _weatherService;
        private readonly IMapper _mapper;
        public LocationService(IWeatherService weatherService, IMapper mapper)
        {
            _weatherService = weatherService;
            _mapper = mapper;
        }

        public override async Task<AirPollutionModelResponse> AirPollution(AirPollutionModelRequest request, ServerCallContext context)
        {
            var reply = await _weatherService.GetAirPollutionAsync(request.City);
            var airPollutionModel = _mapper.Map<AirPollutionModel>(reply);
            return new() { AirPollutionModel = airPollutionModel };
        }
        public override async Task<DailyWeatherModelResponse> DailyWeather(DailyWeatherModelRequest request, ServerCallContext context)
        {
            var reply = await _weatherService.GetDailyWeatherAsync(request.City);
            var dailyWeatherDataModel = _mapper.Map<DailyWeatherDataModel>(reply);
            return new() { DailyWeatherDataModel = dailyWeatherDataModel };
        }






        public override async Task<CurrentWeatherModelResponse> CurrentWeather(CurrentWeatherModelRequest request, ServerCallContext context)
        {
            try
            {
                var reply = await _weatherService.GetCurrentWeatherAsync(request.City);

                var currentWeatherModel = _mapper.Map<CurrentWeatherModel>(reply);
                var currentCloud = _mapper.Map<CurrentCloud>(reply.CurrentCloud);
                var currentRain = _mapper.Map<CurrentRain>(reply.CurrentRain);
                var currentSys = _mapper.Map<CurrentSys>(reply.CurrentSys);

                currentWeatherModel.CurrentCloud = currentCloud;
                currentWeatherModel.CurrentRain = currentRain;
                currentWeatherModel.CurrentSys = currentSys;

                RepeatedField<CurrentWeather> currentWeatherList = new RepeatedField<CurrentWeather>();
                foreach (var weather in reply.CurrentWeather)
                {
                    var currentWeatherMessage = _mapper.Map<CurrentWeather>(weather);
                    currentWeatherModel.CurrentWeather.Add(currentWeatherMessage);
                }

                return new() { CurrentWeatherModel = currentWeatherModel };
            }
            catch (Exception ex)
            {
                return new();
            }
        }
    }
}
