using Microsoft.Extensions.DependencyInjection;
using Services.DataCaptureService.Services.Background;

namespace Services.DataCaptureService.Registrations
{
    public static class HttpClientRegistration
    {
        public static IServiceCollection HttpClientServiceRegistration(this IServiceCollection services)
        {
            services.AddHttpClient<CurrentWeatherService>();

            services.AddHttpClient<DailyWeatherService>();

            services.AddHttpClient<AirWeatherService>();

            return services;
        }
    }
}
