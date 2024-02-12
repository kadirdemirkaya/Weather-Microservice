using BuildingBlock.Base.Extensions;
using BuildingBlock.Base.Options;
using Elastic.CommonSchema.Serilog;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Filters;
using Serilog.Sinks.Elasticsearch;

namespace BuildingBlock.Logger
{
    public static class LogExtension
    {
        public static WebApplicationBuilder AddLogSeriLog(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            var databaseOptions = builder.Services.GetOptions<LoggerOptions>(nameof(LoggerOptions));

            Serilog.Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
                .MinimumLevel.Override("MassTransit", LogEventLevel.Debug)
                .Enrich.FromLogContext()
                .Enrich.WithCorrelationId()
                .Enrich.WithExceptionDetails()
                .Enrich.WithProperty("ApplicationName", $"{databaseOptions.ApplicationName}")
                .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.StaticFiles"))
                .WriteTo.Async(writeTo => writeTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}"))
                .WriteTo.Async(writeTo => writeTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(databaseOptions.ConnectionUrl))
                {
                    TypeName = null,
                    AutoRegisterTemplate = true,
                    IndexFormat = databaseOptions.DefaultIndex,
                    BatchAction = ElasticOpType.Create,
                    CustomFormatter = new EcsTextFormatter(),
                    ModifyConnectionSettings = x => x.BasicAuthentication(databaseOptions.UserName, databaseOptions.Password)
                }))
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Host.UseSerilog(Serilog.Log.Logger, true);

            return builder;
        }

        public static WebApplicationBuilder AddLogSeriLogFile(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            var databaseOptions = builder.Services.GetOptions<LoggerOptions>(nameof(LoggerOptions));

            Serilog.Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
                .MinimumLevel.Override("MassTransit", LogEventLevel.Debug)
                .Enrich.FromLogContext()
                .Enrich.WithCorrelationId()
                .Enrich.WithExceptionDetails()
                .Enrich.WithProperty("ApplicationName", $"{databaseOptions.ApplicationName}")
                .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.StaticFiles"))
                .WriteTo.Async(writeTo => writeTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}"))
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Host.UseSerilog(Serilog.Log.Logger, true);

            return builder;
        }

        public static WebApplicationBuilder AddLogSeriLogConsole(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            var databaseOptions = builder.Services.GetOptions<LoggerOptions>(nameof(LoggerOptions));

            Serilog.Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
                .MinimumLevel.Override("MassTransit", LogEventLevel.Debug)
                .Enrich.FromLogContext()
                .Enrich.WithCorrelationId()
                .Enrich.WithExceptionDetails()
                .Enrich.WithProperty("ApplicationName", $"{databaseOptions.ApplicationName}")
                .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.StaticFiles"))
                .WriteTo.Async(writeTo => writeTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}"))
                .WriteTo.Console()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Host.UseSerilog(Serilog.Log.Logger, true);

            return builder;
        }

        public static WebApplicationBuilder AddLogSeriLogSeq(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            var databaseOptions = builder.Services.GetOptions<LoggerOptions>(nameof(LoggerOptions));

            Serilog.Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
                .MinimumLevel.Override("MassTransit", LogEventLevel.Debug)
                .Enrich.FromLogContext()
                .Enrich.WithCorrelationId()
                .Enrich.WithExceptionDetails()
                .Enrich.WithProperty("ApplicationName", $"{databaseOptions.ApplicationName}")
                .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.StaticFiles"))
                .WriteTo.Async(writeTo => writeTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}"))
                .WriteTo.Seq(databaseOptions.ConnectionUrl)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Host.UseSerilog(Serilog.Log.Logger, true);

            return builder;
        }
    }
}



/*
 public static class LogExtension
    {
        public static WebApplicationBuilder AddLogSeriLog(this WebApplicationBuilder builder, LogConfig config, IConfiguration configuration)
        {
            var databaseOptions = builder.Services.GetOptions<LoggerOptions>(nameof(LoggerOptions));

            Serilog.Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
                .MinimumLevel.Override("MassTransit", LogEventLevel.Debug)
                .Enrich.FromLogContext()
                .Enrich.WithCorrelationId()
                .Enrich.WithExceptionDetails()
                .Enrich.WithProperty("ApplicationName", $"{databaseOptions.ApplicationName}")
                .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.StaticFiles"))
                .WriteTo.Async(writeTo => writeTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}"))
                .WriteTo.Async(writeTo => writeTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(config.ConnectionUrl))
                {
                    TypeName = null,
                    AutoRegisterTemplate = true,
                    IndexFormat = databaseOptions.DefaultIndex,
                    BatchAction = ElasticOpType.Create,
                    CustomFormatter = new EcsTextFormatter(),
                    ModifyConnectionSettings = x => x.BasicAuthentication(databaseOptions.UserName, databaseOptions.Password)
                }))
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Host.UseSerilog(Serilog.Log.Logger, true);

            return builder;
        }

        public static WebApplicationBuilder AddLogSeriLogFile(this WebApplicationBuilder builder, LogConfig config, IConfiguration configuration)
        {
            Serilog.Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
                .MinimumLevel.Override("MassTransit", LogEventLevel.Debug)
                .Enrich.FromLogContext()
                .Enrich.WithCorrelationId()
                .Enrich.WithExceptionDetails()
                .Enrich.WithProperty("ApplicationName", $"{config.ApplicationName}")
                .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.StaticFiles"))
                .WriteTo.Async(writeTo => writeTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}"))
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Host.UseSerilog(Serilog.Log.Logger, true);

            return builder;
        }

        public static WebApplicationBuilder AddLogSeriLogConsole(this WebApplicationBuilder builder, LogConfig config, IConfiguration configuration)
        {
            Serilog.Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
                .MinimumLevel.Override("MassTransit", LogEventLevel.Debug)
                .Enrich.FromLogContext()
                .Enrich.WithCorrelationId()
                .Enrich.WithExceptionDetails()
                .Enrich.WithProperty("ApplicationName", $"{config.ApplicationName}")
                .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.StaticFiles"))
                .WriteTo.Async(writeTo => writeTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}"))
                .WriteTo.Console()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Host.UseSerilog(Serilog.Log.Logger, true);

            return builder;
        }

        public static WebApplicationBuilder AddLogSeriLogSeq(this WebApplicationBuilder builder, LogConfig config, IConfiguration configuration)
        {
            Serilog.Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
                .MinimumLevel.Override("MassTransit", LogEventLevel.Debug)
                .Enrich.FromLogContext()
                .Enrich.WithCorrelationId()
                .Enrich.WithExceptionDetails()
                .Enrich.WithProperty("ApplicationName", $"{config.ApplicationName}")
                .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.StaticFiles"))
                .WriteTo.Async(writeTo => writeTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}"))
                .WriteTo.Seq(config.ConnectionUrl)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Host.UseSerilog(Serilog.Log.Logger, true);

            return builder;
        }
    }
 */