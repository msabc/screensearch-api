using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScreenSearch.Application.Services.Search;
using ScreenSearch.Application.Services.Trailer;
using ScreenSearch.Configuration;
using ScreenSearch.Domain.Interfaces.Serialization;
using ScreenSearch.Domain.Interfaces.Services.External.Kinocheck;
using ScreenSearch.Domain.Interfaces.Services.External.TMDB;
using ScreenSearch.Infrastructure.Serialization;
using ScreenSearch.Infrastructure.Services.External.Kinocheck;
using ScreenSearch.Infrastructure.Services.External.TMDB;

namespace ScreenSearch.IoC
{
    public static class CompositionRoot
    {
        public static void RegisterApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = services.RegisterSettings(configuration);

            services.RegisterDatabaseSettings(configuration);

            services.RegisterHttpClients(settings)
                    .RegisterSerializationOptions()
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

            services.AddHttpClient<IKinocheckService, KinocheckService>(client =>
            {
                client.BaseAddress = new Uri(settings.KinocheckAPISettings.BaseURL);
            });

            return services;
        }

        private static IServiceCollection RegisterSerializationOptions(this IServiceCollection services)
        {
            services.AddSingleton<IKinocheckSerializationOptions, KinocheckSerializationOptions>();

            return services;
        }

        private static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<ITrailerService, TrailerService>();

            return services;
        }
    }
}
