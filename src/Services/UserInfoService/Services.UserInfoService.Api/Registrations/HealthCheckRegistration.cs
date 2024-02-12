using BuildingBlock.HealthCheck;

namespace Services.UserInfoService.Api.Registrations
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
