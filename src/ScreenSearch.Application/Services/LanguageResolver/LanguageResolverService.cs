using ScreenSearch.Application.Models.Enums;

namespace ScreenSearch.Application.Services.LanguageResolver
{
    public class LanguageResolverService : ILanguageResolverService
    {
        public string ParseLanguage(Language language)
        {
            return language switch
            {
                Language.English => "en",
                Language.German => "de",
                _ => "en",
            };
        }
    }
}
