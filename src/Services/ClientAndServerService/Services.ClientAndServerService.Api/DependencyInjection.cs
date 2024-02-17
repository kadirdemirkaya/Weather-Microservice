using Services.ClientAndServerService.Api.Registrations;

namespace Services.ClientAndServerService.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection ClientAndServerApiServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.ControllerServiceRegistration()
                    .CorsServiceRegistration()
                    .HealthCheckServiceRegistration()
                    .SessionServiceRegistration()
                    .SwaggerServiceRegistration();
                    //.ConsulServiceRegistration();

            return services;
        }

        public static WebApplication ClientAndServerApiApplicationRegistration(this WebApplication app, IConfiguration configuration)
        {
            app.CorsApplicationRegistration()
               .HealthCheckApplicationRegistration()
               .SessionApplicationRegistration()
               .SwaggerApplicationRegistration()
               .AuthenticationApplicationRegistration()
               .ControllerApplicationRegistration();
               //.ConsulApplicationBuilderRegsitration(app.Services.GetRequiredService<IHostApplicationLifetime>(), configuration);

            return app;
        }

    }
}
