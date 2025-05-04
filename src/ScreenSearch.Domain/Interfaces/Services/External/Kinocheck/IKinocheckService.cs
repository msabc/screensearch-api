using ScreenSearch.Domain.Models.Services.External.Kinocheck;
using ScreenSearch.Domain.Models.Services.External.Kinocheck.Dto;

namespace ScreenSearch.Domain.Interfaces.Services.External.Kinocheck
{
    public interface IKinocheckService
    {
        Task<KinocheckGetByIdResponse> GetMovieVideosAsync(int tmdbId);

        Task<KinocheckGetByIdResponse> GetSeriesVideosAsync(int tmdbId);

        Task<IEnumerable<KinocheckVideoDto>> GetTrailersAsync(int tmdbId);

        Task<KinocheckGetTrailersResponse> GetLatestTrailersAsync(int? page);

        Task<KinocheckGetTrailersResponse> GetTrendingTrailersAsync(int? page);
    }
}
