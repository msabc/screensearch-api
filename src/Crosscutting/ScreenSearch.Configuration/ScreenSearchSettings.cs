using ScreenSearch.Configuration.Models;
using ScreenSearch.Configuration.Models.External;

namespace ScreenSearch.Configuration
{
    public record ScreenSearchSettings
    {
        public TMDBAPISettingsElement TMDBAPISettings { get; set; }

        public KinocheckAPISettingsElement KinocheckAPISettings { get; set; }

        public CacheSettingsElement CacheSettings { get; set; }

        public TrendingJobSettingsElement TrendingJobSettings { get; set; }
    }
}
