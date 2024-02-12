using BuildingBlock.Base.Models.Base;
using System.Linq.Expressions;

namespace BuildingBlock.Base.Abstractions
{
    public interface IDapperReadRepository<T, TId>
        where T : Entity<TId>
        where TId : ValueObject
    {
        Task<T> GetByGuidAsync(TId id);

        Task<T> GetAsync(Expression<Func<T, bool>> expression);

        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression);

    }

    public interface IDapperReadRepository<T>
       where T : class
    {
        Task<T> GetByGuidAsync(T id);

        Task<T> GetAsync(Expression<Func<T, bool>> expression);

        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression);

    }
}
