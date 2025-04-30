namespace ScreenSearch.Configuration.Models.External
{
    public class TMDBAPISettingsElement
    {
        public required string AccessToken { get; set; }

        public required string BaseURL { get; set; }

        public required string SearchMoviesPath { get; set; }

        public required string SearchSeriesPath { get; set; }

        public required string GetMovieByIdPath { get; set; }

        public required string GetShowByIdPath { get; set; }
    }
}
