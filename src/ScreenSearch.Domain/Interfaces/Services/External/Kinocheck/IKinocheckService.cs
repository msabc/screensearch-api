using ScreenSearch.Domain.Models.Services.External.Kinocheck;
using ScreenSearch.Domain.Models.Services.External.Kinocheck.Dto;

namespace ScreenSearch.Domain.Interfaces.Services.External.Kinocheck
{
    public interface IKinocheckService
    {
        Task<IEnumerable<KinocheckTrailerDto>> GetTrailersAsync(int tmdbId);

        Task<GetLatestTrailersResponse> GetLatestTrailersAsync(int page);

        //Task<KinocheckGetResponse> GetTrendingTrailersAsync(int page, int limit);
    }
}
