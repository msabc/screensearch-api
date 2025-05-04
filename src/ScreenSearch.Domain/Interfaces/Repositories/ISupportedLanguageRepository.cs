using ScreenSearch.Domain.Models.Caching;

namespace ScreenSearch.Domain.Interfaces.Repositories
{
    public interface ISupportedLanguageRepository
    {
        Task SaveSupportedLanguagesAsync(List<SupportedLanguage> languages);

        Task<List<SupportedLanguage>> GetSupportedLanguagesAsync();
    }
}
