using System.Net;
using System.Net.Http.Json;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ScreenSearch.Configuration;
using ScreenSearch.Domain.Interfaces.Services.TMDB;
using ScreenSearch.Domain.Models.Services.TMDB.Discover;
using ScreenSearch.Infrastructure.Extensions;

namespace ScreenSearch.Infrastructure.Services.TMDB
{
    public class TMDBAPIService(
        IOptions<ScreenSearchSettings> screenSearchOptions,
        ILogger<TMDBAPIService> logger,
        HttpClient httpClient) : ITMDBAPIService
    {
        private readonly ScreenSearchSettings _settings = screenSearchOptions.Value;

        public async Task<GetTMDBDiscoverResponse> DiscoverMoviesAsync(GetTMDBDiscoverRequest request)
        {
            try
            {
                return await DiscoverAsync(_settings.TMDBAPISettings.DiscoverMoviesPath, request);
            }
            catch (Exception ex)
            {
                logger.LogError($"{nameof(TMDBAPIService)}.{nameof(DiscoverMoviesAsync)} error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<GetTMDBDiscoverResponse> DiscoverShowsAsync(GetTMDBDiscoverRequest request)
        {
            try
            {
                return await DiscoverAsync(_settings.TMDBAPISettings.DiscoverShowsPath, request);
            }
            catch (Exception ex)
            {
                logger.LogError($"{nameof(TMDBAPIService)}.{nameof(DiscoverShowsAsync)} error occurred: {ex.Message}");
                throw;
            }
        }

        private async Task<GetTMDBDiscoverResponse> DiscoverAsync(string basePath, GetTMDBDiscoverRequest request)
        {
            var queryParameterStringBuilder = new StringBuilder();

            var queryParams = request.ToQueryDictionary();

            foreach (var kvp in queryParams)
                queryParameterStringBuilder.Append($"{kvp.Key}={WebUtility.UrlEncode(kvp.Value)}");

            string requestUri = $"{basePath}?{queryParameterStringBuilder}";

            return await httpClient.GetFromJsonAsync<GetTMDBDiscoverResponse>(requestUri);
        }
    }
}
