using ScreenSearch.Application.Models.Response.Detail;

namespace ScreenSearch.Application.Services.Detail
{
    public interface IDetailService
    {
        Task<GetMovieDetailResponse> GetMovieDetailsByIdAsync(int tmdbId, string? language);

        Task<GetSeriesDetailResponse> GetSeriesDetailsByIdAsync(int tmdbId, string? language);
    }
}
