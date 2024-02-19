using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.LocationService.Configurations.Configs;
using Services.LocationService.Registrations;

namespace Services.LocationService
{
    public static class DependencyInjection
    {
        public static IServiceCollection LocationServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            var config = GetConfigs.GetDatabaseConfig();

            services.SetupServiceRegistration()
                    .RepositoryServiceRegistration()
                    .UnitOfWorkServiceRegistration()
                    .MediatrServiceRegistration()
                    .MapperServiceRegistration()
                    .SeedServiceRegistration()
                    .ServiceRegistration()
                    .GrpcServiceRegistration();


            return services;
        }

        public static WebApplicationBuilder LocationServiceBuilderRegistration(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            builder.LoggerServiceRegistration(configuration);

            return builder;
        }
        public static WebApplication LocationServiceApplicationRegistration(this WebApplication app, IConfiguration configuration)
        {
            app.MiddlewaresApplicationRegistration()
               .GrpcApplicationRegistration();

            return app;
        }
    }
}
