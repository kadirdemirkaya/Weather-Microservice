using BuildingBlock.Base.Models.Base;

namespace BuildingBlock.Base.Abstractions
{
    public interface IMsSqlService<T, TId>
         where T : Entity<TId>
         where TId : ValueObject
    {
    }

    public interface IMsSqlService<T>
       where T : class
    {
    }
}
