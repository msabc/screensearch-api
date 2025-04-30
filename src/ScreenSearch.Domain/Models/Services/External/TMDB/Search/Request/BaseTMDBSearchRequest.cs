using System.Text.Json.Serialization;

namespace ScreenSearch.Domain.Models.Services.External.TMDB.Search.Request
{
    public class BaseTMDBSearchRequest
    {
        [JsonPropertyName("query")]
        public required string Query { get; set; }

        [JsonPropertyName("include_adult")]
        public bool? IncludeAdult { get; set; }

        [JsonPropertyName("language")]
        public string? Language { get; set; }

        [JsonPropertyName("page")]
        public int? Page { get; set; }

        [JsonPropertyName("year")]
        public int? Year { get; set; }
    }
}
