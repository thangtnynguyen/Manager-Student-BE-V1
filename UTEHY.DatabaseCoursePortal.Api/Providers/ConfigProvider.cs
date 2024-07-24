using UTEHY.DatabaseCoursePortal.Api.Services;

namespace UTEHY.DatabaseCoursePortal.Api.Providers
{
    public static class ConfigProvider
    {
        public static IServiceCollection LoadConfigs(this IServiceCollection services)
        {
            //var serviceProvider = services.BuildServiceProvider();
            //var configService = serviceProvider.GetRequiredService<ConfigService>();
            //configService.LoadConfigsAsync().Wait();

            return services;
        }
    }
}
