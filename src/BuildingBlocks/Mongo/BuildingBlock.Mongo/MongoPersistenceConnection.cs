using BuildingBlock.Base.Configs;
using BuildingBlock.Base.Exceptions;
using MongoDB.Driver;
using Newtonsoft.Json;
using Polly;
using Serilog;
using System.Net.Sockets;

namespace BuildingBlock.Mongo
{
    public class MongoPersistenceConnection<T> : IDisposable
        where T : class
    {
        private object lock_object = new object();
        private readonly int _retryCount;
        private bool _disposed;
        private string _connectionString;
        private string _collectionName;
        private IMongoCollection<T> _collection;
        private IMongoDatabase _database;
        private MongoClient _mongoClient;

        public MongoPersistenceConnection(IMongoDatabase database, string? connectionString = null, string collectionName = null, int retryCount = 5)
        {
            _retryCount = retryCount;
            _collectionName = collectionName ?? GetCollectionName();
            _database = database;
            if(connectionString is not null)
                _mongoClient = CreateClient(connectionString);
        }

        public MongoPersistenceConnection(DatabaseConfig dbConfig)
        {
            if (dbConfig.ConnectionString != null)
            {
                var connJson = JsonConvert.SerializeObject(dbConfig.ConnectionString, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                });
                _collectionName = GetCollectionName();
                _connectionString = dbConfig.ConnectionString.ToString();
                _mongoClient = CreateClient(_connectionString);
                _database = CreateDatabase(dbConfig.DatabaseName);
                _retryCount = dbConfig.RetryCount;
                _collection = GetCollection();
            }
        }

        public IMongoCollection<T> GetCollection()
            => _database.GetCollection<T>(typeof(T).Name);

        private MongoClient CreateClient(string connStr)
            => new(connStr);

        private IMongoDatabase CreateDatabase(string dbName)
            => _mongoClient.GetDatabase(dbName);

        private string GetCollectionName()
            => typeof(T).Name;

        public bool IsConnected => IsMongoConnected(_database);

        public void Clear() => Dispose();

        private bool IsMongoConnected(IMongoDatabase database)
        {
            try
            {
                var collectionNames = database.ListCollectionNames().ToList();
                return collectionNames.Count > 0;
            }
            catch (Exception ex)
            {
                Log.Error("Mongo persistence error : " + ex.Message);
                return false;
            }
        }

        public bool TryConnect()
        {
            lock (lock_object)
            {
                var policy = Policy.Handle<SocketException>()
                    .Or<MongoException>()
                    .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                    {
                    }
                );

                policy.Execute(() =>
                {
                    if (!IsMongoConnected(_database))
                    {
                        throw new DatabaseErrorException("Connection error ", "MongoDb");
                    }
                    return true;
                });
                return false;
            }
        }

        public void Dispose()
        {
            _disposed = true;
            _database = null;
            _collection = null;
        }
    }
}
