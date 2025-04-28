using ScreenSearch.Application.Models.Request.Search;
using ScreenSearch.Application.Models.Response.Search;

namespace ScreenSearch.Application.Services.Search
{
    public interface ISearchService
    {
        Task<SearchResponse> SearchMoviesAsync(SearchRequest request);

        Task<SearchResponse> SearchShowsAsync(SearchRequest request);
    }
}
