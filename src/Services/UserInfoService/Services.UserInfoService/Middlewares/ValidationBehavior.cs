using BuildingBlock.Base.Exceptions;
using BuildingBlock.Base.Models;
using BuildingBlock.Base.Models.Responses;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Services.UserInfoService.Middlewares
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse?>
       where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse?> Handle(TRequest request, RequestHandlerDelegate<TResponse?> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
                return next.Invoke();

            var context = new ValidationContext<TRequest>(request);
            var results = Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken))).Result;
            var failures = results.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            if (!failures.Any())
                return next.Invoke();

            var response = CreateValidationErrorResponse(failures);

            throw new PiplineValidationErrorException(response.Errors.ToString()!);
        }

        private ValidationErrorResponse CreateValidationErrorResponse(IEnumerable<ValidationFailure> failures)
        {
            var errors = failures.Select(failure => new ValidationError(failure.PropertyName, failure.ErrorMessage)).ToList();

            return ValidationErrorResponse.Create(errors);
        }
    }
}
