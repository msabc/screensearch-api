namespace ScreenSearch.Application.Models.Request.Search
{
    public class SearchRequest
    {
        public bool? IncludeAdult { get; set; }

        public bool? IncludeVideo { get; set; }

        public string? Language { get; set; }

        public int? Page { get; set; }

        public int? Year { get; set; }

        public string? SortBy { get; set; }
    }
}
