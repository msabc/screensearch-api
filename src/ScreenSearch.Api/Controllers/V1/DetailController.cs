using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ScreenSearch.Api.Controllers.Base;
using ScreenSearch.Application.Services.Detail;

namespace ScreenSearch.Api.Controllers.V1
{
    [ApiVersion(1.0)]
    [Route("v{version:apiVersion}/[controller]")]
    public class DetailController(IDetailService detailService) : BaseScreenSearchController
    {
        [HttpGet("movies/{tmdbId}")]
        public async Task<IActionResult> GetMovieDetailsByIdAsync([FromRoute] int tmdbId)
        {
            return Ok(await detailService.GetMovieDetailsByIdAsync(tmdbId));
        }

        [HttpGet("series/{tmdbId}")]
        public async Task<IActionResult> GetShowDetailsByIdAsync([FromRoute]  int tmdbId)
        {
            return Ok(await detailService.GetSeriesDetailsByIdAsync(tmdbId));
        }
    }
}
