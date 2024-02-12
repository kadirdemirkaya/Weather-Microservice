using BuildingBlock.Base.Models.Base;

namespace BuildingBlock.Base.Abstractions
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();

        Task<bool> PublishEventAsync();

        IReadRepository<T> GetReadRepository<T>() where T : class, new();
        IWriteRepository<T> GetWriteRepository<T>() where T : class, new();

        IReadRepository<T, TId> GetReadRepository<T, TId>() where T : Entity<TId>, new() where TId : ValueObject;
        IWriteRepository<T, TId> GetWriteRepository<T, TId>() where T : Entity<TId>, new() where TId : ValueObject;
    }
}
