namespace UTEHY.DatabaseCoursePortal.Api.Providers
{
    public static class AutoMapperProvider
    {
        public static IServiceCollection AddAutoMapperProvider(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Program));

            return services;
        }
    }
}
