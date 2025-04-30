using System.Net;
using System.Net.Http.Json;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ScreenSearch.Configuration;
using ScreenSearch.Domain.Interfaces.Services.External.TMDB;
using ScreenSearch.Domain.Models.Services.External.TMDB.Details;
using ScreenSearch.Domain.Models.Services.External.TMDB.Search.Request;
using ScreenSearch.Domain.Models.Services.External.TMDB.Search.Response;
using ScreenSearch.Domain.Models.Services.External.TMDB.Search.Response.Dto;
using ScreenSearch.Infrastructure.Extensions;

namespace ScreenSearch.Infrastructure.Services.External.TMDB
{
    public class TMDBService(
        IOptions<ScreenSearchSettings> screenSearchOptions,
        ILogger<TMDBService> logger,
        HttpClient httpClient) : ITMDBService
    {
        private readonly ScreenSearchSettings _settings = screenSearchOptions.Value;

        public async Task<TMDBPagedResponse<TMDBSearchMoviesResponseDto>> SearchMoviesAsync(TMDBSearchMoviesRequest request)
        {
            try
            {
                var queryParameterStringBuilder = new StringBuilder();

                var queryParams = request.ToQueryDictionary();

                foreach (var kvp in queryParams)
                {
                    queryParameterStringBuilder.Append($"{kvp.Key}={WebUtility.UrlEncode(kvp.Value)}");
                    queryParameterStringBuilder.Append('&');
                }

                queryParameterStringBuilder.Remove(queryParameterStringBuilder.Length - 1, 1);

                string requestUri = $"{_settings.TMDBAPISettings.SearchMoviesPath}?{queryParameterStringBuilder}";

                return await httpClient.GetFromJsonAsync<TMDBPagedResponse<TMDBSearchMoviesResponseDto>>(requestUri);
            }
            catch (Exception ex)
            {
                logger.LogError($"{nameof(TMDBService)}.{nameof(SearchMoviesAsync)} error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<TMDBPagedResponse<TMDBSearchSeriesResponseDto>> SearchSeriesAsync(TMDBSearchSeriesRequest request)
        {
            try
            {
                var queryParameterStringBuilder = new StringBuilder();

                var queryParams = request.ToQueryDictionary();

                foreach (var kvp in queryParams)
                    queryParameterStringBuilder.Append($"{kvp.Key}={WebUtility.UrlEncode(kvp.Value)}");

                string requestUri = $"{_settings.TMDBAPISettings.SearchSeriesPath}?{queryParameterStringBuilder}";

                return await httpClient.GetFromJsonAsync<TMDBPagedResponse<TMDBSearchSeriesResponseDto>>(requestUri);
            }
            catch (Exception ex)
            {
                logger.LogError($"{nameof(TMDBService)}.{nameof(SearchSeriesAsync)} error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<TMDBSearchMoviesResponseDto> GetMovieDetailsByIdAsync(int tmdbId, string language)
        {
            try
            {
                string requestUri = $"{_settings.TMDBAPISettings.GetMovieByIdPath}/{tmdbId}?{QueryParameterNames.Language}={language}";

                return await httpClient.GetFromJsonAsync<TMDBSearchMoviesResponseDto>(requestUri);
            }
            catch (Exception ex)
            {
                logger.LogError($"{nameof(TMDBService)}.{nameof(GetMovieDetailsByIdAsync)} error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<TMDBGetShowDetailsResponse> GetSeriesDetailsByIdAsync(int tmdbId, string language)
        {
            try
            {
                string requestUri = $"{_settings.TMDBAPISettings.GetShowByIdPath}/{tmdbId}?{QueryParameterNames.Language}={language}";
                
                return await httpClient.GetFromJsonAsync<TMDBGetShowDetailsResponse>(requestUri);
            }
            catch (Exception ex)
            {
                logger.LogError($"{nameof(TMDBService)}.{nameof(GetSeriesDetailsByIdAsync)} error occurred: {ex.Message}");
                throw;
            }
        }
    }
}
