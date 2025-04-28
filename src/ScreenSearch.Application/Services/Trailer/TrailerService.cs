using ScreenSearch.Application.Mapper;
using ScreenSearch.Application.Models.Response.Trailer;
using ScreenSearch.Domain.Interfaces.Services.External.Kinocheck;

namespace ScreenSearch.Application.Services.Trailer
{
    public class TrailerService(IKinocheckService kinocheckService) : ITrailerService
    {
        public async Task<IEnumerable<MovieTrailerDto>> GetTrailersAsync(int tmdbId)
        {
            var response = await kinocheckService.GetTrailersAsync(tmdbId);

            return response.Select(x => x.MapToDto());
        }
    }
}
