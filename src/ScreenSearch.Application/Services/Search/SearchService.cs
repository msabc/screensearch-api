using ScreenSearch.Application.Mapper;
using ScreenSearch.Application.Models.Request.Search;
using ScreenSearch.Application.Models.Response;
using ScreenSearch.Application.Models.Response.Search.Dto;
using ScreenSearch.Application.Services.LanguageResolver;
using ScreenSearch.Domain.Interfaces.Services.External.TMDB;

namespace ScreenSearch.Application.Services.Search
{
    public class SearchService(
        ILanguageResolverService languageResolverService,
        ITMDBService tmdbService) : ISearchService
    {
        public async Task<PagedResponse<SearchMoviesResponseDto>> SearchMoviesAsync(SearchMoviesRequest request)
        {
            string language = await languageResolverService.ParseLanguageAsync(nameof(SearchMoviesRequest.Language));

            var response = await tmdbService.SearchMoviesAsync(request.MapToInfrastructureRequest(language));

            return response.MapToResponse();
        }

        public async Task<PagedResponse<SearchSeriesResponseDto>> SearchSeriesAsync(SearchSeriesRequest request)
        {
            string language = await languageResolverService.ParseLanguageAsync(nameof(SearchMoviesRequest.Language));

            var response = await tmdbService.SearchSeriesAsync(request.MapToInfrastructureRequest(language));

            return response.MapToResponse();
        }
    }
}
