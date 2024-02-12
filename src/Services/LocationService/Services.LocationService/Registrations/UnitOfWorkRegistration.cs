using BuildingBlock.Base.Abstractions;
using BuildingBlock.Factory.Factories;
using Microsoft.Extensions.DependencyInjection;
using Services.LocationService.Configurations.Configs;

namespace Services.LocationService.Registrations
{
    public static class UnitOfWorkRegistration
    {
        public static IServiceCollection UnitOfWorkServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork>(sp =>
            {
                return UnitOfWorkFactory.CreateUnitOfWork(GetConfigs.GetDatabaseConfig(), null, null, sp);
            });

            return services;
        }
    }
}
