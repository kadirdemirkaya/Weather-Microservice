using BuildingBlock.Base.Configs;
using BuildingBlock.Base.Models.Base;
using BuildingBlock.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace BuildingBlock.Mongo
{
    public static class Extension
    {
        public static IServiceCollection MongoDbServiceExtension<T, TId>(this IServiceCollection services, IConfiguration? configuration, DatabaseConfig? databaseConfig)
            where T : Entity<TId>
            where TId : ValueObject
        {
            if (databaseConfig is null)
            {
                var mongoClient = new MongoClient(configuration["DbConnectionString:ConnectionUrl"]);
                var database = mongoClient.GetDatabase(configuration["DbConnectionString:DatabaseName"]);
                var collection = database.GetCollection<T>(configuration["DbConnectionString:TableName"]);
                services.AddSingleton(collection);
            }

            var _mongoClient = new MongoClient(databaseConfig.ConnectionString.ToString());
            var _database = _mongoClient.GetDatabase(databaseConfig.DatabaseName);
            var _collection = _database.GetCollection<T>(databaseConfig.TableName);
            services.AddSingleton(_collection);

            return services;
        }

        public static IServiceCollection MongoDbServiceExtension<T>(this IServiceCollection services, IConfiguration? configuration, DatabaseConfig? databaseConfig)
          where T : class
        {
            if (databaseConfig is null)
            {
                var mongoClient = new MongoClient(configuration["DbConnectionString:ConnectionUrl"]);
                var database = mongoClient.GetDatabase(configuration["DbConnectionString:DatabaseName"]);
                var collection = database.GetCollection<T>(configuration["DbConnectionString:TableName"]);
                services.AddSingleton(collection);
            }

            var _mongoClient = new MongoClient(databaseConfig.ConnectionString.ToString());
            var _database = _mongoClient.GetDatabase(databaseConfig.DatabaseName);
            var _collection = _database.GetCollection<T>(databaseConfig.TableName);
            services.AddSingleton(_collection);

            return services;
        }

        public static WebApplicationBuilder MongoDbApplicationBuilderExtension(this WebApplicationBuilder builder,IConfiguration configuration)
        {
            builder.AddFileExtensionBuilder(configuration);

            return builder;
        }
        private static LogConfig GetLogConfig(IConfiguration configuration)
            => new()
            {
                ApplicationName = configuration["ElasticSearch:ApplicationName"],
                DefaultIndex = configuration["ElasticSearch:DefaultIndex"],
                ConnectionUrl = configuration["ElasticSearch:ElasticUrl"],
                Password = configuration["ElasticSearch:Password"],
                UserName = configuration["ElasticSearch:Username"]
            };
    }
}
