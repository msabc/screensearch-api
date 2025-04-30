using ScreenSearch.Application.Models.Enums;
using ScreenSearch.Application.Models.Response;
using ScreenSearch.Application.Models.Response.Trailer.Dto;

namespace ScreenSearch.Application.Services.Trailer
{
    public interface ITrailerService
    {
        Task<IEnumerable<MovieTrailerDto>> GetTrailersByIdAsync(int tmdbId, Language language);

        Task<PagedResponse<MovieTrailerDto>> GetLatestAsync(Language language, int? page);

        Task<PagedResponse<MovieTrailerDto>> GetTrendingAsync(Language language, int? page);
    }
}
