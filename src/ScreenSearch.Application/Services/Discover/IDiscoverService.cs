using ScreenSearch.Application.Models.Request.Discover;
using ScreenSearch.Application.Models.Response.Discover;

namespace ScreenSearch.Application.Services.Discover
{
    public interface IDiscoverService
    {
        Task<DiscoverResponse> GetMoviesAsync(DiscoverRequest request);

        Task<DiscoverResponse> GetShowsAsync(DiscoverRequest request);
    }
}
