using AutoMapper;
using DataCaptureService;
using Google.Protobuf.Collections;
using Grpc.Core;
using MediatR;
using Services.DataProcessService.Features.Queries.Air.AirPollution;
using Services.DataProcessService.Features.Queries.Current.CurrentWeather;
using Services.DataProcessService.Features.Queries.Daily.DailyWeather;

namespace Services.DataProcessService.Services.Grpc
{
    public class WeatherService : GrpcWeather.GrpcWeatherBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public WeatherService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override async Task<AirPollutionModelResponse> AirPollution(AirPollutionModelRequest request, ServerCallContext context)
        {
            try
            {
                AirPollutionQueryRequest airPollutionQueryRequest = new(request.Lat, request.Lon);
                AirPollutionQueryResponse airPollutionQueryResponse = await _mediator.Send(airPollutionQueryRequest);

                var airPollutionModel = _mapper.Map<AirPollutionModel>(airPollutionQueryResponse.AirPollutionModel);

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
                CurrentWeatherQueryRequest currentWeatherQueryRequest = new(request.Lat, request.Lon);
                CurrentWeatherQueryResponse currentWeatherQueryResponse = await _mediator.Send(currentWeatherQueryRequest);

                var currentWeatherModel = _mapper.Map<CurrentWeatherModel>(currentWeatherQueryResponse.CurrentWeatherModel);
                var currentCloud = _mapper.Map<CurrentCloud>(currentWeatherQueryResponse.CurrentWeatherModel.Cloud);
                var currentRain = _mapper.Map<CurrentRain>(currentWeatherQueryResponse.CurrentWeatherModel.Rain);
                var currentSys = _mapper.Map<CurrentSys>(currentWeatherQueryResponse.CurrentWeatherModel.Sys);

                currentWeatherModel.CurrentCloud = currentCloud;
                currentWeatherModel.CurrentRain = currentRain;
                currentWeatherModel.CurrentSys = currentSys;

                RepeatedField<CurrentWeather> currentWeatherList = new RepeatedField<CurrentWeather>();
                foreach (var weather in currentWeatherQueryResponse.CurrentWeatherModel.Weathers)
                {
                    var currentWeatherMessage = _mapper.Map<CurrentWeather>(weather);
                    currentWeatherModel.CurrentWeather.Add(currentWeatherMessage);
                }

                return new() { CurrentWeatherModel = currentWeatherModel };
            }
            catch (Exception ex)
            {
                return new(default);
            }
        }

        public override async Task<DailyWeatherModelResponse> DailyWeather(DailyWeatherModelRequest request, ServerCallContext context)
        {
            try
            {
                DailyWeatherQueryRequest dailyWeatherQueryRequest = new(request.Lat, request.Lon);
                DailyWeatherQueryResponse dailyWeatherQueryResponse = await _mediator.Send(dailyWeatherQueryRequest);

                var dailyWeatherDataModel = _mapper.Map<DailyWeatherDataModel>(dailyWeatherQueryResponse.DailyWeatherModel);

                var dailyCity = _mapper.Map<DailyCity>(dailyWeatherQueryResponse.DailyWeatherModel.City);
                var cityCoord = _mapper.Map<DailyCoord>(dailyWeatherQueryResponse.DailyWeatherModel.City.coord);
                dailyCity.DailyCoord = cityCoord;
                RepeatedField<DailyListModel> dailyListModels = new RepeatedField<DailyListModel>();
                foreach (var dListModel in dailyWeatherQueryResponse.DailyWeatherModel.DListModels)
                {
                    var dailyList = _mapper.Map<DailyListModel>(dListModel);
                    var dailyMain = _mapper.Map<DailyMain>(dListModel.Main);
                    var dailyRain = _mapper.Map<DailyRain>(dListModel.Rain);
                    var dailyCloud = _mapper.Map<DailyCloud>(dListModel.Cloud);

                    dailyList.DailyCloud = dailyCloud;
                    dailyList.DailyMain = dailyMain;
                    dailyList.DailyRain = dailyRain;

                    RepeatedField<DailyWeatherModel> dailyWeatherModels = new RepeatedField<DailyWeatherModel>();
                    foreach (var dWeatherModel in dListModel.DWeatherModels)
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
