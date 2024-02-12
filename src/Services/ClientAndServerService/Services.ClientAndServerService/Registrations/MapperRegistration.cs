using Microsoft.Extensions.DependencyInjection;
using BuildingBlock.Mapper;

namespace Services.ClientAndServerService.Registrations
{
    public static class MapperRegistration
    {
        public static IServiceCollection MapperServiceRegistration(this IServiceCollection services)
        {
            services.MapperServiceExtension(AssemblyReference.Assembly);

            return services;
        }
    }
}
