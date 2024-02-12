using BuildingBlock.Base.Models.Base;
using MongoDB.Bson;
using System.Linq.Expressions;

namespace BuildingBlock.Base.Abstractions
{
    public interface IReadRepository<T, TId>
      where T : Entity<TId>
      where TId : ValueObject
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, bool tracking = true, params Expression<Func<T, object>>[] includeEntity);
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(Expression<Func<T, bool>> expression = null, bool tracking = true, params Expression<Func<T, object>>[] includeEntity);
        Task<T> GetByGuidAsync(string id, bool tracking = true);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression, bool tracking = true);
        Task<int> CountAsync(Expression<Func<T, bool>> expression = null, bool tracking = true);
        Task<bool> AnyAsync();
        Task<int> CountAsync();
    }



    public interface IReadRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, bool? tracking = true, params Expression<Func<T, object>>[]? includeEntity);
        Task<T> GetAsync(Expression<Func<T, bool>> expression = null, bool? tracking = true, params Expression<Func<T, object>>[]? includeEntity);
        Task<T> GetByGuidAsync(string id, bool? tracking = true);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression, bool? tracking = true);
        Task<int> CountAsync(Expression<Func<T, bool>> expression = null, bool? tracking = true);
        Task<List<T>> GetAllAsync();
        Task<bool> AnyAsync();
        Task<int> CountAsync();
    }
}
