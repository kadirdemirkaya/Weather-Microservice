using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Configs;
using BuildingBlock.Base.Models.Base;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace BuildingBlock.Dapper
{
    public class UnitOfWork : IUnitOfWork
    {
        private DatabaseConfig _databaseConfig;
        private string _serviceName;
        private Func<string, Task> _eventPublish;
        private readonly DbContext _context;
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(DbContext context, DatabaseConfig databaseConfig, IServiceProvider serviceProvider)
        {
            _context = context;
            _databaseConfig = databaseConfig;
            _serviceProvider = serviceProvider;
        }

        public UnitOfWork(DbContext context, DatabaseConfig databaseConfig, Func<string, Task> eventPublish, string serviceName, IServiceProvider serviceProvider)
        {
            _context = context;
            _databaseConfig = databaseConfig;
            _eventPublish = eventPublish;
            _serviceName = serviceName;
            _serviceProvider = serviceProvider;
        }

        public IReadRepository<T> GetReadRepository<T>() where T : class, new()
            => new ReadRepository<T>(_databaseConfig, _context);

        public IReadRepository<T, TId> GetReadRepository<T, TId>()
            where T : Entity<TId>, new()
            where TId : ValueObject
            => new ReadRepository<T, TId>(_databaseConfig, _context);

        public IWriteRepository<T> GetWriteRepository<T>() where T : class, new()
            => new WriteRepository<T>(_databaseConfig, _context);

        public IWriteRepository<T, TId> GetWriteRepository<T, TId>()
            where T : Entity<TId>, new()
            where TId : ValueObject
            => new WriteRepository<T, TId>(_databaseConfig, _context);

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException("This Method Not İmplemented !");
        }

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
    }
}
