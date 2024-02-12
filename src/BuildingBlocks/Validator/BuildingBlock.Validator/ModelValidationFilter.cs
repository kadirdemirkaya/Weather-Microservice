using BuildingBlock.Base.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BuildingBlock.Validator
{
    public class ModelValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var apiError = new ErrorResponse
                {
                    StatusCode = 400,
                    StatusPhrase = "Bad Request",
                    TimeSpan = DateTime.Now,
                    Errors = new Dictionary<string, string>()
                };

                var errors = context.ModelState.AsEnumerable();

                foreach (var error in errors)
                {
                    foreach (var inn in error.Value!.Errors)
                    {
                        apiError.Errors.Add(error.Key, inn.ErrorMessage);
                    }
                }

                context.Result = new BadRequestObjectResult(apiError);
            }
        }
    }
}
