using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Concrete;
using Microsoft.Extensions.DependencyInjection;
using Services.ClientAndServerService.Abstractions;
using Services.ClientAndServerService.Services.Grpc;

namespace Services.ClientAndServerService.Registrations
{
    public static class Service
    {
        public static IServiceCollection ServiceRegistration(this IServiceCollection services)
        {
            services.AddSingleton<ITokenBlacklistService, TokenBlacklistService>();

            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
