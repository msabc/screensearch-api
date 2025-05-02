namespace ScreenSearch.Configuration.Models.RateLimiting
{
    public class TMDBRateLimitingSettingsElement
    {
        public int NumberOfRequestsPermitted { get; set; }

        public int WindowSizeInSeconds { get; set; }
    }
}
