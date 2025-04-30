using ScreenSearch.Domain.Models.Services.External.Kinocheck.Dto;
using ScreenSearch.Domain.Models.Services.External.TMDB.Search.Response.Dto;

namespace ScreenSearch.Domain.Models.Caching
{
    public class CachedMovieDetails
    {
        public TMDBSearchMoviesResponseDto Metadata { get; set; }

        public List<KinocheckVideoDto> Videos { get; set; }
    }
}
