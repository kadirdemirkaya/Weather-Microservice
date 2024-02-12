using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.UserInfoService.Registrations;

namespace Services.UserInfoService
{
    public static class DependencyInjection
    {
        public static IServiceCollection UserInfoServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.DatabaseServiceRegistration()
                    .MapperServiceRegistration()
                    .MediatrServiceRegistration()
                    .EventBusServiceRegistration()
                    .RepositoryServiceRegistration()
                    .ServiceRegistration()
                    .UnitOfWorkServiceRegistration(services.BuildServiceProvider())
                    .JsonWebTokenServiceRegistration(configuration)
                    .ValidationServiceRegistration()
                    .GrpcServiceRegistration();

            return services;
        }
        public static WebApplication UserInfoServiceApplicationRegistration(this WebApplication app, IConfiguration configuration)
        {
            app.MiddlewaresApplicationRegistration()
               .GrpcApplicationRegistration()
               .HostApplicationRegistration();

            return app;
        }
        public static WebApplicationBuilder UserInfoServiceBuilderRegistration(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            builder.LoggerBuilderRegistration(configuration);

            return builder;
        }


    }
}
