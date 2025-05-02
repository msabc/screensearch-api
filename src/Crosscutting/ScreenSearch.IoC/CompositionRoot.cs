using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScreenSearch.Application.Services.Detail;
using ScreenSearch.Application.Services.LanguageResolver;
using ScreenSearch.Application.Services.Search;
using ScreenSearch.Application.Services.Trailer;
using ScreenSearch.Application.Services.Trending;
using ScreenSearch.Configuration;
using ScreenSearch.Domain.Interfaces.Caching;
using ScreenSearch.Domain.Interfaces.Repositories;
using ScreenSearch.Domain.Interfaces.Serialization;
using ScreenSearch.Domain.Interfaces.Services.External.Kinocheck;
using ScreenSearch.Domain.Interfaces.Services.External.TMDB;
using ScreenSearch.Infrastructure.Caching;
using ScreenSearch.Infrastructure.Repositories;
using ScreenSearch.Infrastructure.Serialization;
using ScreenSearch.Infrastructure.Services.External.Kinocheck;
using ScreenSearch.Infrastructure.Services.External.TMDB;
using StackExchange.Redis;

namespace ScreenSearch.IoC
{
    public static class CompositionRoot
    {
        public static ScreenSearchSettings RegisterApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = services.RegisterSettings(configuration);

            var connectionStrings = services.RegisterDatabaseSettings(configuration);

            services.RegisterCachingServices(connectionStrings)
                    .RegisterRepositories()
                    .RegisterHttpClients(settings)
                    .RegisterInfrastructureServices()
                    .RegisterSerializationOptions()
                    .RegisterApplicationServices();

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

        private static IServiceCollection RegisterCachingServices(this IServiceCollection services, ConnectionStrings connectionStrings)
        {
            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                return ConnectionMultiplexer.Connect(connectionStrings.RedisConnectionString);
            });

            return services;
        }

        private static IServiceCollection RegisterHttpClients(this IServiceCollection services, ScreenSearchSettings settings)
        {
            services.AddHttpClient<ITMDBService, TMDBService>(client =>
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

        private static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICachedDetailRepository, CachedDetailRepository>();

            return services;
        }

        private static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ICacheStore, CacheStore>();

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
            services.AddScoped<IDetailService, DetailService>();
            services.AddScoped<ITrendingService, TrendingService>();
            services.AddScoped<ILanguageResolverService, LanguageResolverService>();

            return services;
        }
    }
}
