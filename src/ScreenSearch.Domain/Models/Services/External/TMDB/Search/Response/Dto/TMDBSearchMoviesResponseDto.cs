using System.Text.Json.Serialization;

namespace ScreenSearch.Domain.Models.Services.External.TMDB.Search.Response.Dto
{
    public class TMDBSearchMoviesResponseDto
    {
        public int Id { get; set; }

        public bool Adult { get; set; } = true;

        [JsonPropertyName("backdrop_path")]
        public string BackdropPath { get; set; }

        [JsonPropertyName("genre_ids")]
        public List<int> GenreIds { get; set; }

        public string Overview { get; set; }

        [JsonPropertyName("original_language")]
        public string OriginalLanguage { get; set; }

        [JsonPropertyName("original_title")]
        public string OriginalTitle { get; set; }

        public double Popularity { get; set; }

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; }

        public string Title { get; set; }

        [JsonPropertyName("video")]
        public bool Video { get; set; } = true;

        [JsonPropertyName("vote_average")]
        public double VoteAverage { get; set; } = 0;

        [JsonPropertyName("vote_count")]
        public int VoteCount { get; set; } = 0;
    }
}
