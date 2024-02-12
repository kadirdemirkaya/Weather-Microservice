using BuildingBlock.Base.Abstractions;
using BuildingBlock.Factory.Factories;
using Microsoft.Extensions.DependencyInjection;
using Services.LocationService.Aggregate;
using Services.LocationService.Aggregate.ValueObjects;
using Services.LocationService.Configurations.Configs;

namespace Services.LocationService.Registrations
{
    public static class RepositoryRegistration
    {
        public static IServiceCollection RepositoryServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped<IReadRepository<City,CityId>>(sp =>
            {
                return RepositoryFactory<City, CityId>.CreateReadRepository(GetConfigs.GetDatabaseConfig(), null, sp);
            });

            services.AddScoped<IWriteRepository<City, CityId>>(sp =>
            {
                return RepositoryFactory<City, CityId>.CreateWriteRepository(GetConfigs.GetDatabaseConfig(), null, sp);
            });

            return services;
        }
    }
}
