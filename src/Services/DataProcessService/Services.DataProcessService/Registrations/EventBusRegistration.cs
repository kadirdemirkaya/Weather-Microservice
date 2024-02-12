using BuildingBlock.Base.Abstractions;
using BuildingBlock.Factory.Factories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.DataProcessService.Aggregate.Air.Events;
using Services.DataProcessService.Aggregate.Current.Events;
using Services.DataProcessService.Aggregate.Daily.Events;
using Services.DataProcessService.Configurations.Configs;
using Services.DataProcessService.Events.EventHandlers;

namespace Services.DataProcessService.Registrations
{
    public static class EventBusRegistration
    {
        public static IServiceCollection EventBusServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IEventBus>(sp =>
            {
                return EventBusFactory.Create(GetConfigs.GetEventBusConfig(), sp);
            });

            services.AddTransient<CurrentWeathIntegrationEventHandler>();
            services.AddTransient<DailyWeathIntegrationEventHandler>();
            services.AddTransient<AirWeathIntegrationEventHandler>();

            return services;
        }

        public static WebApplication EventBusApplicationRegistration(this WebApplication app, IServiceProvider serviceProvider)
        {
            var eventBus = serviceProvider.GetRequiredService<IEventBus>();

            eventBus.Subscribe<CurrentWeathIntegrationEvent, CurrentWeathIntegrationEventHandler>();
            eventBus.Subscribe<DailyWeathIntegrationEvent, DailyWeathIntegrationEventHandler>();
            eventBus.Subscribe<AirWeathIntegrationEvent, AirWeathIntegrationEventHandler>();

            return app;
        }
    }
}
