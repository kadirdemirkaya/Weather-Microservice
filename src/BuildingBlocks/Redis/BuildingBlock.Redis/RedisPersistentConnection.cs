using BuildingBlock.Base.Configs;
using BuildingBlock.Base.Options;
using Newtonsoft.Json;
using Polly;
using StackExchange.Redis;
using System.Net.Sockets;

namespace BuildingBlock.Redis
{
    public class RedisPersistentConnection : IDisposable
    {
        private IConnectionMultiplexer connection { get; set; }
        private object lock_object = new object();
        private readonly int RetryCount;
        private bool _disposed;
        private string _conString;
        private RedisConfig RedisConfig;
        private EventBusConfig EventBusConfig;
        private InMemoryOptions inMemoryOptions;

        public RedisPersistentConnection(IConnectionMultiplexer redis, InMemoryOptions inMemoryOptions, int retryCount = 5)
        {
            connection = redis;
            RetryCount = retryCount;
            inMemoryOptions = inMemoryOptions;

            if (inMemoryOptions.Connection != null)
            {
                var connJson = JsonConvert.SerializeObject(RedisConfig.Connection, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                });
                _conString = connJson;
            }
        }

        public RedisPersistentConnection(IConnectionMultiplexer redis, RedisConfig redisConfig, int retryCount = 5)
        {
            connection = redis;
            RetryCount = retryCount;
            inMemoryOptions = inMemoryOptions;

            if (inMemoryOptions.Connection != null)
            {
                var connJson = JsonConvert.SerializeObject(RedisConfig.Connection, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                });
                _conString = connJson;
            }
        }

        public RedisPersistentConnection(IConnectionMultiplexer redis, InMemoryOptions inMemoryOptions)
        {
            connection = redis;
            this.inMemoryOptions = inMemoryOptions;
            RetryCount = inMemoryOptions.RetryCount;
            if (inMemoryOptions.Connection != null)
            {
                var connJson = JsonConvert.SerializeObject(inMemoryOptions.Connection, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                });
                _conString = connJson;
            }
        }

        public RedisPersistentConnection(IConnectionMultiplexer redis, EventBusConfig eventBusConfig, int retryCount = 5)
        {
            connection = redis;
            RetryCount = retryCount;
            EventBusConfig = eventBusConfig;

            if (eventBusConfig.Connection != null)
            {
                var connJson = JsonConvert.SerializeObject(EventBusConfig.Connection, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                });
                _conString = connJson;
            }
        }


        public RedisPersistentConnection(IConnectionMultiplexer redis, string redisConfig, int retryCount = 5)
        {
            connection = redis;
            RetryCount = retryCount;
            _conString = redisConfig;
        }

        public IConnectionMultiplexer GetConnection
        {
            get
            {
                lock (lock_object)
                {
                    if (connection is null)
                        connection = ConnectionMultiplexer.Connect(JsonConvert.DeserializeObject<string>(_conString));
                    return connection;
                }
            }
        }

        public string ConnectionString
        {
            get
            {
                lock (lock_object)
                {
                    return _conString;
                }
            }
        }

        public bool IsConnected => connection != null && connection.IsConnected;

        public IDatabase CreateModel() => connection.GetDatabase();

        public ISubscriber GetSubscriber() => connection.GetSubscriber();

        public void Dispose()
        {
            _disposed = true;
            connection?.Dispose();
        }

        public bool TryConnect()
        {
            lock (lock_object)
            {
                var policy = Policy.Handle<SocketException>()
                    .Or<RedisConnectionException>()
                    .WaitAndRetry(RetryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                    {

                    }
                );

                policy.Execute(() =>
                {
                    connection = ConnectionMultiplexer.Connect(_conString);
                    connection.ConnectionFailed += Connection_ConnectionFailed;
                    connection.ConnectionRestored += Connection_ConnectionRestored;
                    connection.ErrorMessage += Connection_ErrorMessage;
                    connection.InternalError += Connection_InternalError;
                });

                if (IsConnected)
                {
                    connection.ConnectionFailed += Connection_ConnectionFailed1;
                    return true;
                }

                return false;
            }
        }

        private void Connection_ConnectionFailed1(object? sender, ConnectionFailedEventArgs e)
        {
            if (_disposed) return;

            TryConnect();
        }

        private void Connection_InternalError(object? sender, InternalErrorEventArgs e)
        {
            if (_disposed) return;

            TryConnect();
        }

        private void Connection_ErrorMessage(object? sender, RedisErrorEventArgs e)
        {
            if (_disposed) return;

            TryConnect();
        }

        private void Connection_ConnectionRestored(object? sender, ConnectionFailedEventArgs e)
        {
            if (_disposed) return;

            TryConnect();
        }

        private void Connection_ConnectionFailed(object? sender, ConnectionFailedEventArgs e)
        {
            if (_disposed) return;

            TryConnect();
        }
    }
}
