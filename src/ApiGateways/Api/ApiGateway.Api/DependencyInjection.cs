using ApiGateway.Api.Registrations;

namespace ApiGateway.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection ApiGatewayServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConsulServiceRegistration()
                    .ControllerServiceRegistration()
                    .CorsServiceRegistration()
                    .HealthCheckServiceRegistration()
                    .SessionServiceRegistration()
                    .SwaggerServiceRegistration();

            return services;
        }

        public static WebApplication ApiGatewayApplicationRegistration(this WebApplication app, IConfiguration configuration)
        {
            app.ControllerApplicationRegistration()
               .CorsApplicationRegistration()
               .HealthCheckApplicationRegistration()
               .SessionApplicationRegistration()
               .SwaggerApplicationRegistration()
               .ConsulApplicationBuilderRegsitration(app.Services.GetRequiredService<IHostApplicationLifetime>(), configuration);

            return app;
        }

        public static WebApplicationBuilder ApiGatewayBuilderRegistration(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            builder.LoggerBuilderRegistration(configuration)
                   .ConfigurationBuilderRegistration();

            return builder;
        }
    }
}
