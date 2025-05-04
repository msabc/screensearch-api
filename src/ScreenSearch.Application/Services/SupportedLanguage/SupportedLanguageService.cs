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
            var supportedLanguages = await languageRepository.GetSupportedLanguagesAsync();

            if (supportedLanguages != null && supportedLanguages.Count != 0) {
                return new GetSupportedLanguagesResponse()
                {
                    Languages = [.. supportedLanguages.Select(x => x.ISO6391)]
                };
            }
            else
            {
                logger.LogWarning("Zero languages received from the repository. Reverting to an external API.");

                var tmdbLanguages = await tmdbService.GetLanguagesAsync();

                if (tmdbLanguages != null && tmdbLanguages.Count != 0)
                {
                    return new GetSupportedLanguagesResponse()
                    {
                        Languages = [.. tmdbLanguages.Select(x => x.ISO6391)]
                    };
                }
                else
                {
                    return new GetSupportedLanguagesResponse()
                    {
                        Languages = [_fallbackCulture]
                    };
                }
            }
        }

        public async Task SaveSupportedLanguagesAsync()
        {
            var tmdbLanguages = await tmdbService.GetLanguagesAsync();

            if (tmdbLanguages == null || tmdbLanguages.Count == 0)
            {
                logger.LogWarning($"Zero languages received from the repository when attempting to save data.");
            }
            else
            {
                var languageCollection = tmdbLanguages.Select(x => x.MapToRepositoryData()).ToList();

                await languageRepository.SaveSupportedLanguagesAsync(languageCollection);

            }
        }
    }
}
