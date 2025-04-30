using ScreenSearch.Application.Models.Request.Search.Base;

namespace ScreenSearch.Application.Models.Request.Search
{
    public class SearchSeriesRequest : BaseSearchRequest
    {
        public bool? IncludeAdult { get; set; }

        public int? FirstAirDateYear { get; set; }
    }
}
