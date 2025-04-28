using ScreenSearch.Domain.Models.Services.External.Kinocheck;
using ScreenSearch.Domain.Models.Services.External.Kinocheck.Dto;

namespace ScreenSearch.Domain.Interfaces.Services.External.Kinocheck
{
    public interface IKinocheckService
    {
        Task<IEnumerable<KinocheckTrailerDto>> GetTrailersAsync(int tmdbId);

        Task<GetTrailersResponse> GetLatestTrailersAsync(int? page);

        Task<GetTrailersResponse> GetTrendingTrailersAsync(int? page);
    }
}
