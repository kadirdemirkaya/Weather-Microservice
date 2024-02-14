using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Concrete;
using Microsoft.Extensions.DependencyInjection;
using Services.DataProcessService.Abstractions;
using Services.DataProcessService.Services;
using Services.DataProcessService.Services.Background;

namespace Services.DataProcessService.Registrations
{
    public static class Service
    {
        public static IServiceCollection ServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped<IAirPollutionService, AirPollutionService>();

            services.AddScoped<ICurrentWeatherService, CurrentWeatherService>();

            services.AddScoped<IDailyWeatherService, DailyWeatherService>();

            services.AddHostedService<LogCleanupService>();

            services.AddSingleton<ITokenBlacklistService, TokenBlacklistService>();

            return services;
        }
    }
}
