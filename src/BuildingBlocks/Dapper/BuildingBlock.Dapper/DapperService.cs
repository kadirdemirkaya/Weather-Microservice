using AutoMapper;
using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Configs;
using BuildingBlock.Base.Models.Base;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Serilog;
using System.Data;

namespace BuildingBlock.Dapper
{
    public class DapperService<T, TId> : IDapperService<T, TId>
      where T : Entity<TId>
      where TId : ValueObject
    {
        private IDbConnection _dbConnection { get; set; }
        private DapperPersistenceConnection _persistenceConnection;
        private readonly IMapper _mapper;
        private readonly IServiceProvider _serviceProvider;

        public DapperService(DatabaseConfig databaseConfig, DbContext dbContext, IServiceProvider serviceProvider)
        {
            if (databaseConfig.ConnectionString != null)
            {
                var connJson = JsonConvert.SerializeObject(databaseConfig.ConnectionString, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                });
                _persistenceConnection = new(dbContext, connJson, 5);
                _dbConnection = _persistenceConnection.GetDapperConnection();
                _serviceProvider = serviceProvider;
                _mapper = GetMapperService();
            }
        }

        private IMapper GetMapperService()
            => _serviceProvider.GetRequiredService<IMapper>();

        public async Task<bool> ExecQuery(string query, DynamicParameters? dynamicParameters = null)
        {
            try
            {
                int result = 0;
                if (dynamicParameters is not null)
                {
                    result = await _dbConnection.ExecuteAsync(query, dynamicParameters);
                    return result > 0 ? true : false;
                }
                result = await _dbConnection.ExecuteAsync(query);
                return result > 0 ? true : false;
            }
            catch (System.Exception ex)
            {
                Log.Error("ERROR MESSAGE IN THE DAPPER: " + ex.Message);
                return false;
            }
        }

        public async Task<T> GetEntityStoredProcedure(string storedProcedure, DynamicParameters dynamicParameters)
        {
            _dbConnection.Open();
            try
            {
                var user = await _dbConnection.QuerySingleOrDefaultAsync<object>(storedProcedure, dynamicParameters, commandType: CommandType.StoredProcedure);
                if (user is null)
                    return default;
                return _mapper.Map<T>(user);
            }
            catch (Exception ex)
            {
                Log.Error("ERROR MESSAGE IN THE DAPPER: " + ex.Message);
                return null;
            }
            finally { _dbConnection.Close(); }
        }

        public async Task<List<T>> GetQueryAll(string query)
        {
            _dbConnection.Open();

            try
            {
                var data = await _dbConnection.QueryAsync<T>(query);
                return data.ToList();
            }
            catch (Exception ex)
            {
                Log.Error("ERROR MESSAGE IN THE DAPPER: " + ex.Message);
                return null;
            }
            finally { _dbConnection.Close(); }
        }

        public async Task<int> GetStoredProcedure(string storedProcedure, DynamicParameters dynamicParameters)
        {
            _dbConnection.Open();
            try
            {
                var results = await _dbConnection.ExecuteAsync(storedProcedure, dynamicParameters, commandType: CommandType.StoredProcedure);
                return results;
            }
            catch (Exception ex)
            {
                Log.Error("ERROR MESSAGE IN THE DAPPER: " + ex.Message);
                return 0;
            }
            finally { _dbConnection.Close(); }
        }
    }

    public class DapperService<T> : IDapperService<T>
        where T : class
    {
        private IDbConnection _dbConnection { get; set; }
        private DapperPersistenceConnection _persistenceConnection;

        public DapperService(DatabaseConfig databaseConfig, DbContext dbContext, IServiceProvider serviceProvider)
        {
            if (databaseConfig.ConnectionString != null)
            {
                var connJson = JsonConvert.SerializeObject(databaseConfig.ConnectionString, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                });
                _persistenceConnection = new(dbContext, connJson, 5);
                _dbConnection = _persistenceConnection.GetDapperConnection();
            }
        }

        public async Task<bool> ExecQuery(string query, DynamicParameters? dynamicParameters = null)
        {
            try
            {
                int result = 0;
                if (dynamicParameters is not null)
                {
                    result = await _dbConnection.ExecuteAsync(query, dynamicParameters);
                    return result > 0 ? true : false;
                }
                result = await _dbConnection.ExecuteAsync(query);
                return result > 0 ? true : false;
            }
            catch (System.Exception ex)
            {
                Log.Error("ERROR MESSAGE IN THE DAPPER: " + ex.Message);
                return false;
            }
        }

        public async Task<T> GetEntityStoredProcedure(string storedProcedure, DynamicParameters dynamicParameters)
        {
            _dbConnection.Open();
            try
            {
                var user = await _dbConnection.QuerySingleOrDefaultAsync<T>(storedProcedure, dynamicParameters, commandType: CommandType.StoredProcedure);
                return user;
            }
            catch (Exception ex)
            {
                Log.Error("ERROR MESSAGE IN THE DAPPER: " + ex.Message);
                return null;
            }
            finally { _dbConnection.Close(); }
        }

        public async Task<List<T>> GetQueryAll(string query)
        {
            _dbConnection.Open();

            try
            {
                var data = await _dbConnection.QueryAsync<T>(query);
                return data.ToList();
            }
            catch (Exception ex)
            {
                Log.Error("ERROR MESSAGE IN THE DAPPER: " + ex.Message);
                return null;
            }
            finally { _dbConnection.Close(); }
        }

        public async Task<int> GetStoredProcedure(string storedProcedure, DynamicParameters dynamicParameters)
        {
            _dbConnection.Open();
            try
            {
                var results = await _dbConnection.ExecuteAsync(storedProcedure, dynamicParameters, commandType: CommandType.StoredProcedure);
                return results;
            }
            catch (Exception ex)
            {
                Log.Error("ERROR MESSAGE IN THE DAPPER: " + ex.Message);
                return 0;
            }
            finally { _dbConnection.Close(); }
        }
    }
}
