using System.Net;
using System.Net.Http.Json;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ScreenSearch.Configuration;
using ScreenSearch.Domain.Interfaces.Services.External.TMDB;
using ScreenSearch.Domain.Models.Services.External.TMDB.Discover;
using ScreenSearch.Infrastructure.Extensions;

namespace ScreenSearch.Infrastructure.Services.External.TMDB
{
    public class TMDBAPIService(
        IOptions<ScreenSearchSettings> screenSearchOptions,
        ILogger<TMDBAPIService> logger,
        HttpClient httpClient) : ITMDBAPIService
    {
        private readonly ScreenSearchSettings _settings = screenSearchOptions.Value;

        public async Task<TMDBGetResponse> DiscoverMoviesAsync(TMDBGetRequest request)
        {
            try
            {
                return await GetAsync(_settings.TMDBAPISettings.DiscoverMoviesPath, request);
            }
            catch (Exception ex)
            {
                logger.LogError($"{nameof(TMDBAPIService)}.{nameof(DiscoverMoviesAsync)} error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<TMDBGetResponse> DiscoverShowsAsync(TMDBGetRequest request)
        {
            try
            {
                return await GetAsync(_settings.TMDBAPISettings.DiscoverShowsPath, request);
            }
            catch (Exception ex)
            {
                logger.LogError($"{nameof(TMDBAPIService)}.{nameof(DiscoverShowsAsync)} error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<TMDBGetResponse> SearchMoviesAsync(TMDBGetRequest request)
        {
            try
            {
                return await GetAsync(_settings.TMDBAPISettings.SearchMoviesPath, request);
            }
            catch (Exception ex)
            {
                logger.LogError($"{nameof(TMDBAPIService)}.{nameof(SearchMoviesAsync)} error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<TMDBGetResponse> SearchShowsAsync(TMDBGetRequest request)
        {
            try
            {
                return await GetAsync(_settings.TMDBAPISettings.SearchShowsPath, request);
            }
            catch (Exception ex)
            {
                logger.LogError($"{nameof(TMDBAPIService)}.{nameof(SearchShowsAsync)} error occurred: {ex.Message}");
                throw;
            }
        }

        private async Task<TMDBGetResponse> GetAsync(string basePath, TMDBGetRequest request)
        {
            var queryParameterStringBuilder = new StringBuilder();

            var queryParams = request.ToQueryDictionary();

            foreach (var kvp in queryParams)
                queryParameterStringBuilder.Append($"{kvp.Key}={WebUtility.UrlEncode(kvp.Value)}");

            string requestUri = $"{basePath}?{queryParameterStringBuilder}";

            return await httpClient.GetFromJsonAsync<TMDBGetResponse>(requestUri);
        }
    }
}
