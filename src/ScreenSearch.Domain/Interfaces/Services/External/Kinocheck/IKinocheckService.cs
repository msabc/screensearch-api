using ScreenSearch.Domain.Models.Services.External.Kinocheck;
using ScreenSearch.Domain.Models.Services.External.Kinocheck.Dto;

namespace ScreenSearch.Domain.Interfaces.Services.External.Kinocheck
{
    public interface IKinocheckService
    {
        Task<KinocheckGetByIdResponse> GetMovieVideosAsync(int tmdbId, string language);

        Task<KinocheckGetByIdResponse> GetSeriesVideosAsync(int tmdbId, string language);

        Task<IEnumerable<KinocheckVideoDto>> GetTrailersAsync(int tmdbId, string language);

        Task<KinocheckGetTrailersResponse> GetLatestTrailersAsync(int? page, string language);

        Task<KinocheckGetTrailersResponse> GetTrendingTrailersAsync(int? page, string language);
    }
}
