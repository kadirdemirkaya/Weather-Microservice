using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BuildingBlock.Validator
{
    public static class Validation
    {
        public static ModelStateDictionary ModelValidate<T>(this IValidator<T> validator, T model)
            where T : notnull
        {
            ValidationResult validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
            {
                var modelStateDic = new ModelStateDictionary();

                foreach (ValidationFailure failure in validationResult.Errors)
                    modelStateDic.AddModelError(failure.PropertyName, failure.ErrorMessage);

                return modelStateDic;
            }

            return new ModelStateDictionary();
        }
    }
}
