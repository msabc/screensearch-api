using ScreenSearch.Domain.Models.Caching;

namespace ScreenSearch.Domain.Interfaces.Repositories
{
    public interface ISupportedLanguageRepository
    {
        Task SaveAsync(List<SupportedLanguage> languages);

        Task<List<SupportedLanguage>> GetAsync();
    }
}
