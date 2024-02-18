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
            Console.WriteLine("MONGODB URL --------------------=> " + config.ConnectionString);
            Serilog.Log.Warning("MONGODB URL --------------------=> " + config.ConnectionString);

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
            var config = GetConfigs.GetDatabaseConfig();
            Console.WriteLine("MONGODB URL --------------------=> " + config.ConnectionString);
            Serilog.Log.Warning("MONGODB URL --------------------=> " + config.ConnectionString);

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
