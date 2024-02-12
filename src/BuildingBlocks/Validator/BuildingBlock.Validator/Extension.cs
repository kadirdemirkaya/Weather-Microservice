using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BuildingBlock.Validator
{
    public static class Extension
    {
        public static IServiceCollection ValidatorExtension(this IServiceCollection services, Assembly assembly)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddFluentValidation();

            services.AddValidatorsFromAssembly(assembly);

            return services;
        }
    }
}
