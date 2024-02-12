using BuildingBlock.Validator;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Services.UserInfoService.Middlewares;

namespace Services.UserInfoService.Registrations
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

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
