using Services.UserInfoService.Api.Registrations;

namespace Services.UserInfoService.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection UserInfoApiServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.ControllerServiceRegistration()
                    .CorsServiceRegistration()
                    .HealthCheckServiceRegistration()
                    .SessionServiceRegistration()
                    .SwaggerServiceRegistration()
                    .ConsulServiceRegistration();

            return services;
        }

        public static WebApplication UserInfoApiServiceApplicationRegistration(this WebApplication app, IConfiguration configuration)
        {
            app.CorsApplicationRegistration()
               .HealthCheckApplicationRegistration()
               .SessionApplicationRegistration()
               .SwaggerApplicationRegistration()
               .AuthenticationApplicationRegistration()
               .ControllerApplicationRegistration()
               .ConsulApplicationBuilderRegsitration(app.Services.GetRequiredService<IHostApplicationLifetime>(), configuration);

            return app;
        }


    }
}
