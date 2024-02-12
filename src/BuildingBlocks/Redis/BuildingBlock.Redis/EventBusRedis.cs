using BuildingBlock.Base.Base;
using BuildingBlock.Base.Configs;
using BuildingBlock.Base.Models;
using Newtonsoft.Json;
using Polly;
using StackExchange.Redis;
using System.Net.Sockets;
using System.Text;

namespace BuildingBlock.Redis
{

    public class EventBusRediss : BaseEventBus
    {
        RedisPersistentConnection persistentConnection;
        private readonly IConnectionMultiplexer connectionFactory;
        private ISubscriber consumerChannel;
        private ISubscriber publisherChannel;
        public EventBusRediss(EventBusConfig config, IServiceProvider serviceProvider) : base(config, serviceProvider)
        {
            if (config.Connection != null)
            {
                var connJson = JsonConvert.SerializeObject(EventBusConfig.Connection, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                });

                connectionFactory = ConnectionMultiplexer.Connect(JsonConvert.DeserializeObject<string>(connJson));
                persistentConnection = new RedisPersistentConnection(connectionFactory, connJson, config.ConnectionRetryCount);
            }
            else
                connectionFactory = ConnectionMultiplexer.Connect("localhost:6379");

            SubsManager.OnEventRemoved += SubsManager_OnEventRemoved;
        }

        private ISubscriber CreateSubscriberChannel(string eventName)
        {
            if (!persistentConnection.IsConnected)
            {
                persistentConnection.TryConnect();
            }
            var channel = persistentConnection.GetSubscriber();
            channel.Subscribe(eventName);
            return channel;
        }

        private void SubsManager_OnEventRemoved(object sender, string eventName)
        {
            eventName = ProcessEventName(eventName);

            if (!persistentConnection.IsConnected)
            {
                persistentConnection.TryConnect();
            }

            consumerChannel.Unsubscribe(eventName);
        }

        public override void Publish(IntegrationEvent @event)
        {
            if (publisherChannel is null)
                publisherChannel = persistentConnection.GetSubscriber();

            if (!persistentConnection.IsConnected)
            {
                persistentConnection.TryConnect();
            }

            var policy = Policy.Handle<RedisConnectionException>()
               .Or<SocketException>()
               .WaitAndRetry(EventBusConfig.ConnectionRetryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
               {
                   //logging
               });

            var eventName = @event.GetType().Name;
            eventName = ProcessEventName(eventName);

            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);

            policy.Execute(() =>
            {
                publisherChannel.Publish(eventName, body);
            });
        }

        public override void Subscribe<T, TH>()
        {
            if (publisherChannel is null)
                publisherChannel = persistentConnection.GetSubscriber();

            var eventName = typeof(T).Name;
            eventName = ProcessEventName(eventName);

            consumerChannel = CreateSubscriberChannel(eventName);

            if (!SubsManager.HasSubscriptionForEvent(eventName))
            {
                if (!persistentConnection.IsConnected)
                {
                    persistentConnection.TryConnect();
                }

                consumerChannel.Subscribe(eventName, async (channel, message) =>
                {
                    var Message = Encoding.UTF8.GetString(message);
                    try
                    {
                        await ProcessEvent(eventName, Message);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                });

            }
            SubsManager.AddSubscription<T, TH>();
        }

        public override void UnSubscribe<T, TH>()
        {
            SubsManager.RemoveSubscription<T, TH>();
        }
    }
}
