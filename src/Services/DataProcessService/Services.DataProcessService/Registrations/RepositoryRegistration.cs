using BuildingBlock.Base.Abstractions;
using BuildingBlock.Factory.Factories;
using Microsoft.Extensions.DependencyInjection;
using Services.DataProcessService.Aggregate;
using Services.DataProcessService.Aggregate.Current.Entities;
using Services.DataProcessService.Aggregate.Current.ValueObjects;
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

            services.AddScoped<IReadRepository<CWeather, WeatherId>>(sp =>
            {
                return RepositoryFactory<CWeather, WeatherId>.CreateReadRepository(GetConfigs.GetDatabaseConfig(), dbContext, sp);
            });

            services.AddScoped<IWriteRepository<CWeather, WeatherId>>(sp =>
            {
                return RepositoryFactory<CWeather, WeatherId>.CreateWriteRepository(GetConfigs.GetDatabaseConfig(), dbContext, sp);
            });

            services.AddScoped<IReadRepository<CurrentWeather, CurrentWeatherId>>(sp =>
            {
                return RepositoryFactory<CurrentWeather, CurrentWeatherId>.CreateReadRepository(GetConfigs.GetDatabaseConfig(), dbContext, sp);
            });

            services.AddScoped<IWriteRepository<CurrentWeather, CurrentWeatherId>>(sp =>
            {
                return RepositoryFactory<CurrentWeather, CurrentWeatherId>.CreateWriteRepository(GetConfigs.GetDatabaseConfig(), dbContext, sp);
            });

            return services;
        }
    }
}
