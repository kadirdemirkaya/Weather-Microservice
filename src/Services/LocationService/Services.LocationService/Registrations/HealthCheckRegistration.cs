using BuildingBlock.HealthCheck;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Services.LocationService.Registrations
{
    public static class HealthCheckRegistration
    {
        public static IServiceCollection HealthCheckServiceRegistration(this IServiceCollection services)
        {
            services.HealthCheckServiceExtension();

            return services;
        }

        public static WebApplication HealthCheckApplicationRegistration(this WebApplication app)
        {
            app.HealthCheckApplicationExtension();

            return app;
        }
    }
}
