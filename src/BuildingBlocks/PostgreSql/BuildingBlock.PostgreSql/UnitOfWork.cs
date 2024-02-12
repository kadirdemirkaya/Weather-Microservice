using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Configs;
using BuildingBlock.Base.Models.Base;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace BuildingBlock.PostgreSql
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private DatabaseConfig _databaseConfig;
        private Func<string, Task> _eventPublish;
        private string _serviceName;
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(DbContext context, DatabaseConfig databaseConfig, IServiceProvider serviceProvider)
        {
            _context = context;
            _databaseConfig = databaseConfig;
            _serviceProvider = serviceProvider;
        }

        public UnitOfWork(DbContext context, DatabaseConfig databaseConfig, Func<string, Task>? eventPublish, string serviceName, IServiceProvider serviceProvider)
        {
            _context = context;
            _databaseConfig = databaseConfig;
            _eventPublish = eventPublish ?? null;
            _serviceName = serviceName;
            _serviceProvider = serviceProvider;
        }

        public async Task<int> SaveChangesAsync()
        {
            int result = await _context.SaveChangesAsync();
            if (_serviceName is not null)
                await _eventPublish.Invoke(_serviceName);
            return result;
        }

        public IReadRepository<T> GetReadRepository<T>() where T : class, new()
            => new ReadRepository<T>(_databaseConfig, _context, _serviceProvider);

        public IWriteRepository<T> GetWriteRepository<T>() where T : class, new()
            => new WriteRepository<T>(_databaseConfig, _context,_serviceProvider);

        public IReadRepository<T, TId> GetReadRepository<T, TId>() where T : Entity<TId>, new() where TId : ValueObject
            => new ReadRepository<T, TId>(_databaseConfig, _context, _serviceProvider);

        public IWriteRepository<T, TId> GetWriteRepository<T, TId>() where T : Entity<TId>, new() where TId : ValueObject
            => new WriteRepository<T, TId>(_databaseConfig, _context, _serviceProvider);

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
