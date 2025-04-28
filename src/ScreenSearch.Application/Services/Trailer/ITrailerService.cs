using ScreenSearch.Application.Models.Response;
using ScreenSearch.Application.Models.Response.Trailer;

namespace ScreenSearch.Application.Services.Trailer
{
    public interface ITrailerService
    {
        Task<IEnumerable<MovieTrailerDto>> GetTrailersAsync(int tmdbId);

        Task<PagedResponse<MovieTrailerDto>> GetLatestAsync(int page);
    }
}
