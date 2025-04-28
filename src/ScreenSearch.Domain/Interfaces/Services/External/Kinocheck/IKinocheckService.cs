using ScreenSearch.Domain.Models.Services.External.Kinocheck;

namespace ScreenSearch.Domain.Interfaces.Services.External.Kinocheck
{
    public interface IKinocheckService
    {
        Task<IEnumerable<KinocheckGetResponse>> GetTrailersAsync(int tmdbId);

        //Task<KinocheckGetResponse> GetLatestTrailersAsync(int page);

        //Task<KinocheckGetResponse> GetTrendingTrailersAsync(int page, int limit);
    }
}
