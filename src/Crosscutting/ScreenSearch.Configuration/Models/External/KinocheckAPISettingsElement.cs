namespace ScreenSearch.Configuration.Models.External
{
    public class KinocheckAPISettingsElement
    {
        public required string BaseURL { get; set; }

        public required string GetMovieVideosPath { get; set; }

        public required string GetSeriesVideosPath { get; set; }

        public required string GetTrailersPath { get; set; }

        public required string GetTrendingTrailersPath { get; set; }

        public required string GetLatestTrailersPath { get; set; }
    }
}
