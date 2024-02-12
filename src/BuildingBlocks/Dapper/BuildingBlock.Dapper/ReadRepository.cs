using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Configs;
using BuildingBlock.Base.Exceptions;
using BuildingBlock.Base.Models.Base;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Linq.Expressions;
using Serilog;

namespace BuildingBlock.Dapper
{
    public class ReadRepository<T, TId> : IReadRepository<T, TId> , IDapperReadRepository<T,TId>
            where T : Entity<TId>
            where TId : ValueObject
    {
        private IDbConnection _dbConnection { get; set; }
        private DapperPersistenceConnection _persistenceConnection;

        public ReadRepository(DatabaseConfig databaseConfig, DbContext dbContext)
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
            => typeof(ReadRepository<,>).Name;

        private string GetTableName()
            => typeof(T).Name;

        public async Task<bool> AnyAsync()
        {
            try
            {
                return _dbConnection.ExecuteScalar<bool>($"IF EXISTS (SELECT 1 FROM {GetTableName()}) SELECT 1 ELSE SELECT 0");
            }
            catch (Exception ex)
            {
                Log.Warning("Dapper Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
            finally { _dbConnection.Close(); }
        }

        public async Task<int> CountAsync()
        {
            _dbConnection.Open();

            try
            {
                return _dbConnection.ExecuteScalar<int>($"SELECT COUNT(*) FROM {GetTableName()}");
            }
            catch (Exception ex)
            {
                Log.Warning("Dapper Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
            finally { _dbConnection.Close(); }
        }

        public async Task<List<T>> GetAllAsync()
        {
            _dbConnection.Open();

            try
            {
                var entity = await _dbConnection.QueryAsync<T>($"SELECT * FROM {GetTableName()}s");

                return entity.ToList();
            }
            catch (Exception ex)
            {
                Log.Warning("Dapper Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
            finally { _dbConnection.Close(); }
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        {
            _dbConnection.Open();

            try
            {
                var data = await _dbConnection.GetAllAsync<T>();
                var results = data.AsQueryable().Where(expression).ToList();
                return results;
            }
            catch (Exception ex)
            {
                Log.Warning("Dapper Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
            finally { _dbConnection.Close(); }
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            _dbConnection.Open();
            try
            {
                var data = await _dbConnection.GetAllAsync<T>();
                var results = data.AsQueryable().SingleOrDefault(expression);
                return results;
            }
            catch (Exception ex)
            {
                Log.Warning("Dapper Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
            finally { _dbConnection.Close(); }
        }

        public async Task<T> GetByGuidAsync(TId id)
        {
            _dbConnection.Open();

            try
            {
                var entity = (await _dbConnection.QueryAsync<T>($"SELECT * FROM {GetTableName()}s WHERE Id = @Id", new { Id = id })).FirstOrDefault();
                return entity;
            }
            catch (Exception ex)
            {
                Log.Warning("Dapper Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
            finally { _dbConnection.Close(); }
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, bool tracking = true, params Expression<Func<T, object>>[] includeEntity)
        {
            try
            {
                IEnumerable<T> result;

                if (expression == null)
                {
                    result = await _dbConnection.GetAllAsync<T>();
                }
                else
                {
                    result = await _dbConnection.QueryAsync<T>($"SELECT * FROM YourTableName WHERE {GetSqlWhere(expression)}", expression);
                }

                return result.ToList();
            }
            catch (Exception ex)
            {
                Log.Warning("Dapper Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
            finally { _dbConnection.Close(); }
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression = null, bool tracking = true, params Expression<Func<T, object>>[] includeEntity)
        {
            _dbConnection.Open();

            try
            {
                T? result = await _dbConnection.QueryFirstOrDefaultAsync<T>($"SELECT * FROM {GetTableName()} WHERE {GetSqlWhere(expression)}", expression);
                return result;
            }
            catch (Exception ex)
            {
                Log.Warning("Dapper Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
            finally { _dbConnection.Close(); }
        }

        public async Task<T> GetByGuidAsync(string id, bool tracking = true)
        {
            _dbConnection.Open();

            try
            {
                if (tracking)
                    return await _dbConnection.GetAsync<T>(id);
                else
                    return (await _dbConnection.QueryAsync<T>($"SELECT * FROM {GetTableName()}s WHERE Id = @Id", new { Id = Guid.Parse(id) })).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Log.Warning("Dapper Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
            finally { _dbConnection.Close(); }
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression, bool tracking = true)
        {
            _dbConnection.Open();

            try
            {
                bool result;

                if (tracking)
                {
                    result = _dbConnection.QueryFirstOrDefault<bool>($"SELECT TOP 1 1 FROM {GetTableName()}");
                }
                else
                {
                    result = (await _dbConnection.QueryFirstOrDefaultAsync<int>($"SELECT COUNT(*) FROM YourTableName WHERE {GetSqlWhere(expression)}", expression)) > 0;
                }

                return result;
            }
            catch (Exception ex)
            {
                Log.Warning("Dapper Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
            finally { _dbConnection.Close(); }
        }

        #region This Place Got Error
        public async Task<int> CountAsync(Expression<Func<T, bool>> expression = null, bool tracking = true)
        {
            _dbConnection.Open();

            try
            {
                int result;

                if (tracking)
                {
                    //result = await _dbConnection.CountAsync(expression);
                }
                else
                {
                    result = await _dbConnection.QueryFirstOrDefaultAsync<int>($"SELECT COUNT(*) FROM {GetTableName()} WHERE {GetSqlWhere(expression)}", expression);
                }
                return default;
            }
            catch (Exception ex)
            {
                Log.Warning("Dapper Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
            finally { _dbConnection.Close(); }
        }
        #endregion

        private string GetSqlWhere(Expression<Func<T, bool>> expression)
        {
            var binaryExpression = expression.Body as BinaryExpression;

            var left = binaryExpression?.Left.ToString();
            var right = binaryExpression?.Right.ToString();
            var binaryOperator = binaryExpression?.NodeType.ToString();

            return $"{left} {binaryOperator} {right}";
        }
    }



    public class ReadRepository<T> : IReadRepository<T>, IDapperReadRepository<T>
          where T : class
    {
        private IDbConnection _dbConnection { get; set; }
        private DapperPersistenceConnection _persistenceConnection;

        public ReadRepository(DatabaseConfig databaseConfig, DbContext dbContext)
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
            => typeof(ReadRepository<>).Name;

        private string GetTableName()
            => typeof(T).Name;

        public async Task<bool> AnyAsync()
        {
            try
            {
                return _dbConnection.ExecuteScalar<bool>($"IF EXISTS (SELECT 1 FROM {GetTableName()}) SELECT 1 ELSE SELECT 0");
            }
            catch (Exception ex)
            {
                Log.Warning("Dapper Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
            finally { _dbConnection.Close(); }
        }

        public async Task<int> CountAsync()
        {
            _dbConnection.Open();

            try
            {
                return _dbConnection.ExecuteScalar<int>($"SELECT COUNT(*) FROM {GetTableName()}");
            }
            catch (Exception ex)
            {
                Log.Warning("Dapper Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
            finally { _dbConnection.Close(); }
        }

        public async Task<List<T>> GetAllAsync()
        {
            _dbConnection.Open();

            try
            {
                var entity = await _dbConnection.QueryAsync<T>($"SELECT * FROM {GetTableName()}s");

                return entity.ToList();
            }
            catch (Exception ex)
            {
                Log.Warning("Dapper Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
            finally { _dbConnection.Close(); }
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        {
            _dbConnection.Open();

            try
            {
                var data = await _dbConnection.GetAllAsync<T>();
                var results = data.AsQueryable().Where(expression).ToList();
                return results;
            }
            catch (Exception ex)
            {
                Log.Warning("Dapper Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
            finally { _dbConnection.Close(); }
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            _dbConnection.Open();
            try
            {
                var data = await _dbConnection.GetAllAsync<T>();
                var results = data.AsQueryable().SingleOrDefault(expression);
                return results;
            }
            catch (Exception ex)
            {
                Log.Warning("Dapper Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
            finally { _dbConnection.Close(); }
        }

        public async Task<T> GetByGuidAsync(T id)
        {
            _dbConnection.Open();

            try
            {
                var entity = (await _dbConnection.QueryAsync<T>($"SELECT * FROM {GetTableName()}s WHERE Id = @Id", new { Id = id })).FirstOrDefault();
                return entity;
            }
            catch (Exception ex)
            {
                Log.Warning("Dapper Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
            finally { _dbConnection.Close(); }
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, bool? tracking = true, params Expression<Func<T, object>>[]? includeEntity)
        {
            try
            {
                IEnumerable<T> result;

                if (expression == null)
                {
                    result = await _dbConnection.GetAllAsync<T>();
                }
                else
                {
                    result = await _dbConnection.QueryAsync<T>($"SELECT * FROM YourTableName WHERE {GetSqlWhere(expression)}", expression);
                }

                return result.ToList();
            }
            catch (Exception ex)
            {
                Log.Warning("Dapper Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
            finally { _dbConnection.Close(); }
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression = null, bool? tracking = true, params Expression<Func<T, object>>[]? includeEntity)
        {
            _dbConnection.Open();

            try
            {
                T? result = await _dbConnection.QueryFirstOrDefaultAsync<T>($"SELECT * FROM {GetTableName()} WHERE {GetSqlWhere(expression)}", expression);
                return result;
            }
            catch (Exception ex)
            {
                Log.Warning("Dapper Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
            finally { _dbConnection.Close(); }
        }

        public async Task<T> GetByGuidAsync(string id, bool? tracking = true)
        {
            _dbConnection.Open();

            try
            {
                if (tracking ?? true)
                    return await _dbConnection.GetAsync<T>(id);
                else
                    return (await _dbConnection.QueryAsync<T>($"SELECT * FROM {GetTableName()}s WHERE Id = @Id", new { Id = Guid.Parse(id) })).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Log.Warning("Dapper Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
            finally { _dbConnection.Close(); }
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression, bool? tracking = true)
        {
            _dbConnection.Open();

            try
            {
                bool result;

                if (tracking ?? true)
                {
                    result = _dbConnection.QueryFirstOrDefault<bool>($"SELECT TOP 1 1 FROM {GetTableName()}");
                }
                else
                {
                    result = (await _dbConnection.QueryFirstOrDefaultAsync<int>($"SELECT COUNT(*) FROM YourTableName WHERE {GetSqlWhere(expression)}", expression)) > 0;
                }

                return result;
            }
            catch (Exception ex)
            {
                Log.Warning("Dapper Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
            finally { _dbConnection.Close(); }
        }

        #region This Place Got Error
        public async Task<int> CountAsync(Expression<Func<T, bool>> expression = null, bool? tracking = true)
        {
            _dbConnection.Open();

            try
            {
                int result;

                if (tracking ?? true)
                {
                    //result = await _dbConnection.CountAsync(expression);
                }
                else
                {
                    result = await _dbConnection.QueryFirstOrDefaultAsync<int>($"SELECT COUNT(*) FROM {GetTableName()} WHERE {GetSqlWhere(expression)}", expression);
                }
                return default;
            }
            catch (Exception ex)
            {
                Log.Warning("Dapper Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
            finally { _dbConnection.Close(); }
        }
        #endregion

        private string GetSqlWhere(Expression<Func<T, bool>> expression)
        {
            var binaryExpression = expression.Body as BinaryExpression;

            var left = binaryExpression?.Left.ToString();
            var right = binaryExpression?.Right.ToString();
            var binaryOperator = binaryExpression?.NodeType.ToString();

            return $"{left} {binaryOperator} {right}";
        }
    }
}
