using Microsoft.AspNetCore.Mvc;
using ScreenSearch.Api.Controllers.Base;
using ScreenSearch.Application.Services.Trailer;

namespace ScreenSearch.Api.Controllers
{
    [Route("[controller]")]
    public class TrailerController(ITrailerService trailerService) : BaseScreenSearchController
    {
        [HttpGet("{tmdbId}")]
        public async Task<IActionResult> GetTrailerByIdAsync(int tmdbId)
        {
            return Ok(await trailerService.GetTrailersAsync(tmdbId));
        }
    }
}
