using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Configs;
using BuildingBlock.Base.Exceptions;
using BuildingBlock.Base.Models.Base;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace BuildingBlock.Mongo
{
    public class ReadRepository<T, TId> : IReadRepository<T, TId>
            where T : Entity<TId>
            where TId : ValueObject
    {
        private MongoPersistenceConnection<T> persistenceConnection;
        private IMongoCollection<T> _collection;

        public ReadRepository(IMongoDatabase database, string? collectionName = null)
        {
            persistenceConnection = new(database, null, collectionName, 5);
            _collection = persistenceConnection.GetCollection();
        }

        public ReadRepository(DatabaseConfig databaseConfig, string? collectionName = null)
        {
            if (databaseConfig.ConnectionString != null)
            {
                persistenceConnection = new(databaseConfig);
                _collection = persistenceConnection.GetCollection();
            }
        }

        private string GetRepoName()
            => typeof(ReadRepository<,>).Name;

        public Task<bool> AnyAsync()
            => AnyAsync(null);

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression, bool tracking = true)
        {
            try
            {
                if (expression != null)
                {
                    var filter = Builders<T>.Filter.Where(expression);
                    var result = _collection.CountDocuments(filter);
                    return result > 0;
                }
                else
                {
                    var result = _collection.EstimatedDocumentCount();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                Serilog.Log.Warning("MongoDb Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
        }

        public Task<int> CountAsync()
            => CountAsync(null);

        public async Task<int> CountAsync(Expression<Func<T, bool>> expression = null, bool tracking = true)
        {
            try
            {
                if (expression != null)
                {
                    var filter = Builders<T>.Filter.Where(expression);
                    var result = _collection.CountDocuments(filter);
                    return (int)result;
                }
                else
                {
                    var result = _collection.EstimatedDocumentCount();
                    return (int)result;
                }
            }
            catch (Exception ex)
            {
                Serilog.Log.Warning("MongoDb Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
        }

        public async Task<List<T>> GetAllAsync()
            => await GetAllAsync(null);

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, bool tracking = true, params Expression<Func<T, object>>[] includeEntity)
        {
            try
            {
                var result = new List<T>();

                if (expression == null)
                    result = await _collection.Find(new BsonDocument()).ToListAsync();
                else
                    result = await _collection.Find(expression).ToListAsync();

                foreach (var item in result)
                {
                    var id = GetIdFromObject(item);
                }

                return result;
            }
            catch (Exception ex)
            {
                Serilog.Log.Warning("MongoDb Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression = null, bool tracking = true, params Expression<Func<T, object>>[] includeEntity)
        {
            try
            {
                var filter = expression ?? Builders<T>.Filter.Empty;
                var cursor = await _collection.FindAsync(filter);
                var result = await cursor.FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                Serilog.Log.Warning("MongoDb Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
        }

        public async Task<T> GetByGuidAsync(string id, bool tracking = true)
        {
            try
            {
                var filter = Builders<T>.Filter.Eq("_id._id", ObjectId.Parse(id));
                var cursor = await _collection.FindAsync(filter);
                var result = await cursor.FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                Serilog.Log.Warning("MongoDb Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
        }

        private object GetValueFromExpression(Expression expression)
        {
            if (expression is ConstantExpression)
            {
                return (expression as ConstantExpression).Value;
            }
            else if (expression is MemberExpression)
            {
                var objectMember = Expression.Convert(expression, typeof(object));
                var getterLambda = Expression.Lambda<Func<object>>(objectMember);
                var getter = getterLambda.Compile();
                return getter();
            }
            else
            {
                throw new InvalidOperationException("Invalid expression type");
            }
        }

        private string GetIdFromObject(T obj)
        {
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty != null)
            {
                var idValue = idProperty.GetValue(obj) as ObjectId?;
                if (idValue != null)
                {
                    return idValue.ToString();
                }
            }

            return null;
        }
    }


    public class ReadRepository<T> : IReadRepository<T>
      where T : class
    {
        private MongoPersistenceConnection<T> persistenceConnection;
        private IMongoCollection<T> _collection;

        public ReadRepository(IMongoDatabase database, string? collectionName = null)
        {
            persistenceConnection = new(database, null, collectionName, 5);
        }

        public ReadRepository(DatabaseConfig databaseConfig, string? collectionName = null)
        {
            if (databaseConfig.ConnectionString != null)
            {
                persistenceConnection = new(databaseConfig);
                _collection = persistenceConnection.GetCollection();
            }
        }

        private string GetRepoName()
            => typeof(ReadRepository<>).Name;

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression, bool tracking = true)
        {
            try
            {
                return await _collection.Find(expression).AnyAsync();
            }
            catch (Exception ex)
            {
                Serilog.Log.Warning("MongoDb Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
        }

        public async Task<bool> AnyAsync()
            => await AnyAsync(null, true);

        public async Task<int> CountAsync()
            => await CountAsync(null, true);

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, bool tracking = true)
        {
            try
            {
                if (expression != null)
                {
                    return await _collection.Find(expression).ToListAsync();
                }
                else
                {
                    return await _collection.Find(_ => true).ToListAsync();
                }
            }
            catch (Exception ex)
            {
                Serilog.Log.Warning("MongoDb Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
        }

        public async Task<List<T>> GetAllAsync()
            => await GetAllAsync(null, true, null);

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, bool? tracking = true, params Expression<Func<T, object>>[]? includeEntity)
        {
            try
            {
                return await _collection.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                Serilog.Log.Warning("MongoDb Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression = null, bool? tracking = true, params Expression<Func<T, object>>[]? includeEntity)
        {
            try
            {
                return await _collection.Find(expression).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Serilog.Log.Warning("MongoDb Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
        }

        public async Task<T> GetByGuidAsync(string id, bool? tracking = true)
        {
            try
            {
                var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
                return await _collection.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Serilog.Log.Warning("MongoDb Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression, bool? tracking = true)
        {
            try
            {
                return await _collection.Find(_ => true).AnyAsync();
            }
            catch (Exception ex)
            {
                Serilog.Log.Warning("MongoDb Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> expression = null, bool? tracking = true)
        {
            try
            {
                if (expression != null)
                {
                    return (int)await _collection.CountDocumentsAsync(expression);
                }
                else
                {
                    return (int)await _collection.CountDocumentsAsync(_ => true);
                }
            }
            catch (Exception ex)
            {
                Serilog.Log.Warning("MongoDb Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
        }
    }
}