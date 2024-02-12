using Services.DataCaptureService.Api.Registrations;

namespace Services.DataCaptureService.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection DataCaptureApiServiceRegistration(this IServiceCollection services)
        {
            services.ControllerServiceRegistration()
                    .CorsServiceRegistration()
                    .HealthCheckServiceRegistration()
                    .SessionServiceRegistration()
                    .SwaggerServiceRegistration();

            return services;
        }

        public static WebApplication DataCaptureApiServiceRegistration(this WebApplication app)
        {
            app.CorsApplicationRegistration()
               .ControllerApplicationRegistration()
               .HealthCheckApplicationRegistration()
               .SessionApplicationRegistration()
               .SwaggerApplicationRegistration()
               .AuthenticationApplicationRegistration();

            return app;
        }
    }
}
