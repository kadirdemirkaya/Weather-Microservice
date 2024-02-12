using BuildingBlock.Base.Extensions;
using BuildingBlock.Base.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BuildingBlock.HealthCheck
{
    public static class Extensions
    {
        public static IServiceCollection HealthCheckServiceExtension(this IServiceCollection services)
        {
            var healthOptions = services.GetOptions<HealthOptions>(nameof(HealthOptions));

            var databaseOptions = services.GetOptions<DatabaseOptions>(nameof(DatabaseOptions));

            var rabbitmqOptions = services.GetOptions<RabbitMqOptions>(nameof(RabbitMqOptions));

            var redisOptions = services.GetOptions<RedisOptions>(nameof(RedisOptions));

            services.AddHealthChecks();

            if (databaseOptions.ConnectionUrl is not null)
                services.AddHealthChecks()
                    .AddMongoDb(databaseOptions.ConnectionUrl, databaseOptions.DatabaseName!, databaseOptions.TableName);

            if (databaseOptions.ConnectionUrl is not null)
                services.AddHealthChecks()
                    .AddSqlServer(databaseOptions.ConnectionUrl, name: "Sql-Health_Check");

            if (rabbitmqOptions.RabbitMqUrl is not null)
                services.AddHealthChecks()
                    .AddRabbitMQ(rabbitmqOptions.RabbitMqUrl, name: "Rabbit-Health_Check");

            if (redisOptions.Uri is not null)
                services.AddHealthChecks()
                    .AddRedis(redisOptions.Uri, name: "Redis-Health_Check");

            return services;
        }
        public static WebApplication HealthCheckApplicationExtension(this WebApplication app)
        {
            var healthOptions = app.Configuration.GetOptions<HealthOptions>(nameof(HealthOptions));

            if (!healthOptions.Enabled) return app;

            app.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    var result = report.Status == HealthStatus.Healthy ? "healthy" : "unhealthy";
                    await context.Response.WriteAsync($"Overall Status: {result}");
                },
            });

            return app;
        }
    }
}
