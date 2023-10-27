using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GamesGlobalCore.Models
{
    public static class ConfigSettingsModule
    {
        public static void AddConfigSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CacheConfig>(configuration.GetSection(CacheConfig.ConfigName));
        }
    }
}
