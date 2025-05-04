using ScreenSearch.Domain.Interfaces.Caching;
using ScreenSearch.Domain.Interfaces.Repositories;
using ScreenSearch.Domain.Models.Caching;
using ScreenSearch.Infrastructure.Constants;
using System.Text.Json;

namespace ScreenSearch.Infrastructure.Repositories
{
    public class SupportedLanguageRepository(ICacheStore cacheStore) : ISupportedLanguageRepository
    {
        public async Task SaveSupportedLanguagesAsync(List<SupportedLanguage> languages)
        {
            await cacheStore.SetValueAsync($"{CacheKeys.Languages}", JsonSerializer.Serialize(languages), null);
        }

        public async Task<List<SupportedLanguage>> GetSupportedLanguagesAsync()
        {
            string? supportedLanguages = await cacheStore.GetValueAsync($"{CacheKeys.Languages}", null);

            if (!string.IsNullOrWhiteSpace(supportedLanguages))
                return JsonSerializer.Deserialize<List<SupportedLanguage>>(supportedLanguages);

            return [];
        }
    }
}
