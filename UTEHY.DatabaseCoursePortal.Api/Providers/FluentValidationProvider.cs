using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace UTEHY.DatabaseCoursePortal.Api.Providers
{
    public static class FluentValidationProvider
    {
        public static IServiceCollection AddFluentValidationProvider(this IServiceCollection services)
        {
            services.AddFluentValidation(options =>
            {
                options.ImplicitlyValidateChildProperties = true;
                options.ImplicitlyValidateRootCollectionElements = true;

                options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                        );

                    var result = new BadRequestObjectResult(new
                    {
                        type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                        title = "One or more validation errors occurred.",
                        status = 400,
                        traceId = context.HttpContext.TraceIdentifier,
                        errors = errors,
                        message = errors.FirstOrDefault().Value.FirstOrDefault()
                    });

                    return result;
                };
            });

            return services;
        }
    }
}
