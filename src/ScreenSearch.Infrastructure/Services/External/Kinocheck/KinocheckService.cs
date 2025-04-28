using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ScreenSearch.Configuration;
using ScreenSearch.Domain.Interfaces.Services.External.Kinocheck;
using ScreenSearch.Domain.Models.Services.External.Kinocheck;
using ScreenSearch.Domain.Models.Services.External.Kinocheck.Dto;

namespace ScreenSearch.Infrastructure.Services.External.Kinocheck
{
    public class KinocheckService(
        IOptions<ScreenSearchSettings> screenSearchOptions,
        ILogger<KinocheckService> logger,
        HttpClient httpClient) : IKinocheckService
    {
        private readonly ScreenSearchSettings _settings = screenSearchOptions.Value;

        public async Task<IEnumerable<KinocheckTrailerDto>> GetTrailersAsync(int tmdbId)
        {
            string requestUri = $"{_settings.KinocheckAPISettings.GetLatestTrailersPath}?tmdb_id={tmdbId}";

            return await httpClient.GetFromJsonAsync<IEnumerable<KinocheckTrailerDto>>(requestUri);
        }

        public async Task<GetLatestTrailersResponse> GetLatestTrailersAsync(int page)
        {
            string requestUri = $"{_settings.KinocheckAPISettings.GetLatestTrailersPath}?page={page}";

            var response = await httpClient.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();

            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<GetLatestTrailersResponse>(jsonResponse, options);
        }
    }
}
