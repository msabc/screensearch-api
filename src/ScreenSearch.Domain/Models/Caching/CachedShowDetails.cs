using ScreenSearch.Domain.Models.Services.External.Kinocheck.Dto;
using ScreenSearch.Domain.Models.Services.External.TMDB.Details;

namespace ScreenSearch.Domain.Models.Caching
{
    public class CachedShowDetails
    {
        public TMDBGetShowDetailsResponse Metadata { get; set; }

        public List<KinocheckVideoDto> Videos { get; set; }
    }
}
