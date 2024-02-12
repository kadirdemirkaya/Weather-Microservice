using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Configs;
using BuildingBlock.Base.Enums;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlock.Factory.Factories
{
    public static class UnitOfWorkFactory
    {
        public static IUnitOfWork CreateUnitOfWork(DatabaseConfig config, DbContext? dbContext = null, Func<string, Task>? eventPub = null, IServiceProvider serviceProvider = null, string? serviceName = null)
        {
            return config.DatabaseType switch
            {
                DatabaseType.MsSQL => new BuildingBlock.MsSql.UnitOfWork(dbContext, config, eventPub, serviceName, serviceProvider),
                DatabaseType.Mongo => new BuildingBlock.Mongo.UnitOfWork(config, eventPub, serviceName, serviceProvider),
                DatabaseType.Dapper => new BuildingBlock.Dapper.UnitOfWork(dbContext, config, eventPub, serviceName, serviceProvider),
                DatabaseType.PostgreSQL => new BuildingBlock.PostgreSql.UnitOfWork(dbContext, config, eventPub, serviceName, serviceProvider)
            };
        }
    }
}
