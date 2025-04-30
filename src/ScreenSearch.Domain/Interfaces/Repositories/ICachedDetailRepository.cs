using ScreenSearch.Domain.Models.Caching;
using ScreenSearch.Domain.Models.Services.External.Kinocheck.Dto;
using ScreenSearch.Domain.Models.Services.External.TMDB.Details;
using ScreenSearch.Domain.Models.Services.External.TMDB.Search.Response.Dto;

namespace ScreenSearch.Domain.Interfaces.Repositories
{
    public interface ICachedDetailRepository
    {
        Task<CachedMovieDetails> GetMovieDetailsAsync(int tmdbId, string language);

        Task<CachedShowDetails> GetSeriesDetailsAsync(int tmdbId, string language);

        Task SaveMovieMetadataAsync(int tmdbId, string language, TMDBSearchMoviesResponseDto metadata);

        Task SaveMovieTrailersAsync(int tmdbId, string language, List<KinocheckVideoDto> trailers);

        Task SaveSeriesMetadataAsync(int tmdbId, string language, TMDBGetShowDetailsResponse metadata);

        Task SaveSeriesTrailersAsync(int tmdbId, string language, List<KinocheckVideoDto> trailers);
    }
}
