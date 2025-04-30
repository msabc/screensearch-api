using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ScreenSearch.Domain.Models.Services.External.TMDB.Search.Request
{
    public class TMDBSearchMoviesRequest : BaseTMDBSearchRequest
    {
        [JsonPropertyName("primary_release_year")]
        public int? PrimaryReleaseYear { get; set; }

        [JsonPropertyName("region")]
        public string? Region { get; set; }
    }
}
