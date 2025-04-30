using Microsoft.AspNetCore.Http;
using ScreenSearch.Application.Mapper;
using ScreenSearch.Application.Models.Request.Search;
using ScreenSearch.Application.Models.Response;
using ScreenSearch.Application.Models.Response.Search;
using ScreenSearch.Application.Models.Response.Search.Dto;
using ScreenSearch.Domain.Interfaces.Services.External.TMDB;

namespace ScreenSearch.Application.Services.Search
{
    public class SearchService(ITMDBService tmdbService) : ISearchService
    {
        public async Task<PagedResponse<SearchMoviesResponseDto>> SearchMoviesAsync(SearchMoviesRequest request)
        {
            var response = await tmdbService.SearchMoviesAsync(request.MapToInfrastructureRequest());

            return response.MapToResponse();
        }

        public async Task<PagedResponse<SearchSeriesResponseDto>> SearchSeriesAsync(SearchSeriesRequest request)
        {
            var response = await tmdbService.SearchSeriesAsync(request.MapToInfrastructureRequest());

            return response.MapToResponse();
        }
    }
}
