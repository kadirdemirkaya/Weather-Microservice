using BuildingBlock.Base.Configs;
using BuildingBlock.Base.Models.Base;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace BuildingBlock.Base.Abstractions
{
    public interface IRedisService<T, TId> : ICaching<T, TId>
         where T : Entity<TId>
         where TId : ValueObject
    {
        void AddConnection();
        void AddConnection(IConfiguration configuration, RedisConfig redisConfig);
        bool CheckHealth();
        bool Remove(string key);
        void Remove(RedisKey[] keys);
        bool Exists(string key);
        void Stop();
        bool Add(string key, object value, TimeSpan expiresAt);
        bool Add<T>(string key, T value, TimeSpan expiresAt) where T : class;
        bool AddObjcet(string key, object value, TimeSpan expiresAt);

        public bool JoinT<T>(string key, T value, TimeSpan expiresAt) where T : class;
        bool Update<T>(string key, T value) where T : class;
        bool UpdateList<T>(string key, List<T> value) where T : class;

        T Get<T>(string key) where T : class;
        List<T> GetList<T>(string key) where T : class;

    }
    public interface IRedisService<T> : ICaching<T> where T : class
    {
        void AddConnection();
        void AddConnection(IConfiguration configuration, RedisConfig redisConfig);
        bool CheckHealth();
        bool Remove(string key);
        void Remove(RedisKey[] keys);
        bool Exists(string key);
        void Stop();
        bool Add(string key, object value, TimeSpan expiresAt);
        bool Add<T>(string key, T value, TimeSpan expiresAt) where T : class;
        bool AddObjcet(string key, object value, TimeSpan expiresAt);

        public bool JoinT<T>(string key, T value, TimeSpan expiresAt) where T : class;
        bool Update<T>(string key, T value) where T : class;
        bool UpdateList<T>(string key, List<T> value) where T : class;

        T Get<T>(string key) where T : class;
        List<T> GetList<T>(string key) where T : class;

    }
}
