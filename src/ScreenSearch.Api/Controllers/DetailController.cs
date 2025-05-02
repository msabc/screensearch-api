using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ScreenSearch.Api.Constants;
using ScreenSearch.Api.Controllers.Base;
using ScreenSearch.Application.Models.Enums;
using ScreenSearch.Application.Services.Detail;

namespace ScreenSearch.Api.Controllers
{
    [Route("[controller]")]
    [EnableRateLimiting(RateLimitPolicies.TMDBPolicy)]
    public class DetailController(IDetailService detailService) : BaseScreenSearchController
    {
        [HttpGet("movies/{tmdbId}")]
        public async Task<IActionResult> GetMovieDetailsByIdAsync(int tmdbId, Language language)
        {
            return Ok(await detailService.GetMovieDetailsByIdAsync(tmdbId, language));
        }

        [HttpGet("series/{tmdbId}")]
        public async Task<IActionResult> GetShowDetailsByIdAsync(int tmdbId, Language language)
        {
            return Ok(await detailService.GetSeriesDetailsByIdAsync(tmdbId, language));
        }
    }
}
