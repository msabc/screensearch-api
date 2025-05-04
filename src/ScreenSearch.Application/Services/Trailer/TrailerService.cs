using ScreenSearch.Application.Mapper;
using ScreenSearch.Application.Models.Response;
using ScreenSearch.Application.Models.Response.Trailer.Dto;
using ScreenSearch.Domain.Interfaces.Services.External.Kinocheck;

namespace ScreenSearch.Application.Services.Trailer
{
    public class TrailerService(IKinocheckService kinocheckService) : ITrailerService
    {
        public async Task<List<MovieTrailerDto>> GetTrailersByIdAsync(int tmdbId)
        {
            var response = await kinocheckService.GetTrailersAsync(tmdbId);

            return [.. response.Select(x => x.MapToDto())];
        }

        public async Task<PagedResponse<MovieTrailerDto>> GetLatestAsync(int? page)
        {
            var response = await kinocheckService.GetLatestTrailersAsync(page);

            return response.MapToPagedResponse();
        }

        public async Task<PagedResponse<MovieTrailerDto>> GetTrendingAsync(int? page)
        {
            var response = await kinocheckService.GetTrendingTrailersAsync(page);

            return response.MapToPagedResponse();
        }
    }
}
