using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScreenSearch.Application.Services.Discover;
using ScreenSearch.Configuration;
using ScreenSearch.Domain.Interfaces.Services.TMDB;
using ScreenSearch.Infrastructure.Services.TMDB;

namespace ScreenSearch.IoC
{
    public static class CompositionRoot
    {
        public static void RegisterApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = services.RegisterSettings(configuration);

            services.RegisterDatabaseSettings(configuration);

            services.RegisterHttpClients(settings)
                    .RegisterApplicationServices();
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

        private static IServiceCollection RegisterHttpClients(this IServiceCollection services, ScreenSearchSettings settings)
        {
            services.AddHttpClient<ITMDBAPIService, TMDBAPIService>(client =>
            {
                client.BaseAddress = new Uri(settings.TMDBAPISettings.BaseURL);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", settings.TMDBAPISettings.AccessToken);
            });

            return services;
        }

        private static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDiscoverService, DiscoverService>();

            return services;
        }
    }
}
