using BuildingBlock.Base.Exceptions;
using BuildingBlock.Base.Extensions;
using Microsoft.AspNetCore.Http;
using System.Net;
using Serilog;

namespace Services.DataCaptureService.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (RepositoryErrorException ex)
            {
                await HandleExceptionAsync(context, ex, (HttpStatusCode)400);
            }
            catch (ServiceErrorException ex)
            {
                await HandleExceptionAsync(context, ex, (HttpStatusCode)400);
            }
            catch (ValueNullErrorException ex)
            {
                await HandleExceptionAsync(context, ex, (HttpStatusCode)404);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, (HttpStatusCode)500);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex, HttpStatusCode statusCode = (HttpStatusCode)500)
        {
            Log.Error("Error message : " + ex.Message);
            var code = statusCode;
            var result = (new { error = $"Error appeared when maked process : + {ex.Message}" }).SerialJson();
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
