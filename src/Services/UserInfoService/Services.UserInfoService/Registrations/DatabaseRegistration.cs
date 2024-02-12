using BuildingBlock.Base.Extensions;
using BuildingBlock.Base.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services.UserInfoService.Data;

namespace Services.UserInfoService.Registrations
{
    public static class DatabaseRegistration
    {
        public static IServiceCollection DatabaseServiceRegistration(this IServiceCollection services)
        {
            var dbOptions = services.GetOptions<DatabaseOptions>(nameof(DatabaseOptions));
            services.AddDbContext<UserDbContext>(options =>
            {
                options.UseSqlServer(dbOptions.ConnectionUrl,
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(AssemblyReference.Assembly.GetName().Name);
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: System.TimeSpan.FromSeconds(30), null);
                });
            });

            return services;
        }
    }
}
