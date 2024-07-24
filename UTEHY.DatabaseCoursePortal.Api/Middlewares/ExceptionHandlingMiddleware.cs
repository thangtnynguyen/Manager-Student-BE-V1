using System.Text.Json;
using FluentValidation;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;

namespace UTEHY.DatabaseCoursePortal.Api.Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var statusCode = GetStatusCode(exception);

            var response = new ApiResult<object>
            {
                Status = false,
                Message = exception.Message,
                Data = exception
            };

            httpContext.Response.ContentType = "application/json";

            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private static int GetStatusCode(Exception exception)
        {
            switch (exception)
            {
                case BadHttpRequestException:
                    return StatusCodes.Status400BadRequest;
                case ValidationException:
                    return StatusCodes.Status400BadRequest;
                case FormatException:
                    return StatusCodes.Status422UnprocessableEntity;
                default:
                    return StatusCodes.Status500InternalServerError;
            }
        }
    }
}
