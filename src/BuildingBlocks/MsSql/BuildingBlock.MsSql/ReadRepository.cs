using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Configs;
using BuildingBlock.Base.Models.Base;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;
using System.Linq.Expressions;

namespace BuildingBlock.MsSql
{
    public class ReadRepository<T, TId> : IReadRepository<T, TId>
            where T : Entity<TId>
            where TId : ValueObject
    {
        private SqlPersistenceConnection persistenceConnection;
        private DbContext DbContext;

        public ReadRepository(DatabaseConfig databaseConfig, DbContext? dbContext)
        {
            if (databaseConfig.ConnectionString != null)
            {
                var connJson = JsonConvert.SerializeObject(databaseConfig.ConnectionString, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                });
                persistenceConnection = new SqlPersistenceConnection(databaseConfig, dbContext);
            }
            DbContext = persistenceConnection.GetContext;
        }

        public ReadRepository(DatabaseConfig databaseConfig, DbContext? dbContext, IServiceProvider serviceProvider)
        {
            if (databaseConfig.ConnectionString != null)
            {
                var connJson = JsonConvert.SerializeObject(databaseConfig.ConnectionString, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                });
                persistenceConnection = new SqlPersistenceConnection(databaseConfig, dbContext);
            }
            DbContext = persistenceConnection.GetContext;
        }

        private DbSet<T> Table => DbContext.Set<T>();

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression, bool tracking = true)
        {
            try
            {
                var query = Table.AsQueryable();
                if (!tracking)
                    query = query.AsNoTracking();

                if (expression is not null)
                    return await query.AnyAsync(expression);

                if (expression != null)
                    query = query.Where(expression);

                return await query.AnyAsync();
            }
            catch (Exception ex)
            {
                Log.Error("MsSql Error : " + ex.Message);
                return false;
            }
        }

        public async Task<bool> AnyAsync()
            => await AnyAsync(null, true);

        public async Task<int> CountAsync(Expression<Func<T, bool>> expression = null, bool tracking = true)
        {
            try
            {
                var query = Table.AsQueryable();
                if (!tracking)
                    query = query.AsNoTracking();

                if (expression is not null)
                    return await query.CountAsync(expression);

                if (expression != null)
                    query = query.Where(expression);

                return await query.CountAsync();
            }
            catch (Exception ex)
            {
                Log.Error("MsSql Error : " + ex.Message);
                return default;
            }
        }

        public async Task<int> CountAsync()
            => await CountAsync(null,true);

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, bool tracking = true, params Expression<Func<T, object>>[] includeEntity)
        {
            try
            {
                var query = Table.AsQueryable();
                if (!tracking)
                    query = query.AsNoTracking();

                if (includeEntity.Any())
                    foreach (var include in includeEntity)
                        query = query.Include(include);

                if (expression != null)
                    query = query.Where(expression);

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Error("MsSql Error : " + ex.Message);
                return null;
            }
        }

        public async Task<List<T>> GetAllAsync()
        {
            try
            {
                return await Table.ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Error("MsSql Error : " + ex.Message);
                return null;
            }
        }

        public async Task<T> GetAsync(TId id) // ?
        {
            try
            {
                return await Table.Where(t => t.Id == id).SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Log.Error("MsSql Error : " + ex.Message);
                return null;
            }
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression = null, bool tracking = true, params Expression<Func<T, object>>[] includeEntity)
        {
            try
            {
                var query = Table.AsQueryable();

                if (!tracking)
                    query = query.AsNoTracking();

                if (includeEntity.Any())
                    foreach (var include in includeEntity)
                        query = query.Include(include);

                if (expression != null)
                    query = query.Where(expression);

                return await query.SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Log.Error("MsSql Error : " + ex.Message);
                return default;
            }
        }

        public async Task<T> GetByGuidAsync(string id, bool tracking = true)
        {
            throw new Exception("");
        }
    }

    public class ReadRepository<T> : IReadRepository<T>
        where T : class
    {
        private SqlPersistenceConnection persistenceConnection;
        private DbContext DbContext;
        private IUnitOfWork _unitOfWork;

        public ReadRepository(DatabaseConfig databaseConfig, DbContext? dbContext)
        {
            if (databaseConfig.ConnectionString != null)
            {
                var connJson = JsonConvert.SerializeObject(databaseConfig.ConnectionString, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                });
                persistenceConnection = new SqlPersistenceConnection(databaseConfig, dbContext, 5);
            }
            DbContext = persistenceConnection.GetContext;
        }

        public ReadRepository(DatabaseConfig databaseConfig, DbContext? dbContext, IServiceProvider serviceProvider)
        {
            if (databaseConfig.ConnectionString != null)
            {
                var connJson = JsonConvert.SerializeObject(databaseConfig.ConnectionString, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                });
                persistenceConnection = new SqlPersistenceConnection(databaseConfig, dbContext, 5);
            }
            DbContext = persistenceConnection.GetContext;
        }

        private DbSet<T> Table => DbContext.Set<T>();

        public async Task<bool> AnyAsync()
            => await AnyAsync(null, true);

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression, bool? tracking = true)
        {
            try
            {
                var query = Table.AsQueryable();
                if (!tracking ?? true)
                    query = query.AsNoTracking();

                if (expression is not null)
                    return await query.AnyAsync(expression);

                if (expression != null)
                    query = query.Where(expression);

                return await query.AnyAsync();
            }
            catch (Exception ex)
            {
                Log.Error("MsSql Error : " + ex.Message);
                return false;
            }
        }

        public async Task<int> CountAsync()
            => await CountAsync(null, true);

        public async Task<int> CountAsync(Expression<Func<T, bool>> expression = null, bool? tracking = true)
        {
            try
            {
                var query = Table.AsQueryable();
                if (!tracking ?? true)
                    query = query.AsNoTracking();

                if (expression is not null)
                    return await query.CountAsync(expression);

                if (expression != null)
                    query = query.Where(expression);

                return await query.CountAsync();
            }
            catch (Exception ex)
            {
                Log.Error("MsSql Error : " + ex.Message);
                return default;
            }
        }

        public async Task<List<T>> GetAllAsync()
            => await Table.ToListAsync();

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, bool? tracking = true, params Expression<Func<T, object>>[]? includeEntity)
        {
            try
            {
                var query = Table.AsQueryable();
                if (!tracking ?? true)
                    query = query.AsNoTracking();

                if (includeEntity.Any())
                    foreach (var include in includeEntity)
                        query = query.Include(include);

                if (expression != null)
                    query = query.Where(expression);

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Error("MsSql Error : " + ex.Message);
                return default;
            }
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression = null, bool? tracking = true, params Expression<Func<T, object>>[]? includeEntity)
        {
            try
            {
                var query = Table.AsQueryable();
                if (!tracking ?? true)
                    query = query.AsNoTracking();

                if (includeEntity.Any())
                    foreach (var include in includeEntity)
                        query = query.Include(include);

                if (expression != null)
                    query = query.Where(expression);

                return await query.SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Log.Error("MsSql Error : " + ex.Message);
                return default;
            }
        }


        public async Task<T> GetByGuidAsync(string id, bool? tracking = true)
        {
            try
            {
                IQueryable<T> query = Table.AsQueryable();

                if (!tracking ?? true)
                    query = query.AsNoTracking();

                return await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == Guid.Parse(id));
            }
            catch (Exception ex)
            {
                Log.Error("MsSql Error : " + ex.Message);
                return default;
            }
        }
    }
}
