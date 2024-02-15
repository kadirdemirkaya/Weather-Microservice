using BuildingBlock.Base.Configs;
using BuildingBlock.Base.Enums;
using BuildingBlock.Base.Options;
using Microsoft.Extensions.Configuration;

namespace Services.DataProcessService.Configurations.Configs
{
    public static class GetConfigs
    {
        private static IConfiguration GetConfiguration()
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            string _environmentProd = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMEN_STATE");

            //if (_environmentProd.Length < 1 || string.IsNullOrEmpty(_environmentProd))
            if (string.IsNullOrEmpty(_environmentProd))
                _environmentProd = environment;

            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{_environmentProd}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public static DatabaseConfig GetDatabaseConfig()
        {
            IConfiguration _configuration = GetConfiguration();

            #region docker .env
            //return new()
            //{
            //    ConnectionString = _configuration["ConnectionUrl"],
            //    DatabaseName = _configuration["DatabaseName"],
            //    DatabaseType = BuildingBlock.Base.Enums.DatabaseType.Mongo,
            //    TableName = _configuration["TableName"],
            //    RetryCount = 5
            //};
            #endregion

            return new()
            {
                ConnectionString = _configuration["DatabaseOptions:ConnectionUrl"],
                DatabaseName = _configuration["DatabaseOptions:DatabaseName"],
                DatabaseType = BuildingBlock.Base.Enums.DatabaseType.PostgreSQL,
                TableName = _configuration["DatabaseOptions:TableName"],
                RetryCount = 5
            };
        }

        public static LogConfig GetLogConfig()
        {
            IConfiguration _configuration = GetConfiguration();

            return new()
            {
                ApplicationName = _configuration["ElasticSearch:ApplicationName"],
                DefaultIndex = _configuration["ElasticSearch:DefaultIndex"],
                ConnectionUrl = _configuration["ElasticSearch:ElasticUrl"],
                Password = _configuration["ElasticSearch:Password"],
                UserName = _configuration["ElasticSearch:UserName"],
            };
        }

        public static EventBusConfig GetEventBusConfig()
        {
            IConfiguration _configuration = GetConfiguration();

            return new()
            {
                ConnectionRetryCount = int.Parse(_configuration["EventBusOptions:ConnectionRetryCount"]),
                SubscriberClientAppName = _configuration["EventBusOptions:SubscriberClientAppName"],
                EventBusType = EventBusType.RabbitMQ,
                EventNameSuffix = _configuration["EventBusOptions:EventNameSuffix"],
                EventBusConnectionString = _configuration["EventBusOptions:EventBusConnectionString"]
            };
        }

        public static RedisConfig GetRedisConfig()
        {
            IConfiguration _configuration = GetConfiguration();

            return new()
            {
                Connection = $"{_configuration["RedisOptions:Host"]}:{_configuration["RedisOptions:Port"]}",
                ConnectionRetryCount = int.Parse(_configuration["ConnectionRetryCount:ConnectionRetryCount"])
            };
        }
    }
}
