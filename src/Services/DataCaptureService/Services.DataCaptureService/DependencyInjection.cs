using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.DataCaptureService.Registrations;

namespace Services.DataCaptureService
{
    public static class DependencyInjection
    {
        public static IServiceCollection DataCaptureServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.MediatrServiceRegistration()
                    .EventBusServiceRegistration()
                    .HttpClientServiceRegistration()
                    .ServiceRegistration();

            return services;
        }

        public static WebApplicationBuilder DataCaptureBuilderRegistration(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            builder.LoggerBuilderRegistration(configuration);

            return builder;
        }

        public static WebApplication DataCaptureApplicationRegistration(this WebApplication app, IServiceProvider serviceProvider)
        {
            app.EventBusApplicationRegistration(serviceProvider);

            return app;
        }
    }
}
