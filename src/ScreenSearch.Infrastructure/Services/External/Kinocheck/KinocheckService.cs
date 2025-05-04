using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Options;
using ScreenSearch.Configuration;
using ScreenSearch.Configuration.Models.External;
using ScreenSearch.Domain.Interfaces.Serialization;
using ScreenSearch.Domain.Interfaces.Services.External.Kinocheck;
using ScreenSearch.Domain.Models.Services.External.Kinocheck;
using ScreenSearch.Domain.Models.Services.External.Kinocheck.Dto;

namespace ScreenSearch.Infrastructure.Services.External.Kinocheck
{
    public class KinocheckService(
        IKinocheckSerializationOptions kinocheckSerializationOptions,
        IOptions<ScreenSearchSettings> screenSearchOptions,
        HttpClient httpClient) : IKinocheckService
    {
        private static readonly string _defaultLanguage = "en";
        private readonly KinocheckAPISettingsElement _apiSettings = screenSearchOptions.Value.KinocheckAPISettings;

        public async Task<KinocheckGetByIdResponse> GetMovieVideosAsync(int tmdbId)
        {
            string queryParameters = GetQueryParameters(tmdbId);

            return await httpClient.GetFromJsonAsync<KinocheckGetByIdResponse>($"{_apiSettings.GetMovieVideosPath}?{queryParameters}");
        }

        public async Task<KinocheckGetByIdResponse> GetSeriesVideosAsync(int tmdbId)
        {
            string queryParameters = GetQueryParameters(tmdbId);

            return await httpClient.GetFromJsonAsync<KinocheckGetByIdResponse>($"{_apiSettings.GetSeriesVideosPath}?{queryParameters}");
        }

        public async Task<IEnumerable<KinocheckVideoDto>> GetTrailersAsync(int tmdbId)
        {
            string requestUri = $"{_apiSettings.GetLatestTrailersPath}?{QueryParameterNames.TMDBId}={tmdbId}";

            return await httpClient.GetFromJsonAsync<IEnumerable<KinocheckVideoDto>>(requestUri);
        }

        public async Task<KinocheckGetTrailersResponse> GetLatestTrailersAsync(int? page) => 
            await GetAsync(_apiSettings.GetLatestTrailersPath, page);

        public async Task<KinocheckGetTrailersResponse> GetTrendingTrailersAsync(int? page) =>
            await GetAsync(_apiSettings.GetTrendingTrailersPath, page);

        private async Task<KinocheckGetTrailersResponse> GetAsync(string url, int? page)
        {
            string requestUri = GetUrlWithQueryParameters(url, page);

            var response = await httpClient.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<KinocheckGetTrailersResponse>(
                jsonResponse, 
                kinocheckSerializationOptions.GetOptions()
            );
        }

        private static string GetUrlWithQueryParameters(string url, int? page) => 
            page.HasValue ? 
            $"{url}?{QueryParameterNames.Language}={_defaultLanguage}&{QueryParameterNames.Page}={page}" : 
            $"{url}?{QueryParameterNames.Language}={_defaultLanguage}";

        private static string GetQueryParameters(int tmdbId) => $"{QueryParameterNames.TMDBId}={tmdbId}&{QueryParameterNames.Language}={_defaultLanguage}";
    }
}
