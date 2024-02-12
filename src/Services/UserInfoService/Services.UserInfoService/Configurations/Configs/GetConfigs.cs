using BuildingBlock.Base.Configs;
using Consul;
using Microsoft.Extensions.Configuration;

namespace Services.UserInfoService.Configurations.Configs
{
    public static class GetConfigs
    {
        private static IConfiguration GetConfiguration()
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            string _environmentProd = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMEN_STATE");

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

            #region docker .env, get configuration
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
                DatabaseType = BuildingBlock.Base.Enums.DatabaseType.MsSQL,
                TableName = _configuration["DatabaseOptions:TableName"],
                RetryCount = 5
            };
        }

        public static LogConfig GetLogConfig()
        {
            IConfiguration _configuration = GetConfiguration();

            return new()
            {
                ApplicationName = _configuration["LoggerOptions:ApplicationName"],
                DefaultIndex = _configuration["LoggerOptions:DefaultIndex"],
                ConnectionUrl = _configuration["LoggerOptions:ConnectionUrl"],
                Password = _configuration["LoggerOptions:Password"],
                UserName = _configuration["LoggerOptions:UserName"],
            };
        }

        public static EventBusConfig GetEventBusConfig()
        {
            IConfiguration _configuration = GetConfiguration();

            return new()
            {
                ConnectionRetryCount = int.Parse(_configuration["EventBusOptions:ConnectionRetryCount"]),
                DefaultTopicName = _configuration["EventBusOptions:DefaultTopicName"],
                EventBusConnectionString = _configuration["EventBusOptions:EventBusConnectionString"],
                EventBusType = BuildingBlock.Base.Enums.EventBusType.RabbitMQ,
                EventNamePrefix = null,
                EventNameSuffix = _configuration["EventBusOptions:EventNameSuffix"],
                SubscriberClientAppName = _configuration["EventBusOptions:SubscriberClientAppName"]
            };
        }

        public static string GetEncryptionKey()
        {
            IConfiguration _configuration = GetConfiguration();
            return _configuration["Encryption:Key"]!;
        }
        /*
        public static class AuthenticationConsul
        {
            public const string Tag = "Authentication";
            public const string ID = "AuthenticationService";
            public const string Name = "AuthenticationService";
            public const string Host = "localhost";
            public const string Port = "5285";
        }
         */
        public static AgentServiceRegistration GetAgentServiceRegistration()
        {
            IConfiguration _configuration = GetConfiguration();

            return new()
            {
                ID = _configuration["AgentService:Name"],
                Name = _configuration["AgentService:Name"],
                Address = _configuration["AgentService:Address"],
                Port = int.Parse(_configuration["AgentService:Port"]),
                Tags = new[] { _configuration["AgentService:Name"], _configuration["AgentService:Tag"] },
            };
        }
    }
}
