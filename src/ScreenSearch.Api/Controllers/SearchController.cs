using Microsoft.AspNetCore.Mvc;
using ScreenSearch.Api.Controllers.Base;
using ScreenSearch.Application.Models.Request.Search;
using ScreenSearch.Application.Services.Search;

namespace ScreenSearch.Api.Controllers
{
    [Route("[controller]")]
    public class SearchController(ISearchService searchService) : BaseScreenSearchController
    {
        [HttpGet("movies")]
        public async Task<IActionResult> SearchMoviesAsync([FromQuery] SearchRequest request)
        {
            return Ok(await searchService.SearchMoviesAsync(request));
        }

        [HttpGet("shows")]
        public async Task<IActionResult> SearchShowsAsync([FromQuery] SearchRequest request)
        {
            return Ok(await searchService.SearchShowsAsync(request));
        }
    }
}
