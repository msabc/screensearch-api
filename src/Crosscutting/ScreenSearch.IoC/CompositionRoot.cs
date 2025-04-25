using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScreenSearch.Configuration;

namespace ScreenSearch.IoC
{
    public static class CompositionRoot
    {
        public static ScreenSearchSettings RegisterApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = services.RegisterSettings(configuration);

            var dbSettings = services.RegisterDatabaseSettings(configuration);

            return settings;
        }

        private static ScreenSearchSettings RegisterSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ScreenSearchSettings>(options => configuration.GetSection(nameof(ScreenSearchSettings)).Bind(options));

            var settings = new ScreenSearchSettings();
            configuration.GetSection(nameof(ScreenSearchSettings)).Bind(settings);

            return settings;
        }

        private static ConnectionStrings RegisterDatabaseSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConnectionStrings>(options => configuration.GetSection(nameof(ConnectionStrings)).Bind(options));

            var dbSettings = new ConnectionStrings();
            configuration.GetSection(nameof(ConnectionStrings)).Bind(dbSettings);

            return dbSettings;
        }
    }
}
