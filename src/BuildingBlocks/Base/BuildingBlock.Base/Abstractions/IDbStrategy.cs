using System.Data;

namespace BuildingBlock.Base.Abstractions
{
    public interface IDbStrategy
    {
        IDbConnection GetConnection();
    }
}
