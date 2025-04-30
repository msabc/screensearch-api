using ScreenSearch.Application.Models.Enums;
using ScreenSearch.Application.Models.Response.Detail;

namespace ScreenSearch.Application.Services.Detail
{
    public interface IDetailService
    {
        Task<GetMovieDetailResponse> GetMovieDetailsByIdAsync(int tmdbId, Language language);

        Task<GetSeriesDetailResponse> GetSeriesDetailsByIdAsync(int tmdbId, Language language);
    }
}
