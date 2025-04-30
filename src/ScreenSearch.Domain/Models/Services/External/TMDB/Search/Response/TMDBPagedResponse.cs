using System.Text.Json.Serialization;

namespace ScreenSearch.Domain.Models.Services.External.TMDB.Search.Response
{
    public class TMDBPagedResponse<T> where T : class
    {
        public int Page { get; set; }

        public required IEnumerable<T> Results { get; set; }

        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("total_results")]
        public int TotalResults { get; set; }
    }
}
