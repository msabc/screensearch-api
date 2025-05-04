using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ScreenSearch.Api.Controllers.Base;
using ScreenSearch.Application.Models.Enums;
using ScreenSearch.Application.Services.Trailer;

namespace ScreenSearch.Api.Controllers.V1
{
    [ApiVersion(1.0)]
    [Route("v{version:apiVersion}/[controller]")]
    public class TrailerController(ITrailerService trailerService) : BaseScreenSearchController
    {
        [HttpGet("{tmdbId}")]
        public async Task<IActionResult> GetTrailerByIdAsync(int tmdbId, Language langage)
        {
            return Ok(await trailerService.GetTrailersByIdAsync(tmdbId, langage));
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetLatestAsync(Language langage, int? page)
        {
            return Ok(await trailerService.GetLatestAsync(langage, page));
        }

        [HttpGet("trending")]
        public async Task<IActionResult> GetTrendingAsync(Language langage, int? page)
        {
            return Ok(await trailerService.GetTrendingAsync(langage, page));
        }
    }
}
