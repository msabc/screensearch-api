using ScreenSearch.Application.Models.Response;
using ScreenSearch.Application.Models.Response.Trailer.Dto;

namespace ScreenSearch.Application.Services.Trailer
{
    public interface ITrailerService
    {
        Task<List<MovieTrailerDto>> GetTrailersByIdAsync(int tmdbId);

        Task<PagedResponse<MovieTrailerDto>> GetLatestAsync(int? page);

        Task<PagedResponse<MovieTrailerDto>> GetTrendingAsync(int? page);
    }
}
