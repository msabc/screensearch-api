using ScreenSearch.Domain.Models.Caching;
using ScreenSearch.Domain.Models.Services.External.TMDB.Languages;

namespace ScreenSearch.Application.Mapper
{
    internal static class SupportedLanguageMapper
    {
        public static SupportedLanguage MapToRepositoryData(this TMDBLanguage language)
        {
            return new SupportedLanguage()
            {
                ISO6391 = language.ISO6391,
                EnglishName = language.EnglishName,
                Name = language.Name
            };
        }
    }
}
