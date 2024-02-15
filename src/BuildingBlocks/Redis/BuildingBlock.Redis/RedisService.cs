using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Configs;
using BuildingBlock.Base.Extensions;
using BuildingBlock.Base.Models.Base;
using BuildingBlock.Base.Options;
using Elastic.CommonSchema;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace BuildingBlock.Redis
{
    public class RedisService<T, TId> : IRedisService<T, TId>
           where T : Entity<TId>
           where TId : ValueObject
    {
        private RedisPersistentConnection persistentConnection;
        private IConnectionMultiplexer connectionFactory;
        private IDatabase _redisDb;
        private IServer _server;
        private bool _disposed;
        private string connectionUrl;
        private RedisConfig RedisConfig;
        private InMemoryOptions _inMemoryOptions;

        public RedisService(InMemoryOptions inMemoryOptions, IServiceProvider serviceProvider)
        {
            _inMemoryOptions = inMemoryOptions;
            if (_inMemoryOptions.Connection != null)
            {
                connectionUrl = inMemoryOptions.Connection.ToString();
                connectionFactory = ConnectionMultiplexer.Connect(connectionUrl);
            }
            persistentConnection = new RedisPersistentConnection(connectionFactory, _inMemoryOptions);
            _redisDb = persistentConnection.CreateModel();
            _server = persistentConnection.GetServer;
        }

        public IDatabase GetDatabase
        {
            get
            {
                lock (TypeLock<T>.Lock)
                {
                    if (_redisDb is null)
                        _redisDb = persistentConnection.CreateModel();
                    return _redisDb;
                }
            }
        }

        public bool IsConnected => persistentConnection != null && persistentConnection.IsConnected;

        public void AddConnection(RedisConfig RedisConfig)
        {
            if (RedisConfig.Connection != null)
            {
                var connJson = JsonConvert.SerializeObject(RedisConfig.Connection, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                });
                connectionUrl = JsonConvert.DeserializeObject<string>(connJson);
                connectionFactory = ConnectionMultiplexer.Connect(JsonConvert.DeserializeObject<string>(connJson));
            }
            persistentConnection = new RedisPersistentConnection(connectionFactory, RedisConfig, 5);
        }

        public void AddConnection(IConfiguration configuration, RedisConfig RedisConfig)
        {
            connectionFactory = ConnectionMultiplexer.Connect($"{configuration["RedisSetting:Host"]}.{configuration["RedisSetting:Port"]}");
            persistentConnection = new RedisPersistentConnection(connectionFactory, RedisConfig, 5);
        }

        public bool CheckHealth()
        {
            if (!persistentConnection.IsConnected)
                persistentConnection.TryConnect(); _redisDb = persistentConnection.CreateModel();
            lock (TypeLock<T>.Lock)
            {
                try
                {
                    var pingResult = _redisDb.Ping();
                    return pingResult.ToString() == "PONG";
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public bool Remove(string key)
        {
            if (!persistentConnection.IsConnected)
                persistentConnection.TryConnect();
            lock (TypeLock<T>.Lock)
            {
                try
                {
                    return _redisDb.KeyDelete(key);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }

        public void Remove(RedisKey[] keys)
        {
            if (!persistentConnection.IsConnected)
                persistentConnection.TryConnect();
            lock (TypeLock<T>.Lock)
            {
                try
                {
                    _redisDb.KeyDelete(keys);
                }
                catch (Exception ex)
                {
                    return;
                }
            }
        }

        public bool Exists(string key)
        {
            if (!persistentConnection.IsConnected)
                persistentConnection.TryConnect();
            return _redisDb.KeyExists(key);
        }

        public bool Add<T>(string key, T value, TimeSpan expiresAt) where T : class
        {
            if (!persistentConnection.IsConnected)
                persistentConnection.TryConnect();
            lock (TypeLock<T>.Lock)
            {
                try
                {
                    var stringContent = JsonExtension.SerialJson(value);
                    return _redisDb.StringSet(key, stringContent, expiresAt);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }

        public bool JoinT<T>(string key, T value, TimeSpan expiresAt) where T : class
        {
            if (!persistentConnection.IsConnected)
                persistentConnection.TryConnect();
            lock (TypeLock<T>.Lock)
            {
                try
                {
                    List<T> dataList;

                    string existingData = _redisDb.StringGet(key);
                    if (existingData == null)
                        dataList = new List<T>();
                    else
                        dataList = JsonConvert.DeserializeObject<List<T>>(existingData);
                    dataList.Add(value);
                    string updatedData = JsonConvert.SerializeObject(dataList);
                    _redisDb.StringSet(key, updatedData);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }

        public bool Add(string key, object value, TimeSpan expiresAt)
        {
            if (!persistentConnection.IsConnected)
                persistentConnection.TryConnect();
            lock (TypeLock<T>.Lock)
            {
                try
                {
                    var stringContent = JsonExtension.SerialJson(value);
                    return _redisDb.StringSet(key, stringContent, expiresAt);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }

        public bool AddObjcet(string key, object value, TimeSpan expiresAt)
        {
            if (!persistentConnection.IsConnected)
                persistentConnection.TryConnect();
            lock (TypeLock<T>.Lock)
            {
                try
                {
                    var stringContent = JsonExtension.SerialJson(value);
                    var datas = _redisDb.StringGet(key);
                    return _redisDb.StringSet(key, datas + stringContent, expiresAt);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }

        public bool Update<T>(string key, T value) where T : class
        {
            if (!persistentConnection.IsConnected)
                persistentConnection.TryConnect();
            lock (TypeLock<T>.Lock)
            {
                try
                {
                    var stringContent = JsonExtension.SerialJson(value);
                    return _redisDb.StringSet(key, stringContent);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public T Get<T>(string key) where T : class
        {
            if (!persistentConnection.IsConnected)
                persistentConnection.TryConnect();
            try
            {
                RedisValue myString = _redisDb.StringGet(key);
                if (myString.HasValue && !myString.IsNullOrEmpty)
                {
                    return JsonExtension.DeserializeJson<T>(myString);
                }

                return null;
            }
            catch (Exception)
            {
                // Log Exception
                return null;
            }
        }

        public List<T> GetList<T>(string key) where T : class
        {
            if (!persistentConnection.IsConnected)
                persistentConnection.TryConnect();
            try
            {
                //var server = connectionFactory.GetServer(RedisConfig.Connection);
                var server = connectionFactory.GetServer(connectionUrl);
                var keys = server.Keys(_redisDb.Database, key);
                var keyValues = _redisDb.StringGet(keys.ToArray());

                List<T> values = new List<T>();

                foreach (var redisValue in keyValues)
                {
                    if (redisValue.HasValue && !redisValue.IsNullOrEmpty)
                    {
                        List<T> itemList = JsonExtension.DeserializeJson<List<T>>(redisValue);
                        values.AddRange(itemList);
                    }
                }

                return values;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void Stop()
        {
            _disposed = true;
            connectionFactory.Dispose();
        }

        public bool UpdateList<T>(string key, List<T> value) where T : class
        {
            if (!persistentConnection.IsConnected)
                persistentConnection.TryConnect();
            lock (TypeLock<T>.Lock)
            {
                try
                {
                    var stringContents = JsonExtension.SerialJson(value);
                    return _redisDb.StringSet(key, stringContents);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public void AddConnection()
        {
            lock (TypeLock<T>.Lock)
            {
                _redisDb = GetDatabase;
            }
        }

        public int GetStringKeyCount()
        {
            var keys = _server.Keys(pattern: "*");
            int stringKeyCount = 0;
            foreach (var key in keys)
                if (_redisDb.KeyType(key) == RedisType.String)
                    stringKeyCount++;
            return stringKeyCount;
        }

        public bool DeleteAllKeys()
        {
            RedisResult? redisResult = _redisDb.Execute("FLUSHALL");
            return redisResult.ToString() == "OK" ? true : false;
        }

        public void DeleteKeys(string key)
        {
            if (!key.EndsWith("*"))
                key += "*";

            var cursor = 0;
            do
            {
                var scanResult = _redisDb.Execute("SCAN", cursor, "MATCH", key);
                cursor = (int)((RedisResult[])scanResult)[0];
                var keys = (RedisResult[])((RedisResult[])scanResult)[1];

                foreach (var redisKey in keys)
                    _redisDb.KeyDelete((string)redisKey);
            } while (cursor != 0);
        }

        private static class TypeLock<T>
        {
            public static object Lock { get; } = new object();
        }
    }
    public class RedisService<T> : IRedisService<T> where T : class
    {
        private RedisPersistentConnection persistentConnection;
        private IConnectionMultiplexer connectionFactory;
        private IDatabase _redisDb;
        private bool _disposed;
        private string connectionUrl;
        private RedisConfig RedisConfig;
        private InMemoryOptions _inMemoryOptions;

        public RedisService(InMemoryOptions inMemoryOptions, IServiceProvider serviceProvider)
        {
            _inMemoryOptions = inMemoryOptions;
            if (_inMemoryOptions.Connection != null)
            {
                var connJson = JsonConvert.SerializeObject(RedisConfig.Connection, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                });
                connectionFactory = ConnectionMultiplexer.Connect(JsonConvert.DeserializeObject<string>(connJson));
            }
            persistentConnection = new RedisPersistentConnection(connectionFactory, _inMemoryOptions);
            _redisDb = persistentConnection.CreateModel();
        }

        public IDatabase GetDatabase
        {
            get
            {
                lock (TypeLock<T>.Lock)
                {
                    if (_redisDb is null)
                        _redisDb = persistentConnection.CreateModel();
                    return _redisDb;
                }
            }
        }

        public bool IsConnected => persistentConnection != null && persistentConnection.IsConnected;

        public void AddConnection(RedisConfig RedisConfig)
        {
            if (RedisConfig.Connection != null)
            {
                var connJson = JsonConvert.SerializeObject(RedisConfig.Connection, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                });
                connectionUrl = JsonConvert.DeserializeObject<string>(connJson);
                connectionFactory = ConnectionMultiplexer.Connect(JsonConvert.DeserializeObject<string>(connJson));
            }
            persistentConnection = new RedisPersistentConnection(connectionFactory, RedisConfig, 5);
        }

        public void AddConnection(IConfiguration configuration, RedisConfig RedisConfig)
        {
            connectionFactory = ConnectionMultiplexer.Connect($"{configuration["RedisSetting:Host"]}.{configuration["RedisSetting:Port"]}");
            persistentConnection = new RedisPersistentConnection(connectionFactory, RedisConfig, 5);
        }

        public bool CheckHealth()
        {
            if (!persistentConnection.IsConnected)
                persistentConnection.TryConnect(); _redisDb = persistentConnection.CreateModel();
            lock (TypeLock<T>.Lock)
            {
                try
                {
                    var pingResult = _redisDb.Ping();
                    return pingResult.ToString() == "PONG";
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public bool Remove(string key)
        {
            if (!persistentConnection.IsConnected)
                persistentConnection.TryConnect();
            lock (TypeLock<T>.Lock)
            {
                try
                {
                    return _redisDb.KeyDelete(key);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }

        public void Remove(RedisKey[] keys)
        {
            if (!persistentConnection.IsConnected)
                persistentConnection.TryConnect();
            lock (TypeLock<T>.Lock)
            {
                try
                {
                    _redisDb.KeyDelete(keys);
                }
                catch (Exception ex)
                {
                    return;
                }
            }
        }

        public bool Exists(string key)
        {
            if (!persistentConnection.IsConnected)
                persistentConnection.TryConnect();
            return _redisDb.KeyExists(key);
        }

        public bool Add<T>(string key, T value, TimeSpan expiresAt) where T : class
        {
            if (!persistentConnection.IsConnected)
                persistentConnection.TryConnect();
            lock (TypeLock<T>.Lock)
            {
                try
                {
                    var stringContent = JsonExtension.SerialJson(value);
                    return _redisDb.StringSet(key, stringContent, expiresAt);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }

        public bool JoinT<T>(string key, T value, TimeSpan expiresAt) where T : class
        {
            if (!persistentConnection.IsConnected)
                persistentConnection.TryConnect();
            lock (TypeLock<T>.Lock)
            {
                try
                {
                    List<T> dataList;

                    string existingData = _redisDb.StringGet(key);
                    if (existingData == null)
                        dataList = new List<T>();
                    else
                        dataList = JsonConvert.DeserializeObject<List<T>>(existingData);
                    dataList.Add(value);
                    string updatedData = JsonConvert.SerializeObject(dataList);
                    _redisDb.StringSet(key, updatedData);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }

        public bool Add(string key, object value, TimeSpan expiresAt)
        {
            if (!persistentConnection.IsConnected)
                persistentConnection.TryConnect();
            lock (TypeLock<T>.Lock)
            {
                try
                {
                    var stringContent = JsonExtension.SerialJson(value);
                    return _redisDb.StringSet(key, stringContent, expiresAt);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }

        public bool AddObjcet(string key, object value, TimeSpan expiresAt)
        {
            if (!persistentConnection.IsConnected)
                persistentConnection.TryConnect();
            lock (TypeLock<T>.Lock)
            {
                try
                {
                    var stringContent = JsonExtension.SerialJson(value);
                    var datas = _redisDb.StringGet(key);
                    return _redisDb.StringSet(key, datas + stringContent, expiresAt);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }

        public bool Update<T>(string key, T value) where T : class
        {
            if (!persistentConnection.IsConnected)
                persistentConnection.TryConnect();
            lock (TypeLock<T>.Lock)
            {
                try
                {
                    var stringContent = JsonExtension.SerialJson(value);
                    return _redisDb.StringSet(key, stringContent);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public T Get<T>(string key) where T : class
        {
            if (!persistentConnection.IsConnected)
                persistentConnection.TryConnect();
            try
            {
                RedisValue myString = _redisDb.StringGet(key);
                if (myString.HasValue && !myString.IsNullOrEmpty)
                {
                    return JsonExtension.DeserializeJson<T>(myString);
                }

                return null;
            }
            catch (Exception)
            {
                // Log Exception
                return null;
            }
        }

        public List<T> GetList<T>(string key) where T : class
        {
            if (!persistentConnection.IsConnected)
                persistentConnection.TryConnect();
            try
            {
                //var server = connectionFactory.GetServer(RedisConfig.Connection);
                var server = connectionFactory.GetServer(connectionUrl);
                var keys = server.Keys(_redisDb.Database, key);
                var keyValues = _redisDb.StringGet(keys.ToArray());

                List<T> values = new List<T>();

                foreach (var redisValue in keyValues)
                {
                    if (redisValue.HasValue && !redisValue.IsNullOrEmpty)
                    {
                        List<T> itemList = JsonExtension.DeserializeJson<List<T>>(redisValue);
                        values.AddRange(itemList);
                    }
                }

                return values;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void Stop()
        {
            _disposed = true;
            connectionFactory.Dispose();
        }

        public bool UpdateList<T>(string key, List<T> value) where T : class
        {
            if (!persistentConnection.IsConnected)
                persistentConnection.TryConnect();
            lock (TypeLock<T>.Lock)
            {
                try
                {
                    var stringContents = JsonExtension.SerialJson(value);
                    return _redisDb.StringSet(key, stringContents);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public void AddConnection()
        {
            lock (TypeLock<T>.Lock)
            {
                _redisDb = GetDatabase;
            }
        }

        public int GetStringKeyCount()
        {
            throw new NotImplementedException();
        }

        public bool DeleteAllKeys()
        {
            throw new NotImplementedException();
        }

        public void DeleteKeys(string key)
        {
            throw new NotImplementedException();
        }

        private static class TypeLock<T>
        {
            public static object Lock { get; } = new object();
        }
    }
}
