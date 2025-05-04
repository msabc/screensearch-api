using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ScreenSearch.Api.Constants;
using ScreenSearch.Api.Controllers.Base;
using ScreenSearch.Application.Models.Request.Search;
using ScreenSearch.Application.Services.Search;

namespace ScreenSearch.Api.Controllers.V1
{
    [ApiVersion(1.0)]
    [Route("v{version:apiVersion}/[controller]")]
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
