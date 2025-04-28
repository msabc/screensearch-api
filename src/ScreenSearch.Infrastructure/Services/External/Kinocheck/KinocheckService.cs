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

        public async Task<IEnumerable<KinocheckTrailerDto>> GetTrailersAsync(int tmdbId)
        {
            string requestUri = $"{_settings.KinocheckAPISettings.GetLatestTrailersPath}?tmdb_id={tmdbId}";

            return await httpClient.GetFromJsonAsync<IEnumerable<KinocheckTrailerDto>>(requestUri);
        }

        public async Task<GetTrailersResponse> GetLatestTrailersAsync(int? page) => await GetAsync(_settings.KinocheckAPISettings.GetLatestTrailersPath, page);

        public async Task<GetTrailersResponse> GetTrendingTrailersAsync(int? page) => await GetAsync(_settings.KinocheckAPISettings.GetTrendingTrailersPath, page);

        private async Task<GetTrailersResponse> GetAsync(string url, int? page)
        {
            string requestUri = page.HasValue ? $"{url}?language=en&page={page}" : url;

            var response = await httpClient.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<GetTrailersResponse>(jsonResponse, jsonCaseInsensitiveSerializationOptions.GetOptions());
        }
    }
}
