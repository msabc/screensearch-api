using ScreenSearch.Domain.Models.Services.External.Kinocheck.Dto;
using ScreenSearch.Domain.Models.Services.External.TMDB.Search.Response.Dto;

namespace ScreenSearch.Application.Models.Response.Detail
{
    public class GetMovieDetailResponse
    {
        public TMDBSearchMoviesResponseDto Metadata { get; set; }

        public KinocheckVideoDto Trailer { get; set; }

        public List<KinocheckVideoDto> Videos { get; set; }
    }
}
