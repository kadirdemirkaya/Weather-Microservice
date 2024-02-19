using Amazon.Runtime;
using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Concretes;
using Microsoft.Extensions.DependencyInjection;
using Services.LocationService.Abstractions;
using Services.LocationService.Services.Background;
using Services.LocationService.Services.Grpc;

namespace Services.LocationService.Registrations
{
    public static class Service
    {
        public static IServiceCollection ServiceRegistration(this IServiceCollection services)
        {
            services.AddHostedService<LogCleanupService>();

            services.AddScoped<IWeatherService, WeatherService>();

            services.AddSingleton<ITokenBlacklistService, TokenBlacklistService>();

            return services;
        }
    }
}
