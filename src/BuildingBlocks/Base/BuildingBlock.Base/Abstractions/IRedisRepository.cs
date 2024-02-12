using BuildingBlock.Base.Enums;
using BuildingBlock.Base.Models.Base;

namespace BuildingBlock.Base.Abstractions
{
    public interface IRedisRepository<T, TId>
        where T : Entity<TId>
        where TId : ValueObject
    {
        Task<bool> CreateAsync(string key, T value, RedisDataType dataType);
        bool Create(string key, T value, RedisDataType dataType);
        Task<T>? GetByIdAsync(string key, string? id, RedisDataType dataType);
        T? GetById(string key, string? id, RedisDataType dataType);
        IEnumerable<T?>? GetAll(string? key, RedisDataType dataType);
        bool Delete(string key, string? id, RedisDataType dataType);
        bool Update(string key, T value, RedisDataType dataType);
    }
    public interface IRedisRepository<T>
         where T : class
    {
        Task<bool> CreateAsync(string key, T value, RedisDataType dataType);
        bool Create(string key, T value, RedisDataType dataType);
        Task<T>? GetByIdAsync(string key, string? id, RedisDataType dataType);
        T? GetById(string key, string? id, RedisDataType dataType);
        IEnumerable<T?>? GetAll(string? key, RedisDataType dataType);
        bool Delete(string key, string? id, RedisDataType dataType);
        bool Update(string key, T value, RedisDataType dataType);
    }
}
