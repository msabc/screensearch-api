using ScreenSearch.Domain.Models.Services.TMDB.Discover;

namespace ScreenSearch.Domain.Interfaces.Services.TMDB
{
    public interface ITMDBAPIService
    {
        Task<GetTMDBDiscoverResponse> DiscoverMoviesAsync(GetTMDBDiscoverRequest request);

        Task<GetTMDBDiscoverResponse> DiscoverShowsAsync(GetTMDBDiscoverRequest request);
    }
}
