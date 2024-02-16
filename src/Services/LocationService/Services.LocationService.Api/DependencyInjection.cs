
using Services.LocationService.Api.Registrations;

namespace Services.LocationService.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection LocationApiServiceApiService(this IServiceCollection services, IConfiguration configuration)
        {
            services.ControllerServiceRegistration()
                    .CorsServiceRegistration()
                    .HealthCheckServiceRegistration()
                    .SessionServiceRegistration()
                    .SwaggerServiceRegistration();

            return services;
        }

        public static WebApplication LocationApiApplicationService(this WebApplication app, IConfiguration configuration, IHostApplicationLifetime lifetime)
        {
            app.AuthenticationApplicationRegistration()
               .CorsApplicationRegistration()
               .ControllerApplicationRegistration()
               .HealthCheckApplicationRegistration()
               .SessionApplicationRegistration()
               .SwaggerApplicationRegistration();

            return app;
        }
    }
}
