using BuildingBlock.Base.Exceptions;
using DataProcessService;
using Grpc.Net.Client;
using MediatR;
using Microsoft.Extensions.Configuration;
using Serilog;
using Services.LocationService.Abstractions;
using Services.LocationService.Features.Queries.Location.AirPollution;
using Services.LocationService.Features.Queries.Location.CurrentWeather;
using Services.LocationService.Features.Queries.Location.DailyWeather;

namespace Services.LocationService.Services.Grpc
{
    public class WeatherService : IWeatherService
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        private GrpcChannel _grpcChannel;
        private GrpcWeather.GrpcWeatherClient _grpcWeatherClient;

        public WeatherService(IMediator mediator, IConfiguration configuration)
        {
            _configuration = configuration;
            _mediator = mediator;
            _grpcChannel = GrpcChannel.ForAddress(_configuration["DataProcessService:Url"]);
            _grpcWeatherClient = new GrpcWeather.GrpcWeatherClient(_grpcChannel);
        }

        public async Task<AirPollutionModel> GetAirPollutionAsync(string city)
        {
            AirPollutionQueryRequest airPollutionQueryRequest = new(city);
            AirPollutionQueryResponse airPollutionQueryResponse = await _mediator.Send(airPollutionQueryRequest);

            try
            {
                var request = new AirPollutionModelRequest() { Lat = airPollutionQueryResponse.Latitude, Lon = airPollutionQueryResponse.Longitude };
                var reply = await _grpcWeatherClient.AirPollutionAsync(request);
                return reply.AirPollutionModel;
            }
            catch (Exception ex)
            {
                Log.Error("Grpc communication error : " + ex.Message);
                throw new ServiceErrorException(nameof(WeatherService), ex.Message);
            }
        }

        public async Task<CurrentWeatherModel> GetCurrentWeatherAsync(string city)
        {
            CurrentWeatherQueryRequest currentWeatherQueryRequest = new(city);
            CurrentWeatherQueryResponse currentWeatherQueryResponse = await _mediator.Send(currentWeatherQueryRequest);

            try
            {
                var request = new CurrentWeatherModelRequest() { Lat = currentWeatherQueryResponse.Latitude, Lon = currentWeatherQueryResponse.Longitude };
                var reply = await _grpcWeatherClient.CurrentWeatherAsync(request);
                return reply.CurrentWeatherModel;
            }
            catch (Exception ex)
            {
                Log.Error("Grpc communication error : " + ex.Message);
                throw new ServiceErrorException(nameof(WeatherService), ex.Message);
            }
        }


















        public async Task<DailyWeatherDataModel> GetDailyWeatherAsync(string city)
        {
            DailyWeatherQueryRequest dailyWeatherQueryRequest = new(city);
            DailyWeatherQueryResponse dailyWeatherQueryResponse = await _mediator.Send(dailyWeatherQueryRequest);

            try
            {
                var request = new DailyWeatherModelRequest() { Lat = dailyWeatherQueryResponse.Latitude, Lon = dailyWeatherQueryResponse.Longitude };
                var reply = await _grpcWeatherClient.DailyWeatherAsync(request);
                return reply.DailyWeatherDataModel;
            }
            catch (Exception ex)
            {
                Log.Error("Grpc communication error : " + ex.Message);
                throw new ServiceErrorException(nameof(WeatherService), ex.Message);
            }
        }
    }
}
