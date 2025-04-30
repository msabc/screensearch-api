using ScreenSearch.Domain.Models.Services.External.TMDB.Details;
using ScreenSearch.Domain.Models.Services.External.TMDB.Search.Request;
using ScreenSearch.Domain.Models.Services.External.TMDB.Search.Response;
using ScreenSearch.Domain.Models.Services.External.TMDB.Search.Response.Dto;

namespace ScreenSearch.Domain.Interfaces.Services.External.TMDB
{
    public interface ITMDBService
    {
        Task<TMDBPagedResponse<TMDBSearchMoviesResponseDto>> SearchMoviesAsync(TMDBSearchMoviesRequest request);

        Task<TMDBPagedResponse<TMDBSearchSeriesResponseDto>> SearchSeriesAsync(TMDBSearchSeriesRequest request);

        Task<TMDBSearchMoviesResponseDto> GetMovieDetailsByIdAsync(int tmdbId, string language);

        Task<TMDBGetShowDetailsResponse> GetSeriesDetailsByIdAsync(int tmdbId, string language);
    }
}
