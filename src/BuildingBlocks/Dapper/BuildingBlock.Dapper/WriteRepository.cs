using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Configs;
using BuildingBlock.Base.Exceptions;
using BuildingBlock.Base.Models.Base;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using Serilog;

namespace BuildingBlock.Dapper
{
    public class WriteRepository<T, TId> : IWriteRepository<T, TId>, IDapperWriteRepository<T, TId>
        where T : Entity<TId>
        where TId : ValueObject
    {
        private IDbConnection _dbConnection { get; set; }
        private DapperPersistenceConnection _persistenceConnection;

        public WriteRepository(DatabaseConfig databaseConfig, DbContext dbContext)
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

        private string GetRepoName()
            => typeof(WriteRepository<,>).Name;

        private string GetTableName()
            => typeof(T).Name;

        public async Task<bool> CreateAsync(T entity)
        {
            try
            {
                int result = await _dbConnection.InsertAsync<T>(entity);
                return result > 0 ? true : false;
            }
            catch (Exception ex)
            {
                Log.Warning("Dapper Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
        }

        public bool Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            _dbConnection.Open();

            try
            {
                string tableName = typeof(T).Name;
                var entity = (await _dbConnection.QueryAsync<T>($"SELECT * FROM {tableName}s WHERE Id = @Id", new { Id = id })).FirstOrDefault();

                if (entity == null)
                    return false;

                var keyProperty = entity.GetType().GetProperty("Id");
                var keyValue = keyProperty.GetValue(entity);
                var affectedRows = await _dbConnection.ExecuteAsync($"DELETE FROM {tableName}s WHERE Id = @Id", new { Id = keyValue });
                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                Log.Warning("Dapper Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
            finally { _dbConnection.Close(); }
        }

        public bool UpdateAsync(T entity)
        {
            _dbConnection.Open();

            try
            {
                return _dbConnection.Update(entity);
            }
            catch (Exception ex)
            {
                Log.Warning("Dapper Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
            finally { _dbConnection.Close(); }
        }

        public async Task<bool> DeleteByIdAsync(T entityId)
        {
            _dbConnection.Open();

            try
            {
                string tableName = typeof(T).Name;
                var entity = (await _dbConnection.QueryAsync<T>($"SELECT * FROM {tableName}s WHERE Id = @Id", new { Id = entityId.Id })).FirstOrDefault();

                if (entity == null)
                    return false;

                var keyProperty = entity.GetType().GetProperty("Id");
                var keyValue = keyProperty.GetValue(entity);
                var affectedRows = await _dbConnection.ExecuteAsync($"DELETE FROM {tableName}s WHERE Id = @Id", new { Id = keyValue });
                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                Log.Warning("Dapper Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
            finally { _dbConnection.Close(); }
        }
    }

    public class WriteRepository<T> : IWriteRepository<T>, IDapperWriteRepository<T>
       where T : class
    {
        private IDbConnection _dbConnection { get; set; }
        private DapperPersistenceConnection _persistenceConnection;

        public WriteRepository(DatabaseConfig databaseConfig, DbContext dbContext)
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

        private string GetRepoName()
          => typeof(WriteRepository<>).Name;

        private string GetTableName()
            => typeof(T).Name;

        public async Task<bool> CreateAsync(T entity)
        {
            try
            {
                int result = await _dbConnection.InsertAsync<T>(entity);
                return result > 0 ? true : false;
            }
            catch (Exception ex)
            {
                Log.Warning("Dapper Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
            finally { _dbConnection.Close(); }
        }
        public async Task<bool> DeleteByIdAsync(string id)
        {
            _dbConnection.Open();

            try
            {
                string tableName = typeof(T).Name;
                var entity = (await _dbConnection.QueryAsync<T>($"SELECT * FROM {tableName}s WHERE Id = @Id", new { Id = id })).FirstOrDefault();

                if (entity == null)
                    return false;

                var keyProperty = entity.GetType().GetProperty("Id");
                var keyValue = keyProperty.GetValue(entity);
                var affectedRows = await _dbConnection.ExecuteAsync($"DELETE FROM {tableName}s WHERE Id = @Id", new { Id = keyValue });
                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                Log.Warning("Dapper Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
            finally { _dbConnection.Close(); }
        }

        public bool UpdateAsync(T entity)
        {
            _dbConnection.Open();

            try
            {
                return _dbConnection.Update(entity);
            }
            catch (Exception ex)
            {
                Log.Warning("Dapper Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
            finally { _dbConnection.Close(); }
        }

        public bool Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdAsync(T entityId)
        {
            throw new NotImplementedException();
        }
    }
}
