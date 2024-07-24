using Microsoft.OpenApi.Models;
using UTEHY.DatabaseCoursePortal.Api.Modules;

namespace UTEHY.DatabaseCoursePortal.Api.Providers
{
    public static class AppProvider
    {
        public static IServiceCollection AddAppProvider(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddHttpClient();

            return services;
        }
    }
}
