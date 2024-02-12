using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Configs;
using BuildingBlock.Base.Enums;
using BuildingBlock.Base.Options;
using BuildingBlock.RabbitMq;
using BuildingBlock.Redis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BuildingBlock.Factory.Factories
{
    public static class EventBusFactory
    {
        public static IEventBus Create(EventBusConfig config, IServiceProvider serviceProvider)
        {
            var eventBusOpt = serviceProvider.GetService<IOptions<EventBusOptions>>();

            return config.EventBusType switch
            {
                EventBusType.Redis => new EventBusRediss(config, serviceProvider),
                EventBusType.RabbitMQ => new EventBusRabbitMQ(config, serviceProvider),
                _ => new EventBusRabbitMQ(config, serviceProvider)
            };
        }
    }
}
