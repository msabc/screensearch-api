using ScreenSearch.Application.Models.Request.Search.Base;

namespace ScreenSearch.Application.Models.Request.Search
{
    public class SearchMoviesRequest : BaseSearchRequest
    {
        public int? PrimaryReleaseYear { get; set; }

        public string? Region { get; set; }
    }
}
