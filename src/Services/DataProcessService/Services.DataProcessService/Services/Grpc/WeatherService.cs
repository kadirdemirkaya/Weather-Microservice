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

                return new() { DailyWeatherDataModel = dailyWeatherDataModel };
            }
            catch (Exception ex)
            {
                return new(default);
            }
        }
    }
}
