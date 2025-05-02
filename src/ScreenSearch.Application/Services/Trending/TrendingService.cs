using Microsoft.Extensions.Logging;
using ScreenSearch.Domain.Interfaces.Repositories;
using ScreenSearch.Domain.Interfaces.Services.External.TMDB;

namespace ScreenSearch.Application.Services.Trending
{
    public class TrendingService(
        ITMDBService tmdbService,
        ICachedDetailRepository cachedDetailRepository,
        ILogger<TrendingService> logger) : ITrendingService
    {
        public async Task SaveTrendingDataAsync(string language)
        {
            try
            {
                var trendingMovies = await tmdbService.GetTrendingMoviesAsync(language);

                if (trendingMovies != null && trendingMovies.Results.Any())
                    await cachedDetailRepository.SaveMultipleMovieMetadataRecordsAsync(language, trendingMovies.Results.ToList());

                var trendingSeries = await tmdbService.GetTrendingSeriesAsync(language);

                if (trendingSeries != null && trendingSeries.Results.Any())
                    await cachedDetailRepository.SaveMultipleSeriesMetadataRecordsAsync(language, trendingSeries.Results.ToList());
            }
            catch (Exception ex)
            {
                logger.LogError($"{nameof(SaveTrendingDataAsync)} exception occurred: {ex.Message}");
                throw;
            }
        }
    }
}
