using BuildingBlock.Base.Models.Base;
using Dapper;

namespace BuildingBlock.Base.Abstractions
{
    public interface IDapperService<T, TId>
          where T : Entity<TId>
          where TId : ValueObject
    {
        Task<List<T>> GetQueryAll(string query);
        Task<bool> ExecQuery(string query, DynamicParameters? dynamicParameters = null);
        Task<int> GetStoredProcedure(string storedProcedure, DynamicParameters dynamicParameters);
        Task<T> GetEntityStoredProcedure(string storedProcedure, DynamicParameters dynamicParameters);
    }

    public interface IDapperService<T> where T : class
    {
        Task<List<T>> GetQueryAll(string query);
        Task<bool> ExecQuery(string query, DynamicParameters? dynamicParameters = null);
        Task<int> GetStoredProcedure(string storedProcedure, DynamicParameters dynamicParameters);
        Task<T> GetEntityStoredProcedure(string storedProcedure, DynamicParameters dynamicParameters);
    }
}
