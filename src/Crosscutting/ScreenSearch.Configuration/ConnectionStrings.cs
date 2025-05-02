namespace ScreenSearch.Configuration
{
    public record ConnectionStrings
    {
        public string ApplicationInsightsConnectionString { get; set; }

        public string RedisConnectionString { get; set; }
    }
}
