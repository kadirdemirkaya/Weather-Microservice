using BuildingBlock.Base.Exceptions;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Serilog;
using Services.ClientAndServerService.Abstractions;
using UserInfoService;

namespace Services.ClientAndServerService.Services.Grpc
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private GrpcChannel grpcChannel;
        private GrpcUser.GrpcUserClient grpcUserClient;
        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
            grpcChannel = GrpcChannel.ForAddress(_configuration["UserInfoService:Url"]);
            grpcUserClient = new GrpcUser.GrpcUserClient(grpcChannel);
        }

        public async Task<Models.UserLoginResponseModel> UserLoginAsync(Models.UserLoginModel userLoginModel)
        {
            try
            {
                var request = new UserLoginModelRequest() { UserLoginModel = new() { Email = userLoginModel.Email, Password = userLoginModel.Password } };

                var reply = await grpcUserClient.UserLoginAsync(request);
                return new() { IsSuccess = reply.Issuccess, Token = reply.Token };
            }
            catch (Exception ex)
            {
                Log.Error("Grpc communication error : " + ex.Message);
                throw new ServiceErrorException(nameof(UserService), ex.Message);
            }
        }

        public async Task<bool> UserLogoutAsync(string token)
        {
            try
            {
                var request = new UserLogoutModelRequest() { UserLogoutModel = new() { Token = token } };
                var reply = await grpcUserClient.UserLogoutAsync(request);
                return reply.Response;
            }
            catch (Exception ex)
            {
                Log.Error("Grpc communication error : " + ex.Message);
                throw new ServiceErrorException(nameof(UserService), ex.Message);
            }
        }

        public async Task<bool> UserRegisterAsync(Models.UserRegisterModel userRegisterModel)
        {
            try
            {
                var request = new UserRegisterModelRequest() { UserRegisterModel = new() { Email = userRegisterModel.Email, Password = userRegisterModel.Password } };
                var reply = await grpcUserClient.UserRegisterAsync(request);
                return reply.Response;
            }
            catch (Exception ex)
            {
                Log.Error("Grpc communication error : " + ex.Message);
                throw new ServiceErrorException(nameof(UserService), ex.Message);
            }
        }
    }
}
