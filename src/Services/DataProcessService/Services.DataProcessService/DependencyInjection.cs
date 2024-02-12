using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.DataProcessService.Data;
using Services.DataProcessService.Registrations;
using Services.DataProcessService.Seeds;

namespace Services.DataProcessService
{
    public static class DependencyInjection
    {
        public static IServiceCollection DataProcessServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.DatabaseServiceRegistration(configuration)
                    .MapperServiceRegistration()
                    .MediatrServiceRegistration()
                    .RepositoryServiceRegistration()
                    .UnitOfWorkServiceRegistration()
                    .EventBusServiceRegistration(configuration)
                    .SeedServiceRegistration();

            return services;
        }

        public static WebApplication DataProcessServiceApplicationRegistration(this WebApplication app, IServiceProvider serviceProvider)
        {
            app.EventBusApplicationRegistration(serviceProvider)
                .MiddlewaresApplicationRegistration()
                .HostSettingServiceRegistration<WeatherDbContext>(async (context, serviceProvider) =>
                {
                });

            return app;
        }

        public static WebApplicationBuilder DataProcessServiceBuilderRegistration(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            builder.LoggerBuilderRegistration(configuration);

            return builder;
        }
    }
}
