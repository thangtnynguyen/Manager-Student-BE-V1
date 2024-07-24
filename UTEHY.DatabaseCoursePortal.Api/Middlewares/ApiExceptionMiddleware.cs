using Newtonsoft.Json;
using System.Net;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Exceptions;

namespace UTEHY.DatabaseCoursePortal.Api.Middlewares
{
    public class ApiExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (UnauthorizedAccessException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                await HandleExceptionAsync(context, ex);
            }
            catch (BadHttpRequestException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                await HandleExceptionAsync(context, ex);
            }
            catch (ArgumentNullException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await HandleExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var jsonErrorResponse = JsonConvert.SerializeObject(ex);
            await context.Response.WriteAsync(jsonErrorResponse);
        }
    }
}
