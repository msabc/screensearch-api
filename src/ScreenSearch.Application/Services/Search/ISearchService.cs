using ScreenSearch.Application.Models.Request.Search;
using ScreenSearch.Application.Models.Response;
using ScreenSearch.Application.Models.Response.Search.Dto;

namespace ScreenSearch.Application.Services.Search
{
    public interface ISearchService
    {
        Task<PagedResponse<SearchMoviesResponseDto>> SearchMoviesAsync(SearchMoviesRequest request);

        Task<PagedResponse<SearchSeriesResponseDto>> SearchSeriesAsync(SearchSeriesRequest request);
    }
}
