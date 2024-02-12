using Microsoft.Extensions.DependencyInjection;
using Services.DataCaptureService.Services.Background;

namespace Services.DataCaptureService.Registrations
{
    public static class Service
    {
        public static IServiceCollection ServiceRegistration(this IServiceCollection services)
        {
            services.AddHostedService<CurrentWeatherService>();

            services.AddHostedService<DailyWeatherService>();

            services.AddHostedService<AirWeatherService>();

            return services;
        }
    }
}
