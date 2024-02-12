using BuildingBlock.Base.Abstractions;
using BuildingBlock.Factory.Factories;
using Microsoft.Extensions.DependencyInjection;
using Services.UserInfoService.Aggregates;
using Services.UserInfoService.Aggregates.Entities;
using Services.UserInfoService.Aggregates.ValueObjects;
using Services.UserInfoService.Configurations.Configs;
using Services.UserInfoService.Data;

namespace Services.UserInfoService.Registrations
{
    public static class RepositoryRegistration 
    {
        public static IServiceCollection RepositoryServiceRegistration(this IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var context = sp.GetRequiredService<UserDbContext>();

            services.AddScoped<IReadRepository<User,UserId>>(sp =>
            {
                return RepositoryFactory<User, UserId>.CreateReadRepository(GetConfigs.GetDatabaseConfig(), context, sp);
            });

            services.AddScoped<IWriteRepository<User, UserId>>(sp =>
            {
                return RepositoryFactory<User, UserId>.CreateWriteRepository(GetConfigs.GetDatabaseConfig(), context, sp);
            });

            services.AddScoped<IReadRepository<Role, RoleId>>(sp =>
            {
                return RepositoryFactory<Role, RoleId>.CreateReadRepository(GetConfigs.GetDatabaseConfig(), context, sp);
            });

            services.AddScoped<IWriteRepository<Role, RoleId>>(sp =>
            {
                return RepositoryFactory<Role, RoleId>.CreateWriteRepository(GetConfigs.GetDatabaseConfig(), context, sp);
            });

            services.AddScoped<IReadRepository<RoleUser, RoleUserId>>(sp =>
            {
                return RepositoryFactory<RoleUser, RoleUserId>.CreateReadRepository(GetConfigs.GetDatabaseConfig(), context, sp);
            });

            services.AddScoped<IWriteRepository<RoleUser, RoleUserId>>(sp =>
            {
                return RepositoryFactory<RoleUser, RoleUserId>.CreateWriteRepository(GetConfigs.GetDatabaseConfig(), context, sp);
            });

            return services;
        }
    }
}
