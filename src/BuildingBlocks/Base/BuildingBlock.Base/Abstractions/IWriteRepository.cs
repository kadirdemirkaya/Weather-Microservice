using BuildingBlock.Base.Models.Base;

namespace BuildingBlock.Base.Abstractions
{
    public interface IWriteRepository<T, TId>
       where T : Entity<TId>
       where TId : ValueObject
    {
        public Task<bool> CreateAsync(T entity);
        public bool Delete(T entity);
        public Task<bool> DeleteByIdAsync(T entityId);
        public Task<bool> DeleteByIdAsync(string entityId);
        public bool UpdateAsync(T entity);
    }



    public interface IWriteRepository<T> where T : class
    {
        public Task<bool> CreateAsync(T entity);
        public bool UpdateAsync(T entity);
        public bool Delete(T entity);
        public Task<bool> DeleteByIdAsync(string entityId);
    }
}
