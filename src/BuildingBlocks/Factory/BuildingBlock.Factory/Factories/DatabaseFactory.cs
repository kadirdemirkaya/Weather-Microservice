using BuildingBlock.Base.Models.Base;

namespace BuildingBlock.Factory.Factories
{
    public static class DatabaseFactory<T, TId>
         where T : Entity<TId>
         where TId : ValueObject
    {
        
    }

    public static class DatabaseFactory<T>
         where T : class
    {

    }
}
