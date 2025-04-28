using System.Net.Http.Json;
using System.Net;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ScreenSearch.Configuration;
using ScreenSearch.Domain.Interfaces.Services.External.Kinocheck;
using ScreenSearch.Domain.Models.Services.External.Kinocheck;
using ScreenSearch.Domain.Models.Services.External.TMDB.Discover;

namespace ScreenSearch.Infrastructure.Services.External.Kinocheck
{
    public class KinocheckService(
        IOptions<ScreenSearchSettings> screenSearchOptions,
        ILogger<KinocheckService> logger,
        HttpClient httpClient) : IKinocheckService
    {
        private readonly ScreenSearchSettings _settings = screenSearchOptions.Value;

        public async Task<IEnumerable<KinocheckGetResponse>> GetTrailersAsync(int tmdbId)
        {
            string requestUri = $"{_settings.KinocheckAPISettings.GetLatestTrailersPath}?tmdb_id={tmdbId}";

            return await httpClient.GetFromJsonAsync<IEnumerable<KinocheckGetResponse>>(requestUri);
        }

        public async Task<KinocheckGetResponse> GetLatestTrailersAsync(int page)
        {
            return await httpClient.GetFromJsonAsync<KinocheckGetResponse>(_settings.KinocheckAPISettings.GetLatestTrailersPath);
        }

        public async Task<KinocheckGetResponse> GetTrendingTrailersAsync(int page, int limit)
        {
            return await httpClient.GetFromJsonAsync<KinocheckGetResponse>(_settings.KinocheckAPISettings.GetTrendingTrailersPath);
        }
    }
}
