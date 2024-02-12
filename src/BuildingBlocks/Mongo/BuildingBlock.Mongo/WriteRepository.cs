using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Configs;
using BuildingBlock.Base.Exceptions;
using BuildingBlock.Base.Models.Base;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace BuildingBlock.Mongo
{
    public class WriteRepository<T, TId> : IWriteRepository<T, TId>
         where T : Entity<TId>
         where TId : ValueObject
    {
        private MongoPersistenceConnection<T> persistenceConnection;
        private readonly IMongoCollection<T> _collection;

        public WriteRepository(IMongoDatabase database, string? collectionName = null)
        {
            persistenceConnection = new(database, null, collectionName, 5);
        }

        public WriteRepository(DatabaseConfig databaseConfig, string? collectionName)
        {
            if (databaseConfig.ConnectionString != null)
            {
                persistenceConnection = new(databaseConfig);
                _collection = persistenceConnection.GetCollection();
            }
        }

        private string GetRepoName()
             => typeof(WriteRepository<>).Name;

        public async Task<bool> CreateAsync(T entity)
        {
            try
            {
                await _collection.InsertOneAsync(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(T entity)
        {
            try
            {
                var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(entity.Id.ToString()));
                DeleteResult? result = _collection.DeleteOne(filter);
                return result.DeletedCount == 1 ? true : false;
            }
            catch (Exception ex)
            {
                Serilog.Log.Warning("MongoDb Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
        }

        public async Task<bool> DeleteByIdAsync(string entityId) // !!! - _id._id
        {
            try
            {
                var filter = Builders<T>.Filter.Eq("_id._id", ObjectId.Parse(entityId));
                DeleteResult? result = await _collection.DeleteOneAsync(filter);
                return result.DeletedCount == 1 ? true : false;
            }
            catch (Exception ex)
            {
                Serilog.Log.Warning("MongoDb Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
        }

        public async Task<bool> DeleteByIdAsync(TId id) // !!! - TId
        {
            try
            {
                var filter = Builders<T>.Filter.Eq("_id", id);
                DeleteResult? result = await _collection.DeleteOneAsync(filter);
                return result.DeletedCount == 1 ? true : false;
            }
            catch (Exception ex)
            {
                Serilog.Log.Warning("MongoDb Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
        }

        public async Task<bool> DeleteByIdAsync(T entityId) // !!! - T-TID-Id
        {
            try
            {
                var filter = Builders<T>.Filter.Eq("_id", entityId.Id);
                DeleteResult? result = await _collection.DeleteOneAsync(filter);
                return result.DeletedCount == 1 ? true : false;
            }
            catch (Exception ex)
            {
                Serilog.Log.Warning("MongoDb Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
        }

        public bool UpdateAsync(T entity)
        {
            try
            {
                var filter = Builders<T>.Filter.Eq("_id", GetIdValue(entity));
                ReplaceOneResult? result = _collection.ReplaceOne(filter, entity);
                return result.MatchedCount == 1 ? true : false;
            }
            catch (Exception ex)
            {
                Serilog.Log.Warning("MongoDb Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
        }

        private object GetIdValue(T entity)
        {
            try
            {
                var idProperty = typeof(T).GetProperty("Id");
                return idProperty.GetValue(entity);
            }
            catch (Exception ex)
            {
                Serilog.Log.Warning("MongoDb Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
        }
    }

    public class WriteRepository<T> : IWriteRepository<T>
       where T : class
    {
        private MongoPersistenceConnection<T> persistenceConnection;
        private readonly IMongoCollection<T> _collection;

        public WriteRepository(IMongoDatabase database, string? collectionName = null)
        {
            persistenceConnection = new(database, null, collectionName, 5);
        }

        public WriteRepository(DatabaseConfig databaseConfig, string? collectionName)
        {
            if (databaseConfig.ConnectionString != null)
            {
                persistenceConnection = new(databaseConfig);
                _collection = persistenceConnection.GetCollection();
            }
        }

        private string GetRepoName()
            => typeof(WriteRepository<>).Name;

        public async Task<bool> CreateAsync(T entity)
        {
            try
            {
                await _collection.InsertOneAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                Serilog.Log.Warning("MongoDb Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
        }

        public async Task<bool> DeleteByIdAsync(string entityId)
        {
            try
            {
                var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(entityId));
                DeleteResult? result = await _collection.DeleteOneAsync(filter);
                return result.DeletedCount == 1 ? true : false;
            }
            catch (Exception ex)
            {
                Serilog.Log.Warning("MongoDb Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
        }

        #region !!!!!!!!
        public async Task<bool> DeleteByIdAsync(T entityId)
        {
            try
            {
                var filter = Builders<T>.Filter.Eq("_id", GetIdValue(entityId));
                DeleteResult? result = await _collection.DeleteOneAsync(filter);
                return result.DeletedCount == 1 ? true : false;
            }
            catch (Exception ex)
            {
                Serilog.Log.Warning("MongoDb Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
        }
        #endregion !!!!!!

        public bool UpdateAsync(T entity)
        {
            try
            {
                var filter = Builders<T>.Filter.Eq("_id", GetIdValue(entity));
                ReplaceOneResult? result = _collection.ReplaceOne(filter, entity);
                return result.ModifiedCount == 1 ? true : false;
            }
            catch (Exception ex)
            {
                Serilog.Log.Warning("MongoDb Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
        }

        private object GetIdValue(T entity)
        {
            try
            {
                var idProperty = typeof(T).GetProperty("Id");
                return idProperty.GetValue(entity);
            }
            catch (Exception ex)
            {
                Serilog.Log.Warning("MongoDb Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
        }

        public bool Delete(T entity)
        {
            try
            {
                var filter = Builders<T>.Filter.Eq("_id", GetIdValue(entity));
                DeleteResult? result = _collection.DeleteOne(filter);
                return result.DeletedCount == 1 ? true : false;
            }
            catch (Exception ex)
            {
                Serilog.Log.Warning("MongoDb Repository Error: " + ex.Message);
                throw new RepositoryErrorException(GetRepoName(), ex.Message);
            }
        }
    }
}
