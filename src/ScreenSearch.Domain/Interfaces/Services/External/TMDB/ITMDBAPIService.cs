using ScreenSearch.Domain.Models.Services.External.TMDB.Discover;

namespace ScreenSearch.Domain.Interfaces.Services.External.TMDB
{
    public interface ITMDBAPIService
    {
        Task<TMDBGetResponse> DiscoverMoviesAsync(TMDBGetRequest request);

        Task<TMDBGetResponse> DiscoverShowsAsync(TMDBGetRequest request);

        Task<TMDBGetResponse> SearchMoviesAsync(TMDBGetRequest request);

        Task<TMDBGetResponse> SearchShowsAsync(TMDBGetRequest request);
    }
}
