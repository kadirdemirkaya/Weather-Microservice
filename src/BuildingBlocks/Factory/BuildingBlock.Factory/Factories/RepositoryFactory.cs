using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Configs;
using BuildingBlock.Base.Enums;
using BuildingBlock.Base.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlock.Factory.Factories
{
    public static class RepositoryFactory<T, TId>
        where T : Entity<TId>
         where TId : ValueObject
    {
        public static IWriteRepository<T, TId> CreateWriteRepository(DatabaseConfig config, DbContext? dbContext = null, IServiceProvider serviceProvider = null)
        {
            return config.DatabaseType switch
            {
                DatabaseType.MsSQL => new BuildingBlock.MsSql.WriteRepository<T, TId>(config, dbContext, serviceProvider),
                DatabaseType.Dapper => new BuildingBlock.Dapper.WriteRepository<T, TId>(config, dbContext),
                DatabaseType.Mongo => new BuildingBlock.Mongo.WriteRepository<T, TId>(config, typeof(T).Name),
                DatabaseType.PostgreSQL => new BuildingBlock.PostgreSql.WriteRepository<T, TId>(config, dbContext, serviceProvider)
            };
        }

        public static IReadRepository<T, TId> CreateReadRepository(DatabaseConfig config, DbContext? dbContext = null, IServiceProvider serviceProvider = null)
        {
            return config.DatabaseType switch
            {
                DatabaseType.MsSQL => new BuildingBlock.MsSql.ReadRepository<T, TId>(config, dbContext, serviceProvider),
                DatabaseType.Dapper => new BuildingBlock.Dapper.ReadRepository<T, TId>(config, dbContext),
                DatabaseType.Mongo => new BuildingBlock.Mongo.ReadRepository<T, TId>(config, typeof(T).Name),
                DatabaseType.PostgreSQL => new BuildingBlock.PostgreSql.ReadRepository<T, TId>(config, dbContext, serviceProvider)
            };
        }
    }

    public static class RepositoryFactory<T>
       where T : class
    {
        public static IWriteRepository<T> CreateWriteRepository(DatabaseConfig config, DbContext? dbContext = null, IServiceProvider serviceProvider = null)
        {
            return config.DatabaseType switch
            {
                DatabaseType.MsSQL => new BuildingBlock.MsSql.WriteRepository<T>(config, dbContext, serviceProvider),
                DatabaseType.Dapper => new BuildingBlock.Dapper.WriteRepository<T>(config, dbContext),
                DatabaseType.Mongo => new BuildingBlock.Mongo.WriteRepository<T>(config, typeof(T).Name),
                DatabaseType.PostgreSQL => new BuildingBlock.PostgreSql.WriteRepository<T>(config, dbContext)
            };
        }

        public static IReadRepository<T> CreateReadRepository(DatabaseConfig config, DbContext? dbContext = null, IServiceProvider serviceProvider = null)
        {
            return config.DatabaseType switch
            {
                DatabaseType.MsSQL => new BuildingBlock.MsSql.ReadRepository<T>(config, dbContext, serviceProvider),
                DatabaseType.Dapper => new BuildingBlock.Dapper.ReadRepository<T>(config, dbContext),
                DatabaseType.Mongo => new BuildingBlock.Mongo.ReadRepository<T>(config, typeof(T).Name),
                DatabaseType.PostgreSQL => new BuildingBlock.PostgreSql.ReadRepository<T>(config, dbContext)
            };
        }
    }
}
