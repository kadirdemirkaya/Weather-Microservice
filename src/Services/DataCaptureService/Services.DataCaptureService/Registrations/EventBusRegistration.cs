using BuildingBlock.Base.Abstractions;
using BuildingBlock.Factory.Factories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Services.DataCaptureService.Configurations.Configs;

namespace Services.DataCaptureService.Registrations
{
    public static class EventBusRegistration
    {
        public static IServiceCollection EventBusServiceRegistration(this IServiceCollection services)
        {
            services.AddSingleton<IEventBus>(sp =>
            {
                return EventBusFactory.Create(GetConfigs.GetEventBusConfig(), sp);
            });

            return services;
        }

        public static WebApplication EventBusApplicationRegistration(this WebApplication app, IServiceProvider serviceProvider)
        {
            //var eventBus = serviceProvider.GetRequiredService<IEventBus>();

            return app;
        }
    }
}
