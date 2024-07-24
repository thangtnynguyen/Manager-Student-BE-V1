using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;

namespace UTEHY.DatabaseCoursePortal.Api.Modules
{
    public class SwaggerModule : IDocumentFilter, IOperationFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var path in swaggerDoc.Paths.ToList())
            {
                var lowercasePath = path.Key.ToLowerInvariant();
                if (lowercasePath != path.Key)
                {
                    swaggerDoc.Paths[lowercasePath] = path.Value;
                    swaggerDoc.Paths.Remove(path.Key);
                }
            }
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.ApiDescription == null || operation.Parameters == null)
                return;

            if (!context.ApiDescription.ParameterDescriptions.Any())
                return;


            context.ApiDescription.ParameterDescriptions.Where(p => p.Source.Equals(BindingSource.Form)
                        && p.CustomAttributes().Any(p => p.GetType().Equals(typeof(JsonIgnoreAttribute))))
                .ToList().ForEach(p => operation.RequestBody.Content.Values.Single(v => v.Schema.Properties.Remove(p.Name)));



            context.ApiDescription.ParameterDescriptions.Where(p => p.Source.Equals(BindingSource.Query)
                          && p.CustomAttributes().Any(p => p.GetType().Equals(typeof(JsonIgnoreAttribute))))
                .ToList().ForEach(p => operation.Parameters.Remove(operation.Parameters.Single(w => w.Name.Equals(p.Name))));
        }
    }
}
