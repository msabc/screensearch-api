using System.Text.Json.Serialization;

namespace ScreenSearch.Domain.Models.Services.TMDB.Languages
{
    public class LanguageResponseDto
    {
        [JsonPropertyName("iso_639_1")]
        public string ISO6391 { get; set; }

        [JsonPropertyName("english_name")]
        public string EnglishName { get; set; }

        public string Name { get; set; }
    }
}
