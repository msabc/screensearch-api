using Microsoft.AspNetCore.Mvc;
using ScreenSearch.Api.Controllers.Base;

namespace ScreenSearch.Api.Controllers
{
    [Route("[controller]")]
    public class SearchController() : BaseScreenSearchController
    {
        [HttpGet]
        public async Task<IActionResult> PingAsync()
        {
            return Ok("pong");
        }
    }
}
