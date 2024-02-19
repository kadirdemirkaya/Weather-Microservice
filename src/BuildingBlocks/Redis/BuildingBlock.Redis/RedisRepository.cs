using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Configs;
using BuildingBlock.Base.Enums;
using BuildingBlock.Base.Extensions;
using BuildingBlock.Base.Models.Base;
using BuildingBlock.Base.Options;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace BuildingBlock.Redis
{
    public class RedisRepository<T, TId> : IRedisRepository<T, TId>
         where T : Entity<TId>
         where TId : ValueObject
    {
        private string _connectionString;
        private RedisPersistentConnection persistentConnection;
        private readonly IConnectionMultiplexer _redis;
        private IDatabase _redisDb;
        private bool _disposed;
        private string connectionUrl;
        private RedisConfig RedisConfig;
        private InMemoryOptions inMemoryOptions;

        public RedisRepository(RedisConfig redisConfig, IServiceProvider serviceProvider)
        {
            if (redisConfig.Connection != null)
                _redis = ConnectionMultiplexer.Connect(redisConfig.Connection.ToString());
            persistentConnection = new RedisPersistentConnection(_redis, redisConfig);
            _redisDb = persistentConnection.CreateModel();
        }

        public RedisRepository(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            inMemoryOptions = configuration.GetOptions<InMemoryOptions>(nameof(InMemoryConfig));

            if (inMemoryOptions.Connection != null)
            {
                var connJson = JsonConvert.SerializeObject(inMemoryOptions.Connection, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                });
                _redis = ConnectionMultiplexer.Connect(Newtonsoft.Json.JsonConvert.DeserializeObject<string>(connJson));
            }
            persistentConnection = new RedisPersistentConnection(_redis, inMemoryOptions);
            _redisDb = persistentConnection.CreateModel();
        }

        public async Task<bool> CreateAsync(string key, T value, RedisDataType dataType)
        {
            var db = _redis.GetDatabase();
            string serialValue = System.Text.Json.JsonSerializer.Serialize(value);
            try
            {
                switch (dataType)
                {
                    case RedisDataType.String:
                        bool sResult = await db.SetAddAsync(key, serialValue);
                        return sResult;
                    case RedisDataType.Hash:
                        await db.HashSetAsync($"{key}", new HashEntry[] { new HashEntry(value.Id.ToString(), serialValue) });
                        break;
                    case RedisDataType.Sets:
                        bool setResult = await db.SetAddAsync(key, value.Id.ToString());
                        bool stringResult = await db.StringSetAsync(value.Id.ToString(), serialValue);
                        return setResult = stringResult = true ? true : false;
                    case RedisDataType.Lists:
                        await db.ListLeftPushAsync(key, value.Id.ToString());
                        bool stringResult2 = await db.StringSetAsync(value.Id.ToString(), serialValue);
                        return setResult = stringResult2 = true ? true : false;
                    case RedisDataType.OnlyLists:
                        long onlyListResult = await db.ListRightPushAsync(key, serialValue);
                        return onlyListResult > 0 ? true : false;
                    case RedisDataType.OnlySets:
                        bool onlySetResult = await db.SetAddAsync(key, value.Id.ToString());
                        return setResult = onlySetResult = true ? true : false;
                    default:
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("ERROR MESSAGE IN THE REDISCRUDCOMMON : " + ex.Message);
                return false;
                throw;
            }
        }

        public bool Create(string key, T value, RedisDataType dataType)
        {
            var db = _redis.GetDatabase();
            string serialValue = System.Text.Json.JsonSerializer.Serialize(value);
            try
            {
                switch (dataType)
                {
                    case RedisDataType.String:
                        bool sResult = db.SetAdd(key, serialValue);
                        return sResult;
                    case RedisDataType.Hash:
                        db.HashSet($"{key}", new HashEntry[] { new HashEntry(value.Id.ToString(), serialValue) });
                        break;
                    case RedisDataType.Sets:
                        bool setResult = db.SetAdd(key, value.Id.ToString());
                        bool stringResult = db.StringSet(value.Id.ToString(), serialValue);
                        return setResult = stringResult = true ? true : false;
                    case RedisDataType.Lists:
                        db.ListLeftPush(key, value.Id.ToString());
                        bool stringResult2 = db.StringSet(value.Id.ToString(), serialValue);
                        return setResult = stringResult2 = true ? true : false;
                    case RedisDataType.OnlyLists:
                        long onlyListResult = db.ListRightPush(key, serialValue);
                        return onlyListResult > 0 ? true : false;
                    case RedisDataType.OnlySets:
                        bool onlySetResult = db.SetAdd(key, value.Id.ToString());
                        return setResult = onlySetResult = true ? true : false;
                    default:
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("ERROR MESSAGE IN THE REDISCRUDCOMMON : " + ex.Message);
                return false;
                throw;
            }
        }

        public bool Delete(string key, string? id, RedisDataType dataType)
        {
            var db = _redis.GetDatabase();
            try
            {
                switch (dataType)
                {
                    case RedisDataType.String:
                        bool sResult = db.KeyDelete(key);
                        return sResult is true ? true : false;
                    case RedisDataType.Hash:
                        bool hResult = db.HashDelete(key, id);
                        return hResult is true ? true : false;
                    case RedisDataType.Sets:
                        bool setResult = db.SetRemove(key, id);
                        bool stringResult = db.KeyDelete(id);
                        return setResult = stringResult = true ? true : false;
                    case RedisDataType.Lists:
                        long removedCount = db.ListRemove(key, id);
                        bool stringResult2 = false;
                        if (removedCount > 0)
                            stringResult2 = db.KeyDelete(id);
                        return stringResult2 is true ? true : false;
                    case RedisDataType.OnlyLists:
                        bool deleteList = db.KeyDelete(key);
                        return deleteList;
                    case RedisDataType.OnlySets:
                        bool onlySetResult = db.SetRemove(key, id);
                        return setResult = onlySetResult = true ? true : false;
                    default:
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("ERROR MESSAGE IN THE REDISCRUDCOMMON : " + ex.Message);
                return false;
                throw;
            }
        }

        public IEnumerable<T?>? GetAll(string? key, RedisDataType dataType)
        {
            var db = _redis.GetDatabase();

            try
            {
                switch (dataType)
                {
                    case RedisDataType.String:
                        RedisValue[] sValue = db.SetMembers(key);
                        return Array.ConvertAll(sValue, val => System.Text.Json.JsonSerializer.Deserialize<T>(val)).ToList();
                    case RedisDataType.Hash:
                        HashEntry[] hValue = db.HashGetAll(key);
                        return Array.ConvertAll(hValue, val => System.Text.Json.JsonSerializer.Deserialize<T>(val.Value)).ToList();
                    case RedisDataType.Lists:
                        List<T>? entityListValues = new();
                        RedisValue[] ids = db.ListRange(key);
                        foreach (var id in ids)
                        {
                            var serializeListValue = db.StringGet(id.ToString());
                            if (!serializeListValue.IsNullOrEmpty)
                            {
                                var deserializeEntity = System.Text.Json.JsonSerializer.Deserialize<T>(serializeListValue);
                                entityListValues.Add(deserializeEntity);
                            }
                        }
                        return entityListValues;
                    case RedisDataType.Sets:
                        List<T>? entitySetValues = new List<T>();
                        RedisValue[] _ids = db.SetMembers(key);
                        foreach (var id in _ids)
                        {
                            var serializeSetValue = db.StringGet(id.ToString());
                            if (!serializeSetValue.IsNullOrEmpty)
                            {
                                var entity = System.Text.Json.JsonSerializer.Deserialize<T>(serializeSetValue);
                                entitySetValues.Add(entity);
                            }
                        }
                        return entitySetValues;
                    case RedisDataType.OnlyLists:
                        List<T?>? entityOnlyListValues = new();
                        if (key is null || string.IsNullOrEmpty(key))
                            return null;
                        RedisValue[]? listValues = db.ListRange(key);
                        if (!listValues.Any() || listValues is null || listValues.Count() <= 0)
                            return null;
                        entityOnlyListValues = listValues.Select(value => System.Text.Json.JsonSerializer.Deserialize<T>(value)).ToList();
                        return entityOnlyListValues;
                    case RedisDataType.OnlySets:
                        List<T>? entityOnlySetValues = new List<T>();
                        RedisValue[] setValues = db.SetMembers(key);
                        if (setValues is null || !setValues.Any())
                            return null;
                        entityOnlySetValues = setValues.Select(value => System.Text.Json.JsonSerializer.Deserialize<T>(value)).ToList()!;
                        return entityOnlySetValues;
                    default:
                        break;
                }
                return null;
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("ERROR MESSAGE IN THE REDISCRUDCOMMON : " + ex.Message);
                return null;
                throw;
            }
        }

        public T? GetById(string key, string? id, RedisDataType dataType)
        {
            var db = _redis.GetDatabase();

            try
            {
                switch (dataType)
                {
                    case RedisDataType.String:
                        RedisValue[] sValue = db.SetMembers(key);
                        if (sValue is not null)
                        {
                            var array = Array.ConvertAll(sValue, val => System.Text.Json.JsonSerializer.Deserialize<T>(val));
                            return array[0];
                        }
                        return default(T);
                    case RedisDataType.Hash:
                        RedisValue? hashValue = db.HashGet(key, id);
                        if (hashValue.HasValue)
                        {
                            var hDeserializeValue = System.Text.Json.JsonSerializer.Deserialize<T>(hashValue.Value);
                            return hDeserializeValue;
                        }
                        return default(T);
                    case RedisDataType.Sets:
                        RedisValue? stringSetValue = db.StringGet(key);
                        if (stringSetValue.HasValue)
                        {
                            var sSetDeserializeValue = System.Text.Json.JsonSerializer.Deserialize<T>(stringSetValue);
                            return sSetDeserializeValue;
                        }
                        return default(T);
                    case RedisDataType.Lists:
                        RedisValue? stringListValue = db.StringGet(key);
                        if (stringListValue.HasValue)
                        {
                            var sListDeserializeValue = System.Text.Json.JsonSerializer.Deserialize<T>(stringListValue);
                            return sListDeserializeValue;
                        }
                        return default(T);
                    default:
                        break;
                }
                return default;
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("ERROR MESSAGE IN THE REDISCRUDCOMMON : " + ex.Message);
                return default;
                throw;
            }
        }

        public bool Update(string key, T value, RedisDataType dataType)
        {
            var db = _redis.GetDatabase();
            string serialValue = System.Text.Json.JsonSerializer.Serialize(value);
            try
            {
                switch (dataType)
                {
                    case RedisDataType.String:
                        bool sResult1 = db.KeyDelete(key);
                        bool sResult2 = db.SetAdd(key, serialValue);
                        return sResult1 = sResult2 = true ? true : false;
                    case RedisDataType.Hash:
                        bool hResult = db.HashDelete(key, value.Id.ToString());
                        db.HashSet($"{key}", new HashEntry[] { new HashEntry(value.Id.ToString(), serialValue) });
                        return hResult is true ? true : false;
                    case RedisDataType.Sets:
                        bool setDeleteResult = db.SetRemove(key, value.Id.ToString());
                        bool stringDeleteResult = db.KeyDelete(key);
                        bool setCreateResult = db.SetAdd(key, value.Id.ToString());
                        bool stringCreateResult = db.StringSet(value.Id.ToString(), serialValue);
                        return setDeleteResult = stringDeleteResult = setCreateResult = stringCreateResult = true ? true : false;
                    case RedisDataType.Lists:
                        long removedCount = db.ListRemove(key, value.Id.ToString());
                        bool stringDeleteResult2 = false;
                        if (removedCount > 0)
                            stringDeleteResult2 = db.KeyDelete(value.Id.ToString());
                        db.ListLeftPush(key, value.Id.ToString());
                        bool stringCreateResult2 = db.StringSet(value.Id.ToString(), serialValue);
                        return stringDeleteResult2 = stringCreateResult2 = true ? true : false;
                    default:
                        break;
                }
                return true;
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("ERROR MESSAGE IN THE REDISCRUDCOMMON : " + ex.Message);
                return false;
                throw;
            }
        }

        public async Task<T>? GetByIdAsync(string key, string? id, RedisDataType dataType)
        {
            var db = _redis.GetDatabase();

            try
            {
                switch (dataType)
                {
                    case RedisDataType.String:
                        RedisValue[] sValue = await db.SetMembersAsync(key);
                        if (sValue is not null)
                        {
                            var array = Array.ConvertAll(sValue, val => System.Text.Json.JsonSerializer.Deserialize<T>(val));
                            return array[0];
                        }
                        return default(T);
                    case RedisDataType.Hash:
                        RedisValue? hashValue = await db.HashGetAsync(key, id);
                        if (hashValue.HasValue)
                        {
                            var hDeserializeValue = System.Text.Json.JsonSerializer.Deserialize<T>(hashValue.Value);
                            return hDeserializeValue;
                        }
                        return default(T);
                    case RedisDataType.HashAsync:

                    case RedisDataType.Sets:
                        RedisValue? stringSetValue = await db.StringGetAsync(key);
                        if (stringSetValue.HasValue)
                        {
                            var sSetDeserializeValue = System.Text.Json.JsonSerializer.Deserialize<T>(stringSetValue);
                            return sSetDeserializeValue;
                        }
                        return default(T);
                    case RedisDataType.Lists:
                        RedisValue? stringListValue = await db.StringGetAsync(key);
                        if (stringListValue.HasValue)
                        {
                            var sListDeserializeValue = System.Text.Json.JsonSerializer.Deserialize<T>(stringListValue);
                            return sListDeserializeValue;
                        }
                        return default(T);
                    default:
                        break;
                }
                return default;
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("ERROR MESSAGE IN THE REDISCRUDCOMMON : " + ex.Message);
                return default;
            }
        }

        public long Count(string key, RedisDataType dataType)
        {
            var db = _redis.GetDatabase();

            try
            {
                switch (dataType)
                {
                    case RedisDataType.Sets:
                        long lengthSet = db.SetLength(key);
                        return lengthSet;
                    case RedisDataType.Lists:
                        long lengthList = db.ListLength(key);
                        return lengthList;
                }
                return default;
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("ERROR MESSAGE IN THE REDISCRUDCOMMON : " + ex.Message);
                return default;
            }
        }
    }
}
