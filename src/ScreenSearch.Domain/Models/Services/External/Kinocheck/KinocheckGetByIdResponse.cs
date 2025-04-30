using System.Text.Json.Serialization;
using ScreenSearch.Domain.Models.Services.External.Kinocheck.Dto;

namespace ScreenSearch.Domain.Models.Services.External.Kinocheck
{
    public class KinocheckGetByIdResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("tmdb_id")]
        public int TmdbId { get; set; }

        [JsonPropertyName("imdb_id")]
        public string ImdbId { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("trailer")]
        public KinocheckVideoDto Trailer { get; set; }

        [JsonPropertyName("videos")]
        public List<KinocheckVideoDto> Videos { get; set; }
    }
}
