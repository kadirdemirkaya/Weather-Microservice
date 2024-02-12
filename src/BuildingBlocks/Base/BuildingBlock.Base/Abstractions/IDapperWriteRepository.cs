using BuildingBlock.Base.Models.Base;

namespace BuildingBlock.Base.Abstractions
{
    public interface IDapperWriteRepository<T,TId>
        where T : Entity<TId>
        where TId : ValueObject
    {
    }

    public interface IDapperWriteRepository<T>
        where T : class
    {
    }
}
