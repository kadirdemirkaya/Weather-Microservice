using BuildingBlock.Base.Models.Base;

namespace BuildingBlock.Base.Abstractions
{
    public interface ICaching<T, TId>
          where T : Entity<TId>
          where TId : ValueObject
    {

    }
    public interface ICaching<T> where T : class
    {

    }
}
