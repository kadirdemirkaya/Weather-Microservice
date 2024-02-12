using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Configs;
using BuildingBlock.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.MsSql
{
    public static class Extension
    {
        public static IServiceCollection MsSQLServiceExtension(this IServiceCollection services, MsSqlConfig msSqlConfig)
        {
            services.AddSingleton<MsSqlConfig>(msSqlConfig);

            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

            services.AddScoped(typeof(IReadRepository<,>), typeof(ReadRepository<,>));
            services.AddScoped(typeof(IWriteRepository<,>), typeof(WriteRepository<,>));

            services.AddScoped(typeof(IMsSqlService<,>), typeof(MsSqlService<,>));
            services.AddScoped(typeof(IMsSqlService<>), typeof(MsSqlService<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static WebApplicationBuilder MsSqlApplicationBuilderExtension(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            builder.AddFileExtensionBuilder(configuration);

            return builder;
        }
    }
}
