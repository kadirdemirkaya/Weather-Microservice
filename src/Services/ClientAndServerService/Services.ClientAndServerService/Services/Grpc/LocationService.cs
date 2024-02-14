using BuildingBlock.Base.Exceptions;
using Grpc.Net.Client;
using LocationService;
using Microsoft.Extensions.Configuration;
using Serilog;
using Services.ClientAndServerService.Abstractions;

namespace Services.ClientAndServerService.Services.Grpc
{
    public class LocationService : ILocationService
    {
        private readonly IConfiguration _configuration;
        private GrpcChannel _grpcChannel;
        private GrpcLocation.GrpcLocationClient _grpcLocationClient;
        public LocationService(IConfiguration configuration)
        {
            _configuration = configuration;
            _grpcChannel = GrpcChannel.ForAddress(_configuration["LocationService:Url"]);
            _grpcLocationClient = new GrpcLocation.GrpcLocationClient(_grpcChannel);
        }

        public async Task<AirPollutionModelResponse> GetAirPollutionModelAsync(string city)
        {
            try
            {
                var request = new AirPollutionModelRequest() { City = city };
                var reply = await _grpcLocationClient.AirPollutionAsync(request);
                return new() { AirPollutionModel = reply.AirPollutionModel };
            }
            catch (Exception ex)
            {
                Log.Error("Grpc communication error : " + ex.Message);
                throw new ServiceErrorException(nameof(UserService), ex.Message);
            }
        }

        public async Task<CurrentWeatherModelResponse> GetCurrentWeatherModelResponse(string city)
        {
            try
            {
                var request = new CurrentWeatherModelRequest() { City = city };
                var reply = await _grpcLocationClient.CurrentWeatherAsync(request);
                return new() { CurrentWeatherModel = reply.CurrentWeatherModel };
            }
            catch (Exception ex)
            {
                Log.Error("Grpc communication error : " + ex.Message);
                throw new ServiceErrorException(nameof(UserService), ex.Message);
            }
        }


        public async Task<DailyWeatherModelResponse> GetDailyWeatherModelResponse(string city)
        {
            try
            {
                var request = new DailyWeatherModelRequest() { City = city };
                var reply = await _grpcLocationClient.DailyWeatherAsync(request);
                return new() { DailyWeatherDataModel = reply.DailyWeatherDataModel };
            }
            catch (Exception ex)
            {
                Log.Error("Grpc communication error : " + ex.Message);
                throw new ServiceErrorException(nameof(UserService), ex.Message);
            }
        }
    }
}
