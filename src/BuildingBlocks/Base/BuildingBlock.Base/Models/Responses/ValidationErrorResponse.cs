using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlock.Base.Models.Responses
{
    public class ValidationErrorResponse : ServiceResponse
    {
        public List<ValidationError> Errors { get; set; }

        public ValidationErrorResponse(List<ValidationError> errors)
        {
            Success = false;
            Message = ResponseMessage.InvalidRequest;
            Errors = errors;
        }

        public static ValidationErrorResponse Create(List<ValidationError> errors)
        {
            return new ValidationErrorResponse(errors);
        }
    }

    public static class ResponseMessage
    {
        public const string ServerError = "Something went wrong. Please try again later.";
        public const string InvalidRequest = "Invalid Request";
        public const string SampleNotFound = "Sample Not Found";
        public const string AddedSuccessfully = "Successfully Added";
        public const string AddedFailed = "Added Failed";
    }
}
