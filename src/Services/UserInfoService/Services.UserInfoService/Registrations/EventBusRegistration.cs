using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Services.UserInfoService.Registrations
{
    public static class EventBusRegistration
    {
        public static IServiceCollection EventBusServiceRegistration(this IServiceCollection services)
        {
            //services.AddSingleton<IEventBus>(sp =>
            //{
            //    return EventBusFactory.Create(GetConfigs.GetEventBusConfig(), sp);
            //});

            //services.AddTransient<XIntegrationEventHandler>();

            return services;
        }

        public static WebApplication EventBusApplicationRegistration(this WebApplication app)
        {
            //var eventBus = serviceProvider.GetRequiredService<IEventBus>();

            //eventBus.Subscribe<XIntegrationEvent, XIntegrationEventHandler>();

            return app;
        }
    }
}
