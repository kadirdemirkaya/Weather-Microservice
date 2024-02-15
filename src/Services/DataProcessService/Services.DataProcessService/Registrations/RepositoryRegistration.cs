using BuildingBlock.Base.Abstractions;
using BuildingBlock.Factory.Factories;
using BuildingBlock.Redis;
using Microsoft.Extensions.DependencyInjection;
using Services.DataProcessService.Aggregate;
using Services.DataProcessService.Aggregate.Air;
using Services.DataProcessService.Aggregate.Air.Entities;
using Services.DataProcessService.Aggregate.Air.ValueObjects;
using Services.DataProcessService.Aggregate.Current.Entities;
using Services.DataProcessService.Aggregate.Current.ValueObjects;
using Services.DataProcessService.Aggregate.Daily;
using Services.DataProcessService.Aggregate.Daily.Entities;
using Services.DataProcessService.Aggregate.Daily.ValueObjects;
using Services.DataProcessService.Aggregate.ValueObjects;
using Services.DataProcessService.Configurations.Configs;
using Services.DataProcessService.Data;

namespace Services.DataProcessService.Registrations
{
    public static class RepositoryRegistration
    {
        public static IServiceCollection RepositoryServiceRegistration(this IServiceCollection services)
        {
            var _sp = services.BuildServiceProvider();
            var dbContext = _sp.GetRequiredService<WeatherDbContext>();

            services.AddScoped<IReadRepository<CWeather, Aggregate.Current.ValueObjects.WeatherId>>(sp =>
            {
                return RepositoryFactory<CWeather, Aggregate.Current.ValueObjects.WeatherId>.CreateReadRepository(GetConfigs.GetDatabaseConfig(), dbContext, sp);
            });

            services.AddScoped<IReadRepository<CurrentWeather, CurrentWeatherId>>(sp =>
            {
                return RepositoryFactory<CurrentWeather, CurrentWeatherId>.CreateReadRepository(GetConfigs.GetDatabaseConfig(), dbContext, sp);
            });

            services.AddScoped<IReadRepository<DailyWeather, DailyWeatherId>>(sp =>
            {
                return RepositoryFactory<DailyWeather, DailyWeatherId>.CreateReadRepository(GetConfigs.GetDatabaseConfig(), dbContext, sp);
            });

            services.AddScoped<IReadRepository<DWeather, Aggregate.Daily.ValueObjects.WeatherId>>(sp =>
            {
                return RepositoryFactory<DWeather, Aggregate.Daily.ValueObjects.WeatherId>.CreateReadRepository(GetConfigs.GetDatabaseConfig(), dbContext, sp);
            });

            services.AddScoped<IReadRepository<DList, DListId>>(sp =>
            {
                return RepositoryFactory<DList, DListId>.CreateReadRepository(GetConfigs.GetDatabaseConfig(), dbContext, sp);
            });

            services.AddScoped<IReadRepository<AirPollutionWeather, AirPollutionWeatherId>>(sp =>
            {
                return RepositoryFactory<AirPollutionWeather, AirPollutionWeatherId>.CreateReadRepository(GetConfigs.GetDatabaseConfig(), dbContext, sp);
            });

            services.AddScoped<IReadRepository<AList, AListId>>(sp =>
            {
                return RepositoryFactory<AList, AListId>.CreateReadRepository(GetConfigs.GetDatabaseConfig(), dbContext, sp);
            });


            services.AddScoped<IWriteRepository<DailyWeather, DailyWeatherId>>(sp =>
            {
                return RepositoryFactory<DailyWeather, DailyWeatherId>.CreateWriteRepository(GetConfigs.GetDatabaseConfig(), dbContext, sp);
            });

            services.AddScoped<IWriteRepository<DWeather, Aggregate.Daily.ValueObjects.WeatherId>>(sp =>
            {
                return RepositoryFactory<DWeather, Aggregate.Daily.ValueObjects.WeatherId>.CreateWriteRepository(GetConfigs.GetDatabaseConfig(), dbContext, sp);
            });

            services.AddScoped<IWriteRepository<DList, DListId>>(sp =>
            {
                return RepositoryFactory<DList, DListId>.CreateWriteRepository(GetConfigs.GetDatabaseConfig(), dbContext, sp);
            });

            services.AddScoped<IWriteRepository<CurrentWeather, CurrentWeatherId>>(sp =>
            {
                return RepositoryFactory<CurrentWeather, CurrentWeatherId>.CreateWriteRepository(GetConfigs.GetDatabaseConfig(), dbContext, sp);
            });

            services.AddScoped<IWriteRepository<CWeather, Aggregate.Current.ValueObjects.WeatherId>>(sp =>
            {
                return RepositoryFactory<CWeather, Aggregate.Current.ValueObjects.WeatherId>.CreateWriteRepository(GetConfigs.GetDatabaseConfig(), dbContext, sp);
            });

            services.AddScoped<IWriteRepository<AirPollutionWeather, AirPollutionWeatherId>>(sp =>
            {
                return RepositoryFactory<AirPollutionWeather, AirPollutionWeatherId>.CreateWriteRepository(GetConfigs.GetDatabaseConfig(), dbContext, sp);
            });
            services.AddScoped<IWriteRepository<AList, AListId>>(sp =>
            {
                return RepositoryFactory<AList, AListId>.CreateWriteRepository(GetConfigs.GetDatabaseConfig(), dbContext, sp);
            });

            return services;
        }
    }
}
