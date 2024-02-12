using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Configs;
using BuildingBlock.Base.Models.Base;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;

namespace BuildingBlock.MsSql
{
    public class WriteRepository<T, TId> : IWriteRepository<T, TId>
         where T : Entity<TId>
         where TId : ValueObject
    {
        private SqlPersistenceConnection persistenceConnection;
        private DbContext DbContext;

        public WriteRepository(DatabaseConfig dbConfig, DbContext dbContext)
        {
            if (dbConfig.ConnectionString != null)
            {
                var connJson = JsonConvert.SerializeObject(dbConfig.ConnectionString, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                });
                persistenceConnection = new SqlPersistenceConnection(dbConfig, dbContext, 5);
                DbContext = persistenceConnection.GetContext;
            }
        }

        public WriteRepository(DatabaseConfig dbConfig, DbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbConfig.ConnectionString != null)
            {
                var connJson = JsonConvert.SerializeObject(dbConfig.ConnectionString, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                });
                persistenceConnection = new SqlPersistenceConnection(dbConfig, dbContext, 5);
                DbContext = persistenceConnection.GetContext;
            }
        }

        public DbSet<T> Table => DbContext.Set<T>();

        public async Task<bool> CreateAsync(T entity)
        {
            try
            {
                await Table.AddAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("MsSql Error : " + ex.Message);
                return false;
            }
        }

        public bool Delete(T entity)
        {
            try
            {
                Table.Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("MsSql Error : " + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteByIdAsync(T entityId)
        {
            try
            {
                Table.Remove(entityId);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("MsSql Error : " + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteByIdAsync(string entityId)
        {
            try
            {
                /*
                    Stored Procedure
                 */
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("MsSql Error : " + ex.Message);
                return false;
            }
        }

        public bool UpdateAsync(T entity)
        {
            try
            {
                Table.Update(entity);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("MsSql Error : " + ex.Message);
                return false;
            }
        }
    }
    


    public class WriteRepository<T> : IWriteRepository<T>
        where T : class
    {
        private SqlPersistenceConnection persistenceConnection;
        private DbContext DbContext;

        public WriteRepository(DatabaseConfig databaseConfig, DbContext? dbContext)
        {
            if (databaseConfig.ConnectionString != null)
            {
                var connJson = JsonConvert.SerializeObject(databaseConfig.ConnectionString, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                });
                persistenceConnection = new SqlPersistenceConnection(databaseConfig, dbContext, 5);
                DbContext = persistenceConnection.GetContext;
            }
        }

        public WriteRepository(DatabaseConfig databaseConfig, DbContext? dbContext, IServiceProvider serviceProvider)
        {
            if (databaseConfig.ConnectionString != null)
            {
                var connJson = JsonConvert.SerializeObject(databaseConfig.ConnectionString, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                });
                persistenceConnection = new SqlPersistenceConnection(databaseConfig, dbContext, 5);
                DbContext = persistenceConnection.GetContext;
            }
        }

        private DbSet<T> _table => DbContext.Set<T>();

        public async Task<bool> CreateAsync(T entity)
        {
            try
            {
                await _table.AddAsync(entity);
                return true;
            }
            catch (System.Exception ex)
            {
                Log.Error("MsSql Error : " + ex.Message);
                return false;
            }
        }

        public bool Delete(T entity)
        {
            try
            {
                _table.Remove(entity);
                return true;
            }
            catch (System.Exception ex)
            {
                Log.Error("MsSql Error : " + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteByIdAsync(T entityId)
        {
            try
            {
                _table.Remove(entityId);
                return true;
            }
            catch (System.Exception ex)
            {
                Log.Error("MsSql Error : " + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteByIdAsync(string entityId)
        {
            try
            {
                /*
                    Stored Procedure
                 */
                return true;
            }
            catch (System.Exception ex)
            {
                Log.Error("MsSql Error : " + ex.Message);
                return false;
            }
        }

        public bool UpdateAsync(T entity)
        {
            try
            {
                _table.Update(entity);
                return true;
            }
            catch (System.Exception ex)
            {
                Log.Error("MsSql Error : " + ex.Message);
                return false;
            }
        }
    }
}
