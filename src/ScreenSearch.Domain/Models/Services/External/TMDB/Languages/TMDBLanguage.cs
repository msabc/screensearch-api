using System.Text.Json.Serialization;

namespace ScreenSearch.Domain.Models.Services.External.TMDB.Languages
{
    public class TMDBLanguage
    {
        [JsonPropertyName("iso_639_1")]
        public string ISO6391 { get; set; }

        [JsonPropertyName("english_name")]
        public string EnglishName { get; set; }

        public string Name { get; set; }
    }
}
