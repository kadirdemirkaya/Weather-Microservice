using BuildingBlock.Base.Configs;
using BuildingBlock.Base.Enums;
using Microsoft.Extensions.Configuration;

namespace Services.LocationService.Configurations.Configs
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
                DatabaseType = DatabaseType.Mongo,
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

        public static EventBusConfig GetRabbitMqDefaultEventBusConfig
        {
            get
            {
                IConfiguration configuration = GetConfiguration();
                return new()
                {
                    ConnectionRetryCount = int.Parse(configuration["EventBusOptions:ConnectionRetryCount"]),
                    EventNameSuffix = configuration["EventBusOptions:EventNameSuffix"],
                    SubscriberClientAppName = configuration["EventBusOptions:SubscriberClientAppName"],
                    EventBusType = EventBusType.RabbitMQ
                };
            }
        }
    }
}
