using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ScreenSearch.Api.Controllers.Base;
using ScreenSearch.Application.Services.Trailer;

namespace ScreenSearch.Api.Controllers.V1
{
    [ApiVersion(1.0)]
    [Route("v{version:apiVersion}/[controller]")]
    public class TrailerController(ITrailerService trailerService) : BaseScreenSearchController
    {
        [HttpGet("{tmdbId}")]
        public async Task<IActionResult> GetTrailerByIdAsync(int tmdbId)
        {
            return Ok(await trailerService.GetTrailersByIdAsync(tmdbId));
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetLatestAsync(int? page)
        {
            return Ok(await trailerService.GetLatestAsync(page));
        }

        [HttpGet("trending")]
        public async Task<IActionResult> GetTrendingAsync(int? page)
        {
            return Ok(await trailerService.GetTrendingAsync(page));
        }
    }
}
