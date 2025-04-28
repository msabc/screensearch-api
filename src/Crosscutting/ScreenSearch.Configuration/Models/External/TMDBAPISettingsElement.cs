namespace ScreenSearch.Configuration.Models.External
{
    public class TMDBAPISettingsElement
    {
        public required string AccessToken { get; set; }

        public required string BaseURL { get; set; }

        public required string DiscoverMoviesPath { get; set; }

        public required string DiscoverShowsPath { get; set; }

        public required string SearchMoviesPath { get; set; }

        public required string SearchShowsPath { get; set; }

    }
}
