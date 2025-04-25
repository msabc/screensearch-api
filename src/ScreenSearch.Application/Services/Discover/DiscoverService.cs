using ScreenSearch.Application.Mapper;
using ScreenSearch.Application.Models.Request.Discover;
using ScreenSearch.Application.Models.Response.Discover;
using ScreenSearch.Domain.Interfaces.Services.TMDB;

namespace ScreenSearch.Application.Services.Discover
{
    public class DiscoverService(ITMDBAPIService tmdbService) : IDiscoverService
    {
        public async Task<DiscoverResponse> GetMoviesAsync(DiscoverRequest request)
        {
            var response = await tmdbService.DiscoverMoviesAsync(request.MapToInfrastructureRequest());

            return response.MapToResponse();
        }

        public async Task<DiscoverResponse> GetShowsAsync(DiscoverRequest request)
        {
            var response = await tmdbService.DiscoverShowsAsync(request.MapToInfrastructureRequest());

            return response.MapToResponse();
        }
    }
}
