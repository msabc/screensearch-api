using ScreenSearch.Configuration.Models;
using ScreenSearch.Configuration.Models.External;
using ScreenSearch.Configuration.Models.Jobs;

namespace ScreenSearch.Configuration
{
    public record ScreenSearchSettings
    {
        public TMDBAPISettingsElement TMDBAPISettings { get; set; }

        public KinocheckAPISettingsElement KinocheckAPISettings { get; set; }

        public CacheSettingsElement CacheSettings { get; set; }

        public TrendingJobSettingsElement TrendingJobSettings { get; set; }

        public SupportedLanguagesJobSettingsElement SupportedLanguagesJobSettings { get; set; }

        public RateLimitSettingsElement RateLimitSettings { get; set; }

        public LanguageSettingsElement LanguageSettings { get; set; }
    }
}
