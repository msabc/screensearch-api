using System.Text.Json.Serialization;

namespace ScreenSearch.Domain.Models.Services.External.TMDB.Details
{
    public class TMDBGetShowDetailsResponse
    {
        [JsonPropertyName("adult")]
        public bool Adult { get; set; }

        [JsonPropertyName("backdrop_path")]
        public string BackdropPath { get; set; }

        [JsonPropertyName("created_by")]
        public List<Creator> CreatedBy { get; set; }

        [JsonPropertyName("episode_run_time")]
        public List<int> EpisodeRunTime { get; set; }

        [JsonPropertyName("first_air_date")]
        public string FirstAirDate { get; set; }

        [JsonPropertyName("genres")]
        public List<Genre> Genres { get; set; }

        [JsonPropertyName("homepage")]
        public string Homepage { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("in_production")]
        public bool InProduction { get; set; }

        [JsonPropertyName("languages")]
        public List<string> Languages { get; set; }

        [JsonPropertyName("last_air_date")]
        public string LastAirDate { get; set; }

        [JsonPropertyName("last_episode_to_air")]
        public Episode LastEpisodeToAir { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("next_episode_to_air")]
        public Episode NextEpisodeToAir { get; set; }

        [JsonPropertyName("networks")]
        public List<Network> Networks { get; set; }

        [JsonPropertyName("number_of_episodes")]
        public int NumberOfEpisodes { get; set; }

        [JsonPropertyName("number_of_seasons")]
        public int NumberOfSeasons { get; set; }

        [JsonPropertyName("origin_country")]
        public List<string> OriginCountry { get; set; }

        [JsonPropertyName("original_language")]
        public string OriginalLanguage { get; set; }

        [JsonPropertyName("original_name")]
        public string OriginalName { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }

        [JsonPropertyName("popularity")]
        public double Popularity { get; set; }

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }

        [JsonPropertyName("production_companies")]
        public List<ProductionCompany> ProductionCompanies { get; set; }

        [JsonPropertyName("production_countries")]
        public List<ProductionCountry> ProductionCountries { get; set; }

        [JsonPropertyName("seasons")]
        public List<Season> Seasons { get; set; }

        [JsonPropertyName("spoken_languages")]
        public List<SpokenLanguage> SpokenLanguages { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("tagline")]
        public string Tagline { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("vote_average")]
        public double VoteAverage { get; set; }

        [JsonPropertyName("vote_count")]
        public int VoteCount { get; set; }
    }
}