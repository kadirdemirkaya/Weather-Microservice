using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.ClientAndServerService.Registrations;

namespace Services.ClientAndServerService
{
    public static class DependencyInjection
    {
        public static IServiceCollection ClientAndServerServiceRegistration(this IServiceCollection services)
        {
            services.MapperServiceRegistration()
                    .MediatrServiceRegistration()
                    .ServiceRegistration()
                    .ValidationServiceRegistration();

            return services;
        }

        public static WebApplicationBuilder ClientAndServerBuilderRegistration(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            builder.LoggerBuilderRegistration(configuration);

            return builder;
        }

        public static WebApplication ClientAndServerApplicationRegistration(this WebApplication app)
        {
            app.MiddlewaresApplicationRegistration();

            return app;
        }
    }
}
