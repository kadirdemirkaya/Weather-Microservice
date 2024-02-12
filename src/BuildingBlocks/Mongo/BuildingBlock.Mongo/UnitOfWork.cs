using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Configs;
using BuildingBlock.Base.Models.Base;
using Serilog;

namespace BuildingBlock.Mongo
{
    public class UnitOfWork : IUnitOfWork
    {
        private DatabaseConfig _databaseConfig;
        private string _serviceName;
        private Func<string, Task> _eventPublish;
        private readonly IServiceProvider _serviceProvider;
        public UnitOfWork(DatabaseConfig databaseConfig, Func<string, Task>? eventPublish, string serviceName, IServiceProvider serviceProvider)
        {
            _databaseConfig = databaseConfig;
            _serviceName = serviceName ;
            _serviceProvider = serviceProvider;
            _eventPublish = eventPublish ?? null;
        }

        public IReadRepository<T> GetReadRepository<T>() where T : class, new()
            => new ReadRepository<T>(_databaseConfig, typeof(T).Name);

        public IReadRepository<T, TId> GetReadRepository<T, TId>()
            where T : Entity<TId>, new()
            where TId : ValueObject
            => new ReadRepository<T, TId>(_databaseConfig, typeof(T).Name);
        public IWriteRepository<T> GetWriteRepository<T>() where T : class, new()
            => new WriteRepository<T>(_databaseConfig, typeof(T).Name);

        public IWriteRepository<T, TId> GetWriteRepository<T, TId>()
            where T : Entity<TId>, new()
            where TId : ValueObject
            => new WriteRepository<T, TId>(_databaseConfig, typeof(T).Name);

        public async Task<bool> PublishEventAsync()
        {
            try
            {
                if (_serviceName is not null)
                    _eventPublish.Invoke(_serviceName);
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.Error("Event Publish Error : " + ex.Message);
                return false;
            }
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException("This Method Not İmplemented !");
        }
    }
}
