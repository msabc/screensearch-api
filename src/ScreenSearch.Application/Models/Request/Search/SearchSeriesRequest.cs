using ScreenSearch.Application.Models.Request.Search.Base;

namespace ScreenSearch.Application.Models.Request.Search
{
    public class SearchSeriesRequest : BaseSearchRequest
    {
        public int? FirstAirDateYear { get; set; }
    }
}
