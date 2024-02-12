using BuildingBlock.Base.Configs;
using BuildingBlock.Base.Enums;
using Microsoft.Extensions.Configuration;

namespace Services.DataCaptureService.Configurations.Configs
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
    }
}
