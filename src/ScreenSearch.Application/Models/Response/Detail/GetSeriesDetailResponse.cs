using ScreenSearch.Domain.Models.Services.External.Kinocheck.Dto;
using ScreenSearch.Domain.Models.Services.External.TMDB.Details;

namespace ScreenSearch.Application.Models.Response.Detail
{
    public class GetSeriesDetailResponse
    {
        public TMDBGetShowDetailsResponse Metadata { get; set; }

        public List<KinocheckVideoDto> Videos { get; set; }
    }
}
