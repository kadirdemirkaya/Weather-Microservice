using BuildingBlock.Base.Abstractions;
using BuildingBlock.Factory.Factories;
using Microsoft.Extensions.DependencyInjection;
using Services.DataProcessService.Configurations.Configs;
using Services.DataProcessService.Data;

namespace Services.DataProcessService.Registrations
{
    public static class UnitOfWorkRegistration
    {
        public static IServiceCollection UnitOfWorkServiceRegistration(this IServiceCollection services)
        {
            var _sp = services.BuildServiceProvider();
            var dbContext = _sp.GetRequiredService<WeatherDbContext>();

            services.AddScoped<IUnitOfWork>(sp =>
            {
                return UnitOfWorkFactory.CreateUnitOfWork(GetConfigs.GetDatabaseConfig(), dbContext, null, sp);
            });

            return services;
        }
    }
}
