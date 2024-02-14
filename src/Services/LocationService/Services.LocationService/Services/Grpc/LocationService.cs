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
            try
            {
                var reply = await _weatherService.GetAirPollutionAsync(request.City);

                var airPollutionModel = _mapper.Map<AirPollutionModel>(reply);

                RepeatedField<AirListModel> currentWeatherList = new RepeatedField<AirListModel>();
                foreach (var airListModel in airPollutionModel.AirListModel)
                {
                    var airList = _mapper.Map<AirListModel>(airListModel);
                    var airComponent = _mapper.Map<AirComponent>(airListModel.AirComponent);
                    var airmain = _mapper.Map<AirMain>(airListModel.Main);

                    airList.Main = airmain;
                    airList.AirComponent = airComponent;

                    currentWeatherList.Add(airList);
                }
                airPollutionModel.AirListModel.Add(currentWeatherList);

                return new() { AirPollutionModel = airPollutionModel };
            }
            catch (Exception ex)
            {
                return new(default);
            }
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


        public override async Task<DailyWeatherModelResponse> DailyWeather(DailyWeatherModelRequest request, ServerCallContext context)
        {
            try
            {
                var reply = await _weatherService.GetDailyWeatherAsync(request.City);

                DailyWeatherDataModel dailyWeatherDataModel = new();

                var dailyCity = _mapper.Map<DailyCity>(reply.DailyCity);
                var cityCoord = _mapper.Map<DailyCoord>(reply.DailyCity.DailyCoord);
                dailyCity.DailyCoord = cityCoord;
                RepeatedField<DailyListModel> dailyListModels = new RepeatedField<DailyListModel>();
                foreach (var dListModel in reply.DailyListModel)
                {
                    var dailyList = _mapper.Map<DailyListModel>(dListModel);
                    var dailyMain = _mapper.Map<DailyMain>(dListModel.DailyMain);
                    var dailyRain = _mapper.Map<DailyRain>(dListModel.DailyRain);
                    var dailyCloud = _mapper.Map<DailyCloud>(dListModel.DailyCloud);

                    dailyList.DailyCloud = dailyCloud;
                    dailyList.DailyMain = dailyMain;
                    dailyList.DailyRain = dailyRain;

                    RepeatedField<DailyWeatherModel> dailyWeatherModels = new RepeatedField<DailyWeatherModel>();
                    foreach (var dWeatherModel in dListModel.DailyWeatherModel)
                    {
                        var dailyWeatherModel = _mapper.Map<DailyWeatherModel>(dWeatherModel);

                        dailyWeatherModels.Add(dailyWeatherModel);
                    }

                    dailyList.DailyWeatherModel.AddRange(dailyWeatherModels);

                    dailyListModels.Add(dailyList);
                }

                dailyWeatherDataModel.DailyCity = dailyCity;
                dailyWeatherDataModel.DailyListModel.Add(dailyListModels);

                return new() { DailyWeatherDataModel = dailyWeatherDataModel };
            }
            catch (Exception ex)
            {
                return new(default);
            }
        }
    }
}
