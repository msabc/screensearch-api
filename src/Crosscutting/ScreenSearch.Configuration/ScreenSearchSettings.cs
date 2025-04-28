using ScreenSearch.Configuration.Models.External;

namespace ScreenSearch.Configuration
{
    public record ScreenSearchSettings
    {
        public TMDBAPISettingsElement TMDBAPISettings { get; set; }

        public KinocheckAPISettingsElement KinocheckAPISettings { get; set; }
    }
}
