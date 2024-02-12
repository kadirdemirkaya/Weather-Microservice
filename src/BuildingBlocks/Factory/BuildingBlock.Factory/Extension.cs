using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Factory
{
    public static class Extension
    {
        public static IServiceCollection FactoryServiceExtension(this IServiceCollection services)
        {


            return services;
        }
    }
}