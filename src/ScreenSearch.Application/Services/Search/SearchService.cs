using Microsoft.AspNetCore.Http;
using ScreenSearch.Application.Mapper;
using ScreenSearch.Application.Models.Request.Search;
using ScreenSearch.Application.Models.Response.Search;
using ScreenSearch.Domain.Interfaces.Services.External.TMDB;

namespace ScreenSearch.Application.Services.Search
{
    public class SearchService(ITMDBAPIService tmdbService) : ISearchService
    {
        public async Task<SearchResponse> SearchMoviesAsync(SearchRequest request)
        {
            var response = await tmdbService.SearchMoviesAsync(request.MapToInfrastructureRequest());

            return response.MapToResponse();
        }

        public async Task<SearchResponse> SearchShowsAsync(SearchRequest request)
        {
            var response = await tmdbService.SearchShowsAsync(request.MapToInfrastructureRequest());

            return response.MapToResponse();
        }
    }
}
