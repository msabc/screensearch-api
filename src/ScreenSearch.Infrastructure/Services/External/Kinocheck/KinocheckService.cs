using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Options;
using ScreenSearch.Configuration;
using ScreenSearch.Domain.Interfaces.Serialization;
using ScreenSearch.Domain.Interfaces.Services.External.Kinocheck;
using ScreenSearch.Domain.Models.Services.External.Kinocheck;
using ScreenSearch.Domain.Models.Services.External.Kinocheck.Dto;

namespace ScreenSearch.Infrastructure.Services.External.Kinocheck
{
    public class KinocheckService(
        IKinocheckSerializationOptions jsonCaseInsensitiveSerializationOptions,
        IOptions<ScreenSearchSettings> screenSearchOptions,
        HttpClient httpClient) : IKinocheckService
    {
        private readonly ScreenSearchSettings _settings = screenSearchOptions.Value;

        public async Task<KinocheckGetByIdResponse> GetMovieVideosAsync(int tmdbId, string language)
        {
            string queryParameters = GetQueryParameters(tmdbId, language);

            return await httpClient.GetFromJsonAsync<KinocheckGetByIdResponse>($"{_settings.KinocheckAPISettings.GetMovieVideosPath}?{queryParameters}");
        }

        public async Task<KinocheckGetByIdResponse> GetSeriesVideosAsync(int tmdbId, string language)
        {
            string queryParameters = GetQueryParameters(tmdbId, language);

            return await httpClient.GetFromJsonAsync<KinocheckGetByIdResponse>($"{_settings.KinocheckAPISettings.GetSeriesVideosPath}?{queryParameters}");
        }

        public async Task<IEnumerable<KinocheckVideoDto>> GetTrailersAsync(int tmdbId, string language)
        {
            string requestUri = $"{_settings.KinocheckAPISettings.GetLatestTrailersPath}?tmdb_id={tmdbId}";

            return await httpClient.GetFromJsonAsync<IEnumerable<KinocheckVideoDto>>(requestUri);
        }

        public async Task<KinocheckGetTrailersResponse> GetLatestTrailersAsync(int? page, string language) => 
            await GetAsync(_settings.KinocheckAPISettings.GetLatestTrailersPath, page, language);

        public async Task<KinocheckGetTrailersResponse> GetTrendingTrailersAsync(int? page, string language) =>
            await GetAsync(_settings.KinocheckAPISettings.GetTrendingTrailersPath, page, language);

        private async Task<KinocheckGetTrailersResponse> GetAsync(string url, int? page, string language)
        {
            string requestUri = GetUrlWithQueryParameters(url, page, language);

            var response = await httpClient.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<KinocheckGetTrailersResponse>(jsonResponse, jsonCaseInsensitiveSerializationOptions.GetOptions());
        }

        private static string GetUrlWithQueryParameters(string url, int? page, string language) => 
            page.HasValue ? $"{url}?{QueryParameterNames.Language}={language}&{QueryParameterNames.Page}={page}" : $"{url}?{QueryParameterNames.Language}={language}";

        private static string GetQueryParameters(int tmdbId, string language) => $"{QueryParameterNames.TMDBId}={tmdbId}&{QueryParameterNames.Language}={language}";
    }
}
