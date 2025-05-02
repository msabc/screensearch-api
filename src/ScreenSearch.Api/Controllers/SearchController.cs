using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ScreenSearch.Api.Constants;
using ScreenSearch.Api.Controllers.Base;
using ScreenSearch.Application.Models.Request.Search;
using ScreenSearch.Application.Services.Search;

namespace ScreenSearch.Api.Controllers
{
    [Route("[controller]")]
    [EnableRateLimiting(RateLimitPolicies.TMDBPolicy)]
    public class SearchController(ISearchService searchService) : BaseScreenSearchController
    {
        [HttpGet("movies")]
        public async Task<IActionResult> SearchMoviesAsync([FromQuery] SearchMoviesRequest request)
        {
            return Ok(await searchService.SearchMoviesAsync(request));
        }

        [HttpGet("shows")]
        public async Task<IActionResult> SearchSeriesAsync([FromQuery] SearchSeriesRequest request)
        {
            return Ok(await searchService.SearchSeriesAsync(request));
        }
    }
}
