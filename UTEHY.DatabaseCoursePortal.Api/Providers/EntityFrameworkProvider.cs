using Microsoft.EntityFrameworkCore;
using UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore;

namespace UTEHY.DatabaseCoursePortal.Api.Providers
{
    public static class EntityFrameworkProvider
    {
        public static IServiceCollection AddEntityFrameworkProvider(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }, ServiceLifetime.Transient);

            return services;
        }
    }
}
