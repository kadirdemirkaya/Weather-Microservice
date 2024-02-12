using Services.DataProcessService.Api.Registrations;

namespace Services.DataProcessService.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection DataProcessApiServiceRegistration(this IServiceCollection services)
        {

            services.ControllerServiceRegistration()
                    .CorsServiceRegistration()
                    .HealthCheckServiceRegistration()
                    .SessionServiceRegistration()
                    .SwaggerServiceRegistration();

            return services;
        }

        public static WebApplication DataProcessApiApplicationRegistration(this WebApplication app)
        {
            app.CorsApplicationRegistration()
               .HealthCheckApplicationRegistration()
               .SessionApplicationRegistration()
               .SwaggerApplicationRegistration()
               .AuthenticationApplicationRegistration()
               .ControllerApplicationRegistration();

            return app;
        }
    }
}
