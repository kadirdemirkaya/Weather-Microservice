using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Concretes;
using Microsoft.Extensions.DependencyInjection;
using Services.UserInfoService.Abstractions;
using Services.UserInfoService.Services;
using Services.UserInfoService.Services.Background;

namespace Services.UserInfoService.Registrations
{
    public static class Service
    {
        public static IServiceCollection ServiceRegistration(this IServiceCollection services)
        {
            services.AddHostedService<LogCleanupService>();

            services.AddScoped<IPubEventService, PubEventService>();

            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IUserService, UserService>();

            services.AddSingleton<ITokenBlacklistService, TokenBlacklistService>();

            return services;
        }
    }
}
