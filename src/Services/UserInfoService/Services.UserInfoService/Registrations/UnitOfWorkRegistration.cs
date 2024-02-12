using BuildingBlock.Base.Abstractions;
using BuildingBlock.Factory.Factories;
using Microsoft.Extensions.DependencyInjection;
using Services.UserInfoService.Configurations.Configs;
using Services.UserInfoService.Data;
using static Services.UserInfoService.Constants.Constant;

namespace Services.UserInfoService.Registrations
{
    public static class UnitOfWorkRegistration
    {
        public static IServiceCollection UnitOfWorkServiceRegistration(this IServiceCollection services,IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<UserDbContext>();

            var pubService = serviceProvider.GetRequiredService<IPubEventService>();

            Func<string, Task> pubEvent = pubService.PublishDomainEventAsync;

            services.AddScoped(sp =>
            {
                return UnitOfWorkFactory.CreateUnitOfWork(GetConfigs.GetDatabaseConfig(), context, pubEvent, serviceProvider, Application.Name);
            });

            return services;
        }
    }
}
