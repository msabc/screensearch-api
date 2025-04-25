namespace ScreenSearch.Application.Models.Response
{
    public class PagedResponse<T> where T : class
    {
        public int Page { get; set; }

        public required IEnumerable<T> Results { get; set; }

        public int TotalPages { get; set; }

        public int TotalResults { get; set; }
    }
}
