using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using BuildingBlock.Validator;
using FluentValidation.AspNetCore;
using FluentValidation;

namespace Services.ClientAndServerService.Registrations
{
    public static class ValidationRegistration
    {
        public static IServiceCollection ValidationServiceRegistration(this IServiceCollection services)
        {
            services.ValidatorExtension(AssemblyReference.Assembly);

            services.AddFluentValidationAutoValidation(opt =>
            {
                opt.DisableDataAnnotationsValidation = true;
            }).AddValidatorsFromAssembly(AssemblyReference.Assembly);

            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
