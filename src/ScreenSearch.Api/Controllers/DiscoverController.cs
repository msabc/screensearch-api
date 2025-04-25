using Microsoft.AspNetCore.Mvc;
using ScreenSearch.Api.Controllers.Base;
using ScreenSearch.Application.Models.Request.Discover;
using ScreenSearch.Application.Services.Discover;

namespace ScreenSearch.Api.Controllers
{
    [Route("[controller]")]
    public class DiscoverController(IDiscoverService discoverService) : BaseScreenSearchController
    {
        [HttpGet("movies")]
        public async Task<IActionResult> GetMoviesAsync([FromQuery] DiscoverRequest request)
        {
            return Ok(await discoverService.GetMoviesAsync(request));
        }

        [HttpGet("shows")]
        public async Task<IActionResult> GetShowsAsync([FromQuery] DiscoverRequest request)
        {
            return Ok(await discoverService.GetShowsAsync(request));
        }
    }
}
