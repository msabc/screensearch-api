using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ScreenSearch.Configuration;
using ScreenSearch.Domain.Interfaces.Caching;
using ScreenSearch.Domain.Interfaces.Repositories;
using ScreenSearch.Domain.Models.Caching;
using ScreenSearch.Domain.Models.Services.External.Kinocheck.Dto;
using ScreenSearch.Domain.Models.Services.External.TMDB.Details;
using ScreenSearch.Domain.Models.Services.External.TMDB.Search.Response.Dto;
using ScreenSearch.Infrastructure.Constants;

namespace ScreenSearch.Infrastructure.Repositories
{
    public class CachedDetailRepository(
        ICacheStore cacheStore,
        ILogger<CachedDetailRepository> logger,
        IOptions<ScreenSearchSettings> screenSearchOptions) : ICachedDetailRepository
    {
        private readonly int _cacheExpirationInMinutes = screenSearchOptions.Value.CacheSettings.CacheExpirationInMinutes;

        public async Task<CachedMovieDetails> GetMovieDetailsAsync(int tmdbId, string language)
        {
            CachedMovieDetails detail = new();

            try
            {
                var slidingCacheExpiration = TimeSpan.FromMinutes(_cacheExpirationInMinutes);

                string? metadata = await cacheStore.GetValueAsync($"{CachePrefixes.MovieMetadataPrefix}:{tmdbId}:{language}", slidingCacheExpiration);
                string? videos = await cacheStore.GetValueAsync($"{CachePrefixes.MovieTrailersPrefix}:{tmdbId}:{language}", slidingCacheExpiration);

                if (!string.IsNullOrWhiteSpace(metadata))
                    detail.Metadata = JsonSerializer.Deserialize<TMDBSearchMoviesResponseDto>(metadata)!;

                if (!string.IsNullOrWhiteSpace(videos))
                    detail.Videos = JsonSerializer.Deserialize<List<KinocheckVideoDto>>(videos)!;
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occurred in {nameof(GetMovieDetailsAsync)} for {nameof(tmdbId)} {tmdbId}. {ex.Message}");
            }

            return detail;
        }

        public async Task<CachedShowDetails> GetSeriesDetailsAsync(int tmdbId, string language)
        {
            CachedShowDetails detail = new();

            try
            {
                var slidingCacheExpiration = TimeSpan.FromMinutes(_cacheExpirationInMinutes);

                string? metadata = await cacheStore.GetValueAsync($"{CachePrefixes.SeriesMetadataPrefix}:{tmdbId}:{language}", slidingCacheExpiration);
                string? videos = await cacheStore.GetValueAsync($"{CachePrefixes.SeriesTrailersPrefix}:{tmdbId}:{language}", slidingCacheExpiration);

                if (!string.IsNullOrWhiteSpace(metadata))
                    detail.Metadata = JsonSerializer.Deserialize<TMDBGetShowDetailsResponse>(metadata)!;

                if (!string.IsNullOrWhiteSpace(videos))
                    detail.Videos = JsonSerializer.Deserialize<List<KinocheckVideoDto>>(videos)!;
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occurred in {nameof(GetSeriesDetailsAsync)} for {nameof(tmdbId)} {tmdbId}. {ex.Message}");
            }

            return detail;
        }

        public async Task SaveMovieMetadataAsync(int tmdbId, string language, TMDBSearchMoviesResponseDto metadata)
        {
            try
            {
                await cacheStore.SetValueAsync(
                    $"{CachePrefixes.MovieMetadataPrefix}:{tmdbId}:{language}",
                    JsonSerializer.Serialize(metadata),
                    TimeSpan.FromMinutes(_cacheExpirationInMinutes)
                );
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occurred in {nameof(SaveMovieMetadataAsync)} for {nameof(tmdbId)} {tmdbId}. {ex.Message}");
            }
        }

        public async Task SaveMultipleMovieMetadataRecordsAsync(string language, List<TMDBSearchMoviesResponseDto> metadataRecords)
        {
            try
            {
                Dictionary<string, string> movieMetadataRecordsDictionary = metadataRecords.ToDictionary(
                    metadataRecord => $"{CachePrefixes.MovieMetadataPrefix}:{metadataRecord.Id}:{language}",
                    metadataRecord => JsonSerializer.Serialize(metadataRecord)
                );

                await cacheStore.SetValuesAsync(
                    movieMetadataRecordsDictionary,
                    TimeSpan.FromMinutes(_cacheExpirationInMinutes)
                );
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occurred in {nameof(SaveMultipleMovieMetadataRecordsAsync)}: {ex.Message}");
            }
        }

        public async Task SaveMovieTrailersAsync(int tmdbId, string language, List<KinocheckVideoDto> trailers)
        {
            try
            {
                await cacheStore.SetValueAsync(
                    $"{CachePrefixes.MovieTrailersPrefix}:{tmdbId}:{language}",
                    JsonSerializer.Serialize(trailers),
                    TimeSpan.FromMinutes(_cacheExpirationInMinutes)
                );
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occurred in {nameof(SaveMovieTrailersAsync)} for {nameof(tmdbId)} {tmdbId}. {ex.Message}");
            }
        }

        public async Task SaveSeriesMetadataAsync(int tmdbId, string language, TMDBGetShowDetailsResponse metadata)
        {
            try
            {
                await cacheStore.SetValueAsync(
                    $"{CachePrefixes.SeriesMetadataPrefix}:{tmdbId}:{language}",
                    JsonSerializer.Serialize(metadata),
                    TimeSpan.FromMinutes(_cacheExpirationInMinutes)
                );
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occurred in {nameof(SaveSeriesMetadataAsync)} for {nameof(tmdbId)} {tmdbId}. {ex.Message}");
            }
        }

        public async Task SaveMultipleSeriesMetadataRecordsAsync(string language, List<TMDBSearchSeriesResponseDto> metadataRecords)
        {
            try
            {
                Dictionary<string, string> seriesMetadataRecordsDictionary = metadataRecords.ToDictionary(
                    metadataRecord => $"{CachePrefixes.SeriesMetadataPrefix}:{metadataRecord.Id}:{language}",
                    metadataRecord => JsonSerializer.Serialize(metadataRecord)
                );

                await cacheStore.SetValuesAsync(
                    seriesMetadataRecordsDictionary,
                    TimeSpan.FromMinutes(_cacheExpirationInMinutes)
                );
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occurred in {nameof(SaveMultipleSeriesMetadataRecordsAsync)}: {ex.Message}");
            }
        }

        public async Task SaveSeriesTrailersAsync(int tmdbId, string language, List<KinocheckVideoDto> trailers)
        {
            try
            {
                await cacheStore.SetValueAsync(
                    $"{CachePrefixes.SeriesTrailersPrefix}:{tmdbId}:{language}",
                    JsonSerializer.Serialize(trailers),
                    TimeSpan.FromMinutes(_cacheExpirationInMinutes)
                );
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occurred in {nameof(SaveSeriesTrailersAsync)} for {nameof(tmdbId)} {tmdbId}. {ex.Message}");
            }
        }
    }
}
