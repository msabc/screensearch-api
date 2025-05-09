using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ScreenSearch.Application.Mapper;
using ScreenSearch.Application.Models.Response.SupportedLanguage;
using ScreenSearch.Configuration;
using ScreenSearch.Domain.Interfaces.Repositories;
using ScreenSearch.Domain.Interfaces.Services.External.TMDB;

namespace ScreenSearch.Application.Services.SupportedLanguage
{
    public class SupportedLanguageService(
        ISupportedLanguageRepository languageRepository,
        ITMDBService tmdbService,
        IOptions<ScreenSearchSettings> screenSearchOptions,
        ILogger<SupportedLanguageService> logger) : ISupportedLanguageService
    {
        private readonly string _fallbackCulture = screenSearchOptions.Value.LanguageSettings.FallbackCulture;

        public async Task<GetSupportedLanguagesResponse> GetSupportedLanguagesAsync()
        {
            return new GetSupportedLanguagesResponse()
            {
                Languages = await GetAsync()
            };
        }

        public async Task SaveSupportedLanguagesAsync()
        {
            var tmdbLanguages = await tmdbService.GetLanguagesAsync();

            if (tmdbLanguages == null || tmdbLanguages.Count == 0)
            {
                logger.LogWarning($"{nameof(SaveSupportedLanguagesAsync)}: No supported languages received when saving.");
            }
            else
            {
                var languageCollection = tmdbLanguages.Select(x => x.MapToRepositoryData()).ToList();

                await languageRepository.SaveAsync(languageCollection);
            }
        }

        public async Task<bool> IsLanguageSupportedAsync(string language)
        {
            // not reverting to an HTTP calling in case of a missing language is intentional
            var supportedLanguages = await GetAsync();

            return supportedLanguages.Contains(language);
        }

        private async Task<List<string>> GetAsync()
        {
            var supportedLanguages = await languageRepository.GetAsync();

            if (supportedLanguages != null && supportedLanguages.Count != 0)
            {
                return [.. supportedLanguages.Select(x => x.ISO6391)];
            }
            else
            {
                logger.LogWarning($"{nameof(GetAsync)}: No supported languages received from cache during retrieval.");

                var tmdbLanguages = await tmdbService.GetLanguagesAsync();

                if (tmdbLanguages != null && tmdbLanguages.Count != 0)
                {
                    return [.. tmdbLanguages.Select(x => x.ISO6391)];
                }
                else
                {
                    logger.LogWarning($"{nameof(GetAsync)}: No supported languages received from TMDB during retrieval.");

                    return [_fallbackCulture];
                }
            }
        }
    }
}
